using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public AudioSource WeaponSound; //���� ����
    public AudioClip AtkSound; //���� ����
    public AudioClip RelordSound; //������ ����
    public bool Reloading = false;

    public bool Attackable = true; // ���� ���ɿ���)
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

    public Sprite WeaponSprite; //���� �̹��� ��������Ʈ
    public GameObject BulletPrefab; // ������ �Ѿ�
    public Transform FirePos; // ����ü ���� ��ġ
    public Bullet bullet;
    public PlayerMovement PlayerMovement;
    public Inventory theInventory;
    public Slot theSlot;
    public bool AtkStop = false; //��ȭ, �̺�Ʈ �� �߻��� ������ ����


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
    public void GunBulletCheck() //���� ������ �κ��� �ִ� źȯ�� �°� ����
    {
        if (weaponTypes == WeaponTypes.Fist)
        {

        }
        if (weaponTypes == WeaponTypes.Onehand)
        {

        }
        if (weaponTypes == WeaponTypes.OnehandGun)
        {
            //�� �Ʒ� ReroldOn �� �������϶� �Ʒ� ���� ���ϰ� ���ƾ� �� if������
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
