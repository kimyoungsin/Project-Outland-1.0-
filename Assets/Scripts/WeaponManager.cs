using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WeaponManager : MonoBehaviour
{
    public float SwitchDelay = 1f; //무기 교체 딜레이
    public bool Switchable = true; // 무기스왑가능 여부 true면 가능
    public GameObject[] QuickSlotWeapons;
    //public GameObject CurrentWeapon; // 현재 들고있는 무기 오브젝트
    //public Transform WeaponPosition; //현재 들고있는 무기의 위치

    public UIText UItext; //무기 교체시 UI 변경용
    public PlayerMovement_FSM FSM; //PlayerMovementFSM 스크립트 참조용
    public GameObject PreviousWeapon; //이전에 들고있던 무기(교체 시 삭제용)
    public Weapons CurrentWeapon; //현재 들고있는 무기의 Weapons 스크립트 참조용
    public Weapons Fist; //아무런 무기도 안 들고 있을 시 주먹으로 변경용
    public Transform FirePos; // 투사체 발사 위치
    public Transform MeleePos; //근접무기 히트박스 위치
    public Vector2 BoxSize; // 근접무기 히트박스
    public Inventory theInventory;
    public bool Attackable = true; // 공격 가능여부)
    public bool Reloading = false; //true면 리로드 중
    public bool AtkStop = false; //대화, 이벤트 등 발생시 움직임 멈춤

    public CinemachineVirtualCamera vcam; //카메라
    public float CamFov;



    void Start()
    {
        //Weapon = FindObjectOfType<Weapons>();
        theInventory = FindObjectOfType<Inventory>();
        //theSlot = FindObjectOfType<Slot>();
        FSM = FindObjectOfType<PlayerMovement_FSM>();
        UItext = FindObjectOfType<UIText>();
        vcam = FindObjectOfType<CinemachineVirtualCamera>();
        CamFov = vcam.m_Lens.OrthographicSize;
        GunBulletCheck();
    }


    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rototion = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rototion;

        if (AtkStop == false)
        {
            if (FSM.WeaponShow == true)
            {
                if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
                {
                    FSM.Anim.SetFloat("MoveX", direction.x);
                    FSM.Anim.SetFloat("MoveY", direction.y);
                }
                else
                {
                    FSM.Anim.SetFloat("LastMoveX", direction.x);
                    FSM.Anim.SetFloat("LastMoveY", direction.y);
                }

            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 MousePos = Input.mousePosition;
                Debug.Log(MousePos);
                FSM.Anim.SetBool("isWalk", false);
                if (FSM.WeaponShow == false)
                { 
                    if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Fist)
                    {
                        FSM.Anim.SetBool("isFist", true);
                    }
                    else if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.OnehandGun)
                    {
                        FSM.Anim.SetBool("isOnehandGun", true);
                    }
                    else if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Onehand)
                    {
                        FSM.Anim.SetBool("isOnehandGun", true);
                    }
                    else if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.TwohandGun)
                    {
                        FSM.Anim.SetBool("isTwohandGun", true);
                    }
                    FSM.WeaponShow = true;
                }
                else
                {

                    if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Fist)
                    {
                        FSM.Anim.SetBool("isFist", true);
                        if (Attackable == true)
                        {
                            Attackable = false;
                            Invoke("AttackableOn", CurrentWeapon.AttackSpeed);
                            StartCoroutine(UnarmdAttack(direction.x, direction.y));

                        }
                    }
                    else if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.OnehandGun)
                    {
                        CurrentWeapon.BulletSetting(); //weapon의 BulletSetting로 발사하는 탄환의 대미지, 탄속 등 설정
                        FSM.Anim.SetBool("isOnehandGun", true);
                        if (Attackable == true)
                        {
                            if (CurrentWeapon.Round > 0)
                            {
                                SoundManager.SharedInstance.PlaySE(CurrentWeapon.Weapon_Atk_Sound);
                                CurrentWeapon.Round -= 1;
                                Attackable = false;
                                Invoke("AttackableOn", CurrentWeapon.AttackSpeed);
                                Instantiate(CurrentWeapon.BulletPrefab, FirePos.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
                            }

                        }
                    }
                    else if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Onehand)
                    {
                        CurrentWeapon.BulletSetting(); //weapon의 BulletSetting로 발사하는 탄환의 대미지, 탄속 등 설정
                        FSM.Anim.SetBool("isOnehand", true);
                        if (Attackable == true)
                        {
                            Attackable = false;
                            Invoke("AttackableOn", CurrentWeapon.AttackSpeed);

                        }
                    }
                    else if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.TwohandGun)
                    {
                        CurrentWeapon.BulletSetting(); //weapon의 BulletSetting로 발사하는 탄환의 대미지, 탄속 등 설정
                        FSM.Anim.SetBool("isTwohandGun", true);
                        if (Attackable == true)
                        {
                            if (CurrentWeapon.Round > 0)
                            {
                                SoundManager.SharedInstance.PlaySE(CurrentWeapon.Weapon_Atk_Sound);
                                CurrentWeapon.Round -= 1;
                                Attackable = false;
                                Invoke("AttackableOn", CurrentWeapon.AttackSpeed);
                                Instantiate(CurrentWeapon.BulletPrefab, FirePos.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
                            }

                        }
                    }
                }
            }

            if(Input.GetMouseButton(1))
            {
                if(FSM.WeaponShow == true)
                {
                    if(CurrentWeapon.weaponTypes == Weapons.WeaponTypes.OnehandGun)
                    {
                        if(vcam.m_Lens.OrthographicSize < 6)
                        {
                            vcam.m_Lens.OrthographicSize += 0.1f;
                        }
                        
                    }
                    if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.TwohandGun)
                    {
                        if (vcam.m_Lens.OrthographicSize < 8)
                        {
                            vcam.m_Lens.OrthographicSize += 0.1f;
                        }
                    }
                }
            }
            if (Input.GetMouseButtonUp(1) || FSM.WeaponShow == false)
            {
                vcam.m_Lens.OrthographicSize = 5;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Onehand)
                {

                }
                else
                {
                    if (CurrentWeapon.Round < CurrentWeapon.MaxRound)
                    {
                        FSM.WeaponShow = true;
                        SoundManager.SharedInstance.PlaySE(CurrentWeapon.Weapon_Reload_Sound);
                        FSM.Anim.SetBool("isRelord", true);
                        Attackable = false;
                        Reloading = true;
                        Invoke("ReroldOn", CurrentWeapon.RelordSpeed);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                if (FSM.WeaponShow == false)
                {
                    FSM.WeaponShow = true;
                    FSM.Anim.SetBool("isWalk", false);

                    if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Fist)
                    {
                        FSM.Anim.SetBool("isFist", true);
                    }
                    else if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.OnehandGun)
                    {
                        FSM.Anim.SetBool("isOnehandGun", true);
                    }
                    else if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Onehand)
                    {
                        FSM.Anim.SetBool("isOnehand", true);
                    }
                    else if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.TwohandGun)
                    {
                        FSM.Anim.SetBool("isTwohandGun", true);
                    }
                }
                else
                {
                    FSM.WeaponShow = false;
                    FSM.Anim.SetBool("isWalk", true);
                    FSM.Anim.SetBool("isFist", false);
                    FSM.Anim.SetBool("isOnehandGun", false);
                    FSM.Anim.SetBool("isOnehand", false);
                    FSM.Anim.SetBool("isTwohandGun", false);
                }
            }
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(MeleePos.position, BoxSize);
    }


    IEnumerator UnarmdAttack(float dir_X, float dir_y)
    {

        //MeleePos.transform.position = new Vector3(dir_X, dir_y, 0);
        FSM.ChangeState(PlayerMovement_FSM.State.MELEE);
        FSM.Anim.SetBool("isFistAtk", true);
        SoundManager.SharedInstance.PlaySE(CurrentWeapon.Weapon_Atk_Sound);
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(MeleePos.position, BoxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider is BoxCollider2D)
            {
                if (collider.tag == "Enemy")
                {
                    Enemy enemy = collider.gameObject.GetComponent<Enemy>();
                    enemy.MeleeHit(CurrentWeapon.Damage, MeleePos.transform);
                }
            }

        }
        yield return null;
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
            //맨 아래 ReroldOn 이 실행중일때 아래 실행 못하게 막아야 함 if문으로
            if (Reloading == false)
            {
                theInventory.CheckBullet(CurrentWeapon.BulletType);
                CurrentWeapon.Round = theInventory.BulletCountReturn(CurrentWeapon.MaxRound);

                Debug.Log(CurrentWeapon.Round);
            }



        }
        if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.TwohandGun)
        {
            if (Reloading == false)
            {
                theInventory.CheckBullet(CurrentWeapon.BulletType);
                CurrentWeapon.Round = theInventory.BulletCountReturn(CurrentWeapon.MaxRound);

                Debug.Log(CurrentWeapon.Round);
            }
        }
    }

    public void StopAtk()
    {
        if (AtkStop == true)
        {
            AtkStop = false;
        }
        else if (AtkStop == false)
        {
            AtkStop = true;
        }

    }

    void AttackableOn()
    {
        Attackable = true;
        FSM.Anim.SetBool("isFistAtk", false);
        if (FSM.Anim.GetBool("isWalk") == true)
        {
            FSM.ChangeState(PlayerMovement_FSM.State.WALK);
            
        }
        else
        {
            FSM.ChangeState(PlayerMovement_FSM.State.STAND);

        }


    }

    public void ReroldOn()
    {
        FSM.Anim.SetBool("isRelord", false);
        CurrentWeapon.UseRound = CurrentWeapon.Round - CurrentWeapon.MaxRound;
        theInventory.CheckBullet(CurrentWeapon.BulletType);
        CurrentWeapon.Round += theInventory.BulletCountReturn(CurrentWeapon.MaxRound);
        if (CurrentWeapon.Round > CurrentWeapon.MaxRound)
        {
            CurrentWeapon.Round = CurrentWeapon.MaxRound;
        }
        theInventory.UseBullet(CurrentWeapon.BulletType, CurrentWeapon.UseRound);
        Reloading = false;
        AttackableOn();
    }


    void SwhichableOn()
    {
        Switchable = true;
    }

    public void SwhichWeapon(Item _weapon)
    {
        PreviousWeapon = transform.GetChild(2).gameObject;
        Destroy(PreviousWeapon);
        GameObject.Instantiate(_weapon.itemPrefab, transform.position, Quaternion.identity).transform.parent = this.transform;
        Switchable = false;
        Invoke("SwhichableOn", SwitchDelay);
        FSM.Anim.SetBool("isWalk", true);
        FSM.Anim.SetBool("isFist", false);
        FSM.Anim.SetBool("isOnehandGun", false);
        FSM.Anim.SetBool("isOnehand", false);
        FSM.Anim.SetBool("isTwohandGun", false);
        //QuickSlotWeapons[0].SetActive(false);
        //QuickSlotWeapons[1].SetActive(false);
        //QuickSlotWeapons[2].SetActive(false);
        CurrentWeapon = FindObjectOfType<Weapons>();
        UItext.WeaponChange(CurrentWeapon);
        GunBulletCheck();

        if (_weapon.itemName != CurrentWeapon.Name)
        {

        }
        else
        {

        }
        

    }
}
