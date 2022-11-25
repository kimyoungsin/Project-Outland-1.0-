using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public AudioSource WeaponSound; //무기 사운드
    public AudioClip AtkSound; //공격 사운드
    public AudioClip RelordSound; //재장전 사운드
    public bool Reloading = false;

    public bool Attackable = true; // 공격 가능여부)
    public Weapons weaponScript;

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
        weaponScript = FindObjectOfType<Weapons>();
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

                    bullet.BulletSpeed = weaponScript.BulletSpeed;
                    bullet.BulletDamage = weaponScript.Damage;
                    if (weaponTypes == WeaponTypes.Fist)
                    {
                        PlayerMovement.Anim.SetBool("isFist", true);
                        if (Attackable == true)
                        {
                            Attackable = false;
                            Invoke("AttackableOn", weaponScript.AttackSpeed);
                            //Instantiate(BulletPrefab, FirePos.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));

                        }
                    }
                    else if (weaponTypes == WeaponTypes.OnehandGun)
                    {
                        PlayerMovement.Anim.SetBool("isOnehandGun", true);
                        if (Attackable == true)
                        {
                            if (weaponScript.Round > 0)
                            {
                                WeaponSound.PlayOneShot(AtkSound);
                                weaponScript.Round -= 1;
                                //theInventory.UseBullet(BulletType, -1);
                                Attackable = false;
                                Invoke("AttackableOn", weaponScript.AttackSpeed);
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
                            Invoke("AttackableOn", weaponScript.AttackSpeed);
                            //Instantiate(BulletPrefab, FirePos.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
                        }
                    }
                    else if (weaponTypes == WeaponTypes.TwohandGun)
                    {
                        PlayerMovement.Anim.SetBool("isTwohandGun", true);
                        if (Attackable == true)
                        {
                            if (weaponScript.Round > 0)
                            {
                                WeaponSound.PlayOneShot(AtkSound);
                                weaponScript.Round -= 1;
                                //theInventory.UseBullet(BulletType, -1);
                                Attackable = false;
                                Invoke("AttackableOn", weaponScript.AttackSpeed);
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
                    if (weaponScript.Round < weaponScript.MaxRound)
                    {
                        WeaponSound.PlayOneShot(RelordSound);
                        Attackable = false;
                        Reloading = true;
                        Invoke("ReroldOn", weaponScript.RelordSpeed);
                        Invoke("AttackableOn", weaponScript.RelordSpeed);
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
            if (Reloading == false)
            {
                theInventory.CheckBullet(weaponScript.BulletType);
                weaponScript.Round = theInventory.BulletCountReturn(weaponScript.MaxRound);

                Debug.Log(weaponScript.Round);
            }



        }
        if (weaponTypes == WeaponTypes.TwohandGun)
        {
            if (Reloading == false)
            {
                theInventory.CheckBullet(weaponScript.BulletType);
                weaponScript.Round = theInventory.BulletCountReturn(weaponScript.MaxRound);

                Debug.Log(weaponScript.Round);
            }
        }
    }
    void GunFire()
    {

    }

    public void MaxRoundSet()
    {
        if (weaponScript.Round > weaponScript.MaxRound)
        {
            weaponScript.Round = weaponScript.MaxRound;
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
        theInventory.UseBullet(weaponScript.BulletType, weaponScript.Round - weaponScript.MaxRound);
        theInventory.CheckBullet(weaponScript.BulletType);
        weaponScript.Round = theInventory.BulletCountReturn(weaponScript.MaxRound);
        Reloading = false;
    }
}
