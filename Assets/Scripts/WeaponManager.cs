using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WeaponManager : MonoBehaviour
{
    public float SwitchDelay = 1f; //���� ��ü ������
    public bool Switchable = true; // ���⽺�Ұ��� ���� true�� ����
    public GameObject[] QuickSlotWeapons;
    //public GameObject CurrentWeapon; // ���� ����ִ� ���� ������Ʈ
    //public Transform WeaponPosition; //���� ����ִ� ������ ��ġ

    public UIText UItext; //���� ��ü�� UI �����
    public PlayerMovement_FSM FSM; //PlayerMovementFSM ��ũ��Ʈ ������
    public GameObject PreviousWeapon; //������ ����ִ� ����(��ü �� ������)
    public Weapons CurrentWeapon; //���� ����ִ� ������ Weapons ��ũ��Ʈ ������
    public Weapons Fist; //�ƹ��� ���⵵ �� ��� ���� �� �ָ����� �����
    public Transform FirePos; // ����ü �߻� ��ġ
    public Transform MeleePos; //�������� ��Ʈ�ڽ� ��ġ
    public Vector2 BoxSize; // �������� ��Ʈ�ڽ�
    public Inventory theInventory;
    public bool Attackable = true; // ���� ���ɿ���)
    public bool Reloading = false; //true�� ���ε� ��
    public bool AtkStop = false; //��ȭ, �̺�Ʈ �� �߻��� ������ ����

    public CinemachineVirtualCamera vcam; //ī�޶�
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
                        CurrentWeapon.BulletSetting(); //weapon�� BulletSetting�� �߻��ϴ� źȯ�� �����, ź�� �� ����
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
                        CurrentWeapon.BulletSetting(); //weapon�� BulletSetting�� �߻��ϴ� źȯ�� �����, ź�� �� ����
                        FSM.Anim.SetBool("isOnehand", true);
                        if (Attackable == true)
                        {
                            Attackable = false;
                            Invoke("AttackableOn", CurrentWeapon.AttackSpeed);

                        }
                    }
                    else if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.TwohandGun)
                    {
                        CurrentWeapon.BulletSetting(); //weapon�� BulletSetting�� �߻��ϴ� źȯ�� �����, ź�� �� ����
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

    public void GunBulletCheck() //���� ������ �κ��� �ִ� źȯ�� �°� ����
    {
        if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Fist)
        {

        }
        if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Onehand)
        {

        }
        if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.OnehandGun)
        {
            //�� �Ʒ� ReroldOn �� �������϶� �Ʒ� ���� ���ϰ� ���ƾ� �� if������
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
