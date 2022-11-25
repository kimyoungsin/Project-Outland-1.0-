using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public string Name; //���� �̸�
    public float AttackSpeed; // ���� ������
    public int Damage; // ���� �����
    public float Critical; // ���� ġ��ŸȮ��
    public float CriticalDamage; // ���� ġ��Ÿ ���� ���
    public float Range; // ���� ��ȿ ��Ÿ�
    public float BulletSpeed; //ź��
    public float RelordSpeed; // �ѱ� �������ӵ�
    public Item BulletType; // ź��(������ �κ��丮���� �ش� ź�� Ȯ��)
    public int MaxRound; // �ִ� źâ��
    public int Round; // ���� źâ��
    public float Spread; // ��ź��
    public float Weight; // ����
    public bool Attackable = true; // ���� ���ɿ���)
    public AudioSource WeaponSound; //���� ����
    public AudioClip AtkSound; //���� ����
    public AudioClip RelordSound; //������ ����
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
