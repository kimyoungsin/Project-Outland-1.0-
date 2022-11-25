using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public string Name; //무기 이름
    public float AttackSpeed; // 공격 딜레이
    public int Damage; // 무기 대미지
    public float Critical; // 무기 치명타확률
    public float CriticalDamage; // 무기 치명타 피해 계수
    public float Range; // 무기 유효 사거리
    public float BulletSpeed; //탄속
    public float RelordSpeed; // 총기 재장전속도
    public Item BulletType; // 탄종(장착시 인벤토리에서 해당 탄약 확인)
    public int MaxRound; // 최대 탄창수
    public int Round; // 현재 탄창수
    public float Spread; // 산탄도
    public float Weight; // 무게
    public bool Attackable = true; // 공격 가능여부)
    public AudioSource WeaponSound; //무기 사운드
    public AudioClip AtkSound; //공격 사운드
    public AudioClip RelordSound; //재장전 사운드
    public bool Reloading = false;
    
    public enum WeaponTypes
    {
        Fist,
        Unarmd,
        Onehand,
        Twohand,
        OnehandGun,
        TwohandGun,
        DoubleGun,
        Gun_Tlet,
        HeavyArms

    }
    public WeaponTypes weaponTypes;

    public Sprite WeaponSprite; //무기 이미지 스프라이트
    public GameObject BulletPrefab; // 생성할 총알
    public Transform FirePos; // 투사체 생성 위치
    public Bullet bullet;
    public PlayerMovement PlayerMovement;
    public Inventory theInventory;
    public Slot theSlot;
    public bool AtkStop = false; //대화, 이벤트 등 발생시 움직임 멈춤


    // Start is called before the first frame update
    void Start()
    {
        WeaponSound = GetComponent<AudioSource>();
        theInventory = FindObjectOfType<Inventory>();
        theSlot = FindObjectOfType<Slot>();
        PlayerMovement = FindObjectOfType<PlayerMovement>();
        GunBulletCheck();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rototion = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rototion;
        //Debug.Log(direction);
        
        //MaxRoundSet();

        if (AtkStop == false)
        {
            if (PlayerMovement.WeaponShow == true)
            {
                if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
                {
                    PlayerMovement.Anim.SetFloat("MoveX", direction.x);
                    PlayerMovement.Anim.SetFloat("MoveY", direction.y);
                }
                else
                {
                    PlayerMovement.Anim.SetFloat("LastMoveX", direction.x);
                    PlayerMovement.Anim.SetFloat("LastMoveY", direction.y);
                }

            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 MousePos = Input.mousePosition;
                Debug.Log(MousePos);
                if (PlayerMovement.WeaponShow == false)
                {
                    PlayerMovement.Anim.SetBool("isWalk", false);
                    if (weaponTypes == WeaponTypes.Fist)
                    {
                        PlayerMovement.Anim.SetBool("isFist", true);
                    }
                    else if (weaponTypes == WeaponTypes.OnehandGun)
                    {
                        PlayerMovement.Anim.SetBool("isOnehandGun", true);
                    }
                    else if (weaponTypes == WeaponTypes.Onehand)
                    {
                        PlayerMovement.Anim.SetBool("isOnehandGun", true);
                    }
                    else if (weaponTypes == WeaponTypes.TwohandGun)
                    {
                        PlayerMovement.Anim.SetBool("isTwohandGun", true);
                    }
                    PlayerMovement.WeaponShow = true;
                }
                else
                {
                    PlayerMovement.Anim.SetBool("isWalk", false);

                    bullet.BulletSpeed = BulletSpeed;
                    bullet.BulletDamage = Damage;
                    if (weaponTypes == WeaponTypes.Fist)
                    {
                        PlayerMovement.Anim.SetBool("isFist", true);
                        if (Attackable == true)
                        {
                            Attackable = false;
                            Invoke("AttackableOn", AttackSpeed);
                            //Instantiate(BulletPrefab, FirePos.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));

                        }
                    }
                    else if (weaponTypes == WeaponTypes.OnehandGun)
                    {
                        PlayerMovement.Anim.SetBool("isOnehandGun", true);
                        if (Attackable == true)
                        {
                            if (Round > 0)
                            {
                                WeaponSound.PlayOneShot(AtkSound);
                                Round -= 1;
                                //theInventory.UseBullet(BulletType, -1);
                                Attackable = false;
                                Invoke("AttackableOn", AttackSpeed);
                                Instantiate(BulletPrefab, FirePos.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
                            }

                        }
                    }
                    else if (weaponTypes == WeaponTypes.Onehand)
                    {
                        PlayerMovement.Anim.SetBool("isOnehand", true);
                        if (Attackable == true)
                        {
                            Attackable = false;
                            Invoke("AttackableOn", AttackSpeed);
                            //Instantiate(BulletPrefab, FirePos.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
                        }
                    }
                    else if (weaponTypes == WeaponTypes.TwohandGun)
                    {
                        PlayerMovement.Anim.SetBool("isTwohandGun", true);
                        if (Attackable == true)
                        {
                            if (Round > 0)
                            {
                                WeaponSound.PlayOneShot(AtkSound);
                                Round -= 1;
                                //theInventory.UseBullet(BulletType, -1);
                                Attackable = false;
                                Invoke("AttackableOn", AttackSpeed);
                                Instantiate(BulletPrefab, FirePos.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
                            }
                        }
                    }
                }


            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (weaponTypes == WeaponTypes.Onehand)
                {

                }
                else
                {
                    if (Round < MaxRound)
                    {
                        WeaponSound.PlayOneShot(RelordSound);
                        Attackable = false;
                        Reloading = true;
                        Invoke("ReroldOn", RelordSpeed);
                        Invoke("AttackableOn", RelordSpeed);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                if (PlayerMovement.WeaponShow == false)
                {
                    PlayerMovement.WeaponShow = true;
                    PlayerMovement.Anim.SetBool("isWalk", false);

                    if (weaponTypes == WeaponTypes.Fist)
                    {
                        PlayerMovement.Anim.SetBool("isFist", true);
                    }
                    else if (weaponTypes == WeaponTypes.OnehandGun)
                    {
                        PlayerMovement.Anim.SetBool("isOnehandGun", true);
                    }
                    else if (weaponTypes == WeaponTypes.Onehand)
                    {
                        PlayerMovement.Anim.SetBool("isOnehand", true);
                    }
                    else if (weaponTypes == WeaponTypes.TwohandGun)
                    {
                        PlayerMovement.Anim.SetBool("isTwohandGun", true);
                    }
                    PlayerMovement.WeaponShow = true;
                }
                else
                {
                    PlayerMovement.WeaponShow = false;
                    PlayerMovement.Anim.SetBool("isWalk", true);
                    PlayerMovement.Anim.SetBool("isFist", false);
                    PlayerMovement.Anim.SetBool("isOnehandGun", false);
                    PlayerMovement.Anim.SetBool("isOnehand", false);
                    PlayerMovement.Anim.SetBool("isTwohandGun", false);
                }
            }
        }
        
    }
    public void GunBulletCheck() //무기 장착시 인벤에 있는 탄환에 맞게 장전
    {
        if (weaponTypes == WeaponTypes.Fist)
        {

        }
        if (weaponTypes == WeaponTypes.Onehand)
        {

        }
        if (weaponTypes == WeaponTypes.OnehandGun)
        {
            //맨 아래 ReroldOn 이 실행줄일때 아래 실행 못하게 막아야 함 if문으로
            if(Reloading == false)
            {
                theInventory.CheckBullet(BulletType);
                Round = theInventory.BulletCountReturn(MaxRound);

                Debug.Log(Round);
            }
            


        }
        if (weaponTypes == WeaponTypes.TwohandGun)
        {
            if (Reloading == false)
            {
                theInventory.CheckBullet(BulletType);
                Round = theInventory.BulletCountReturn(MaxRound);

                Debug.Log(Round);
            }
        }
    }
    void GunFire()
    {

    }

    public void MaxRoundSet()
    {
        if (Round > MaxRound)
        {
            Round = MaxRound;
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
    }

    void ReroldOn()
    {
        theInventory.UseBullet(BulletType, Round - MaxRound);
        theInventory.CheckBullet(BulletType);
        Round = theInventory.BulletCountReturn(MaxRound);
        Reloading = false;
    }
}
