using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Weapon_Manager : MonoBehaviour
{
    public float SwitchDelay = 1f; //무기 교체 딜레이
    public bool Switchable = true; // 무기스왑가능 여부 true면 가능
    public Enemy Enemy; //Enemy 스크립트 참조용
    public GameObject CurrentWeaponInstance; //적 생성이 착용 무기 인스턴스 생성용
    public Weapons CurrentWeapon; //현재 들고있는 무기의 Weapons 스크립트 참조용
    public Weapons FistInstance; //주먹인스턴스 생성용
    public Weapons Fist; //아무런 무기도 안 들고 있거나 총기 탄약 전부 소모시 주먹으로 변경용
    public Transform FirePos; // 투사체 발사 위치
    public Transform MeleePos; //근접무기 히트박스 위치
    public Transform PlayerPos; //플레이어 위치(사격 용)
    public Vector2 BoxSize; // 근접무기 히트박스
    public bool Attackable = true; // 공격 가능여부)
    public bool Reloading = false; //true면 리로드 중
    public bool isAtk = false; //전투돌입 시 공격
    public int BulletCount; //적 기본 총알 보유개수
    public Enemy_Weapon_Atk EnemyAtk;
    public Enemy_Gun_Atk gun;


    void Start()
    {
        
        GameObject.Instantiate(CurrentWeaponInstance, transform.position, Quaternion.identity).transform.parent = this.transform;
        CurrentWeapon = FindObjectOfType<Weapons>();
        BulletCount = CurrentWeapon.MaxRound * 2;
        GunBulletCheck();
    }


    void Update()
    {
        if(Enemy.hp >= 0)
        {
            StopAllCoroutines();
        }
        
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(MeleePos.position, BoxSize);
    }


    public IEnumerator UnarmdAttack(float dir_X, float dir_y)
    {
        yield return new WaitForSeconds(CurrentWeapon.AttackSpeed);
        //MeleePos.transform.position = new Vector3(dir_X, dir_y, 0);
        if (Attackable == true)
        {
            StartCoroutine(AttackableOn());
            Attackable = false;         
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(MeleePos.position, BoxSize, 0);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider is BoxCollider2D)
                {
                    if (collider.tag == "Player")
                    {
                        EnemyAtk.isMove = false;
                        EnemyAtk.rigid.velocity = Vector2.zero;
                        Enemy.animator.SetBool("isFistAtk", true);
                        Player player = collider.gameObject.GetComponent<Player>();
                        player.Hit(CurrentWeapon.Damage);
                        
                    }
                }

            }
        }

        
        StartCoroutine(UnarmdAttack(dir_X, dir_y));
    }

    public IEnumerator GunFire(Transform TargetPos) //적 총 발사
    {
        yield return new WaitForSeconds(CurrentWeapon.AttackSpeed + 1f);
        if (Attackable == true)
        {
            if (CurrentWeapon.Round > 0)
            {
                Vector2 direction = PlayerPos.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rototion = Quaternion.AngleAxis(angle-90, Vector3.forward);
                transform.rotation = rototion;

                Enemy.animator.SetBool("isOnehandGun", true);
                SoundManager.SharedInstance.PlaySE(CurrentWeapon.Weapon_Atk_Sound);
                CurrentWeapon.Round -= 1;
                Debug.Log("적 현재 총알: " + CurrentWeapon.Round);
                Attackable = false;
                StartCoroutine(AttackableOn());
                Instantiate(CurrentWeapon.enemyBulletPrefab, FirePos.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
                Debug.Log("적 사격");
                StartCoroutine(GunFire(PlayerPos));
            }
            else if (CurrentWeapon.Round <= 0) //총알 다 쓰면 재장전
            {
                if (BulletCount > 0) //총알이 있을 경우
                {
                    StopCoroutine(GunFire(PlayerPos));
                    SoundManager.SharedInstance.PlaySE(CurrentWeapon.Weapon_Reload_Sound);
                    Enemy.animator.SetBool("isRelord", true);
                    Attackable = false;
                    Reloading = true;
                    StartCoroutine(ReroldOn());
                    Debug.Log("적 재장전");
                    StartCoroutine(GunFire(PlayerPos));
                }
                else if (BulletCount <= 0)
                {
                    //총알 다 떨어져서 주먹으로 변경
                    Enemy.animator.SetBool("isOnehandGun", false);
                    EnemyAtk.isMove = true;
                    Debug.Log("적 주먹으로 무기 변경");
                    GameObject.Instantiate(FistInstance, transform.position, Quaternion.identity).transform.parent = this.transform;
                    CurrentWeapon = FindObjectOfType<Weapons>();
                    StartCoroutine(UnarmdAttack(EnemyAtk.direction.x, EnemyAtk.direction.y));
                }
            }

        }
        
    }

    public void GunBulletCheck() //무기 장착시 인벤에 있는 탄환에 맞게 장전
    {
        if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Fist)
        {

        }
        if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Onehand)
        {

        }
        if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.OnehandGun)
        {
            CurrentWeapon.EnemyBulletSetting(); //weapon의 BulletSetting로 발사하는 탄환의 대미지, 탄속 등 설정



        }
        if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.TwohandGun)
        {
            CurrentWeapon.EnemyBulletSetting(); //weapon의 BulletSetting로 발사하는 탄환의 대미지, 탄속 등 설정
        }
    }

    public void StopAtk()
    {
        if (isAtk == true)
        {
            isAtk = false;
        }
        else if (isAtk == false)
        {
            isAtk = true;
        }

    }

    public IEnumerator AttackableOn()
    {
        yield return new WaitForSeconds(CurrentWeapon.AttackSpeed);
        if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Unarmd || CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Onehand || CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Twohand || CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Fist)
        {
            EnemyAtk.isMove = true;
            Enemy.animator.SetBool("isFistAtk", false);

        }
        if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.OnehandGun || CurrentWeapon.weaponTypes == Weapons.WeaponTypes.TwohandGun || CurrentWeapon.weaponTypes == Weapons.WeaponTypes.DoubleGun || CurrentWeapon.weaponTypes == Weapons.WeaponTypes.HeavyArms)
        {

        }
        Attackable = true;
        
    }



    public IEnumerator ReroldOn()
    {
        yield return new WaitForSeconds(CurrentWeapon.RelordSpeed);
        Enemy.animator.SetBool("isRelord", false);
        BulletCount -= CurrentWeapon.MaxRound;
        if (CurrentWeapon.Round > CurrentWeapon.MaxRound)
        {
            CurrentWeapon.Round = CurrentWeapon.MaxRound;
        }
        else
        {
            CurrentWeapon.Round = BulletCount;
        }
        Reloading = false;
        StartCoroutine(AttackableOn());

    }


    public void SwhichableOn()
    {
        Switchable = true;
    }

    public void SwhichWeapon(Item _weapon)
    {
        //PreviousWeapon = transform.GetChild(2).gameObject;
        //Destroy(PreviousWeapon);
        GameObject.Instantiate(_weapon.itemPrefab, transform.position, Quaternion.identity).transform.parent = this.transform;
        Switchable = false;
        Invoke("SwhichableOn", SwitchDelay);
        Enemy.animator.SetBool("isWalk", true);
        Enemy.animator.SetBool("isFist", false);
        Enemy.animator.SetBool("isOnehandGun", false);
        Enemy.animator.SetBool("isOnehand", false);
        Enemy.animator.SetBool("isTwohandGun", false);
        //QuickSlotWeapons[0].SetActive(false);
        //QuickSlotWeapons[1].SetActive(false);
        //QuickSlotWeapons[2].SetActive(false);
        CurrentWeapon = FindObjectOfType<Weapons>();
        GunBulletCheck();
    }
}
