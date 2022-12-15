using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public float SwitchDelay = 1f; //무기 교체 딜레이
    public bool Switchable = true; // 무기스왑가능 여부 true면 가능
    public GameObject[] QuickSlotWeapons;

    private string currentWeaponName; //현재 무기의 이름
    public GameObject CurrentWeapon; // 현재 들고있는 무기
    public Transform WeaponPosition; //현재 들고있는 무기의 위치

    public PlayerMovement PlayerMovement; //PlayerMovement 스크립트 참조용
    public Weapons Weapon; //Weapons 스크립트 참조용
    public Transform FirePos; // 투사체 발사 위치
    public Inventory theInventory;
    //public Slot theSlot;
    public bool Attackable = true; // 공격 가능여부)
    public bool Reloading = false; //true면 리로드 중
    public bool AtkStop = false; //대화, 이벤트 등 발생시 움직임 멈춤

    // Start is called before the first frame update
    void Start()
    {
        Weapon = FindObjectOfType<Weapons>();
        theInventory = FindObjectOfType<Inventory>();
        //theSlot = FindObjectOfType<Slot>();
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
                PlayerMovement.Anim.SetBool("isWalk", false);
                if (PlayerMovement.WeaponShow == false)
                { 
                    if (Weapon.weaponTypes == Weapons.WeaponTypes.Fist)
                    {
                        PlayerMovement.Anim.SetBool("isFist", true);
                    }
                    else if (Weapon.weaponTypes == Weapons.WeaponTypes.OnehandGun)
                    {
                        PlayerMovement.Anim.SetBool("isOnehandGun", true);
                    }
                    else if (Weapon.weaponTypes == Weapons.WeaponTypes.Onehand)
                    {
                        PlayerMovement.Anim.SetBool("isOnehandGun", true);
                    }
                    else if (Weapon.weaponTypes == Weapons.WeaponTypes.TwohandGun)
                    {
                        PlayerMovement.Anim.SetBool("isTwohandGun", true);
                    }
                    PlayerMovement.WeaponShow = true;
                }
                else
                {

                    if (Weapon.weaponTypes == Weapons.WeaponTypes.Fist)
                    {
                        PlayerMovement.Anim.SetBool("isFist", true);
                        if (Attackable == true)
                        {
                            Attackable = false;
                            Invoke("AttackableOn", Weapon.AttackSpeed);


                        }
                    }
                    else if (Weapon.weaponTypes == Weapons.WeaponTypes.OnehandGun)
                    {
                        Weapon.BulletSetting(); //weapon의 BulletSetting로 발사하는 탄환의 대미지, 탄속 등 설정
                        PlayerMovement.Anim.SetBool("isOnehandGun", true);
                        if (Attackable == true)
                        {
                            if (Weapon.Round > 0)
                            {
                                SoundManager.SharedInstance.PlaySE(Weapon.pistol_shot_sound);
                                Weapon.Round -= 1;
                                Attackable = false;
                                Invoke("AttackableOn", Weapon.AttackSpeed);
                                Instantiate(Weapon.BulletPrefab, FirePos.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
                            }

                        }
                    }
                    else if (Weapon.weaponTypes == Weapons.WeaponTypes.Onehand)
                    {
                        Weapon.BulletSetting(); //weapon의 BulletSetting로 발사하는 탄환의 대미지, 탄속 등 설정
                        PlayerMovement.Anim.SetBool("isOnehand", true);
                        if (Attackable == true)
                        {
                            Attackable = false;
                            Invoke("AttackableOn", Weapon.AttackSpeed);

                        }
                    }
                    else if (Weapon.weaponTypes == Weapons.WeaponTypes.TwohandGun)
                    {
                        PlayerMovement.Anim.SetBool("isTwohandGun", true);
                        if (Attackable == true)
                        {
                            if (Weapon.Round > 0)
                            {
                                Weapon.Round -= 1;
                                Attackable = false;
                                Invoke("AttackableOn", Weapon.AttackSpeed);
                                Instantiate(Weapon.BulletPrefab, FirePos.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
                            }
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (Weapon.weaponTypes == Weapons.WeaponTypes.Onehand)
                {

                }
                else
                {
                    if (Weapon.Round < Weapon.MaxRound)
                    {
                        SoundManager.SharedInstance.PlaySE(Weapon.pistol_reload_sound);
                        PlayerMovement.Anim.SetBool("isRelord", true);
                        Attackable = false;
                        Reloading = true;
                        Invoke("ReroldOn", Weapon.RelordSpeed);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                if (PlayerMovement.WeaponShow == false)
                {
                    PlayerMovement.WeaponShow = true;
                    PlayerMovement.Anim.SetBool("isWalk", false);

                    if (Weapon.weaponTypes == Weapons.WeaponTypes.Fist)
                    {
                        PlayerMovement.Anim.SetBool("isFist", true);
                    }
                    else if (Weapon.weaponTypes == Weapons.WeaponTypes.OnehandGun)
                    {
                        PlayerMovement.Anim.SetBool("isOnehandGun", true);
                    }
                    else if (Weapon.weaponTypes == Weapons.WeaponTypes.Onehand)
                    {
                        PlayerMovement.Anim.SetBool("isOnehand", true);
                    }
                    else if (Weapon.weaponTypes == Weapons.WeaponTypes.TwohandGun)
                    {
                        PlayerMovement.Anim.SetBool("isTwohandGun", true);
                    }
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
        if (Weapon.weaponTypes == Weapons.WeaponTypes.Fist)
        {

        }
        if (Weapon.weaponTypes == Weapons.WeaponTypes.Onehand)
        {

        }
        if (Weapon.weaponTypes == Weapons.WeaponTypes.OnehandGun)
        {
            //맨 아래 ReroldOn 이 실행중일때 아래 실행 못하게 막아야 함 if문으로
            if (Reloading == false)
            {
                theInventory.CheckBullet(Weapon.BulletType);
                Weapon.Round = theInventory.BulletCountReturn(Weapon.MaxRound);

                Debug.Log(Weapon.Round);
            }



        }
        if (Weapon.weaponTypes == Weapons.WeaponTypes.TwohandGun)
        {
            if (Reloading == false)
            {
                theInventory.CheckBullet(Weapon.BulletType);
                Weapon.Round = theInventory.BulletCountReturn(Weapon.MaxRound);

                Debug.Log(Weapon.Round);
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
    }

    public void ReroldOn()
    {
        PlayerMovement.Anim.SetBool("isRelord", false);
        Weapon.UseRound = Weapon.Round - Weapon.MaxRound;
        theInventory.CheckBullet(Weapon.BulletType);
        Weapon.Round += theInventory.BulletCountReturn(Weapon.MaxRound);
        if (Weapon.Round > Weapon.MaxRound)
        {
            Weapon.Round = Weapon.MaxRound;
        }
        theInventory.UseBullet(Weapon.BulletType, Weapon.UseRound);
        Reloading = false;
        AttackableOn();
    }


    void SwhichableOn()
    {
        Switchable = true;
    }

    public void SwhichWeapon(GameObject _weapon)
    {

        Switchable = false;
        Invoke("SwhichableOn", SwitchDelay);
        PlayerMovement.Anim.SetBool("isWalk", true);
        PlayerMovement.Anim.SetBool("isFist", false);
        PlayerMovement.Anim.SetBool("isOnehandGun", false);
        PlayerMovement.Anim.SetBool("isOnehand", false);
        PlayerMovement.Anim.SetBool("isTwohandGun", false);
        QuickSlotWeapons[0].SetActive(false);
        QuickSlotWeapons[1].SetActive(false);
        QuickSlotWeapons[2].SetActive(false);
        Destroy(CurrentWeapon);
        CurrentWeapon = _weapon;
        CurrentWeapon = (GameObject)Instantiate(_weapon, WeaponPosition.transform);
        CurrentWeapon.transform.parent = gameObject.transform;
        Weapon = FindObjectOfType<Weapons>();
        GunBulletCheck();
    }
}
