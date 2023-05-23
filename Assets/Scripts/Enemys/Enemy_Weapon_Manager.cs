using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Weapon_Manager : MonoBehaviour
{
    public float SwitchDelay = 1f; //���� ��ü ������
    public bool Switchable = true; // ���⽺�Ұ��� ���� true�� ����
    public Enemy enemy; //Enemy ��ũ��Ʈ ������
    public GameObject CurrentWeaponInstance; //�� ������ ���� ���� �ν��Ͻ� ������
    public Weapons CurrentWeapon; //���� ����ִ� ������ Weapons ��ũ��Ʈ ������
    public Weapons FistInstance; //�ָ��ν��Ͻ� ������
    public Weapons Fist; //�ƹ��� ���⵵ �� ��� �ְų� �ѱ� ź�� ���� �Ҹ�� �ָ����� �����

    public Transform FirePos; // ����ü �߻� ��ġ
    public Transform MeleePos; //�������� ��Ʈ�ڽ� ��ġ
    public Transform PlayerPos; //�÷��̾� ��ġ(��� ��)
    public Vector2 BoxSize; // �������� ��Ʈ�ڽ�
    public bool Attackable = true; // ���� ���ɿ���)
    public bool Reloading = false; //true�� ���ε� ��
    public bool isAtk = false; //�������� �� ����
    public int BulletCount; //�� �⺻ �Ѿ� ��������
    public Enemy_Weapon_Atk EnemyAtk;

    public int rand; // 0: ����, 1: �̵�

    void Start()
    {
        rand = Random.Range(0, 1); // 0: ����, 1: �̵�
        GameObject.Instantiate(CurrentWeaponInstance, transform.position, Quaternion.identity).transform.parent = this.transform;
        CurrentWeapon = FindObjectOfType<Weapons>();
        BulletCount = CurrentWeapon.MaxRound * 2;
        GunBulletCheck();
    }


    void Update()
    {
        
        
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(MeleePos.position, BoxSize);
    }

    public IEnumerator AttackAI()
    {
        
        yield return new WaitForSeconds(0.25f);
        Debug.Log("�� AI����");
        if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Unarmd || CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Onehand || CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Twohand || CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Fist)
        {
            StartCoroutine(UnarmdAttack(EnemyAtk.direction.x, EnemyAtk.direction.y));
            Debug.Log("��ø");
            Debug.Log("����ִ� ���� Ÿ��: " + CurrentWeapon.weaponTypes);
            rand = Random.Range(0, 1); // 0: ����, 1: �̵�
            Debug.Log("rand�������: " + rand);
            if (rand == 0)
            {
                Debug.Log("�� ����");
                StartCoroutine(UnarmdAttack(EnemyAtk.direction.x, EnemyAtk.direction.y));
            }
            else if (rand == 1)
            {
                Debug.Log("�� �̵�");
                StartCoroutine(Move());
            }

        }
        else if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.OnehandGun || CurrentWeapon.weaponTypes == Weapons.WeaponTypes.TwohandGun || CurrentWeapon.weaponTypes == Weapons.WeaponTypes.DoubleGun || CurrentWeapon.weaponTypes == Weapons.WeaponTypes.HeavyArms)
        {
            StartCoroutine(GunFire(PlayerPos));
            Debug.Log("���Ÿ�");
            Debug.Log("����ִ� ���� Ÿ��: " + CurrentWeapon.weaponTypes);
            rand = Random.Range(0, 2); // 0: ����, 1: �̵�
            Debug.Log("rand�������: " + rand);
            if (rand == 0)
            {
                StartCoroutine(GunFire(PlayerPos));
            }
            else if (rand == 1)
            {
                StartCoroutine(Move());
            }

        }

        
    }

    public IEnumerator Move()
    {
        yield return new WaitForSeconds(1f);
        EnemyAtk.isMove = false;
        Debug.Log("�� �̵���");

        yield return new WaitForSeconds(1f);
        EnemyAtk.isMove = true;
        StartCoroutine(AttackAI());
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
                        enemy.animator.SetBool("isFistAtk", true);
                        Player player = collider.gameObject.GetComponent<Player>();
                        player.Hit(CurrentWeapon.Damage);
                        
                    }
                }

            }
        }


        StartCoroutine(AttackAI());
    }

    public IEnumerator GunFire(Transform TargetPos) //�� �� �߻�
    {
        yield return new WaitForSeconds(CurrentWeapon.AttackSpeed);
        if (Attackable == true)
        {
            if (CurrentWeapon.Round > 0)
            {
                Vector2 direction = PlayerPos.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rototion = Quaternion.AngleAxis(angle-90, Vector3.forward);
                transform.rotation = rototion;

                enemy.animator.SetBool("isOnehandGun", true);
                SoundManager.SharedInstance.PlaySE(CurrentWeapon.Weapon_Atk_Sound);
                CurrentWeapon.Round -= 1;
                Debug.Log("�� ���� �Ѿ�: " + CurrentWeapon.Round);
                Attackable = false;
                StartCoroutine(AttackableOn());
                Instantiate(CurrentWeapon.enemyBulletPrefab, FirePos.position, Quaternion.AngleAxis(angle - 90, Vector3.forward));
                Debug.Log("�� ���");
                StartCoroutine(AttackAI());
            }
            else if (CurrentWeapon.Round <= 0) //�Ѿ� �� ���� ������
            {
                if (BulletCount > 0) //�Ѿ��� ���� ���
                {
                    StopCoroutine(GunFire(PlayerPos));
                    SoundManager.SharedInstance.PlaySE(CurrentWeapon.Weapon_Reload_Sound);
                    enemy.animator.SetBool("isRelord", true);
                    Attackable = false;
                    Reloading = true;
                    StartCoroutine(ReroldOn());
                    Debug.Log("�� ������");
                }
                else if (BulletCount <= 0)
                {
                    //�Ѿ� �� �������� �ָ����� ����
                    enemy.animator.SetBool("isOnehandGun", false);
                    EnemyAtk.isMove = true;
                    Debug.Log("�� �ָ����� ���� ����");
                    GameObject.Instantiate(FistInstance, transform.position, Quaternion.identity).transform.parent = this.transform;
                    CurrentWeapon = FindObjectOfType<Weapons>();
                    StartCoroutine(AttackAI());
                }
            }

        }
        
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
            CurrentWeapon.EnemyBulletSetting(); //weapon�� BulletSetting�� �߻��ϴ� źȯ�� �����, ź�� �� ����



        }
        if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.TwohandGun)
        {
            CurrentWeapon.EnemyBulletSetting(); //weapon�� BulletSetting�� �߻��ϴ� źȯ�� �����, ź�� �� ����
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
            enemy.animator.SetBool("isFistAtk", false);

        }
        if (CurrentWeapon.weaponTypes == Weapons.WeaponTypes.OnehandGun || CurrentWeapon.weaponTypes == Weapons.WeaponTypes.TwohandGun || CurrentWeapon.weaponTypes == Weapons.WeaponTypes.DoubleGun || CurrentWeapon.weaponTypes == Weapons.WeaponTypes.HeavyArms)
        {

        }
        Attackable = true;
        
    }



    public IEnumerator ReroldOn()
    {
        yield return new WaitForSeconds(CurrentWeapon.RelordSpeed);
        enemy.animator.SetBool("isRelord", false);
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
        StartCoroutine(AttackAI());

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
        enemy.animator.SetBool("isWalk", true);
        enemy.animator.SetBool("isFist", false);
        enemy.animator.SetBool("isOnehandGun", false);
        enemy.animator.SetBool("isOnehand", false);
        enemy.animator.SetBool("isTwohandGun", false);
        //QuickSlotWeapons[0].SetActive(false);
        //QuickSlotWeapons[1].SetActive(false);
        //QuickSlotWeapons[2].SetActive(false);
        CurrentWeapon = FindObjectOfType<Weapons>();
        GunBulletCheck();
    }
}
