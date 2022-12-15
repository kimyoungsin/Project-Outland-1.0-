using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public float SwitchDelay = 1f; //���� ��ü ������
    public bool Switchable = true; // ���⽺�Ұ��� ���� true�� ����
    public GameObject[] QuickSlotWeapons;

    private string currentWeaponName; //���� ������ �̸�
    public GameObject CurrentWeapon; // ���� ����ִ� ����
    public Transform WeaponPosition; //���� ����ִ� ������ ��ġ

    public PlayerMovement PlayerMovement; //PlayerMovement ��ũ��Ʈ ������
    public Weapons Weapon; //Weapons ��ũ��Ʈ ������
    public Transform FirePos; // ����ü �߻� ��ġ
    public Inventory theInventory;
    //public Slot theSlot;
    public bool Attackable = true; // ���� ���ɿ���)
    public bool Reloading = false; //true�� ���ε� ��
    public bool AtkStop = false; //��ȭ, �̺�Ʈ �� �߻��� ������ ����

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
                        Weapon.BulletSetting(); //weapon�� BulletSetting�� �߻��ϴ� źȯ�� �����, ź�� �� ����
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
                        Weapon.BulletSetting(); //weapon�� BulletSetting�� �߻��ϴ� źȯ�� �����, ź�� �� ����
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

    public void GunBulletCheck() //���� ������ �κ��� �ִ� źȯ�� �°� ����
    {
        if (Weapon.weaponTypes == Weapons.WeaponTypes.Fist)
        {

        }
        if (Weapon.weaponTypes == Weapons.WeaponTypes.Onehand)
        {

        }
        if (Weapon.weaponTypes == Weapons.WeaponTypes.OnehandGun)
        {
            //�� �Ʒ� ReroldOn �� �������϶� �Ʒ� ���� ���ϰ� ���ƾ� �� if������
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
