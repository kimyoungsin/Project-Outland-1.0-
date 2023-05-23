using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Weapon_Atk : MonoBehaviour
{
    public float MovementSpeed; //�⺻�̼�
    public float AlertSpeed; //������ �̼�

    public FieldOfView FOV; //��ü �þ�
    public Vector2 direction; //�̵�����
    public bool Alert = false; //������
    public bool Warning = false; //�߰�=���� ����
    public float Radius; //��Ŭ �ݶ��̴� ������=���� �þ߹���
    public float AlertRadius; //��� �� �þ߹���
    public float WarningRadius; //�߰� �� �þ߹���

    public Transform targetTransform;
    public Rigidbody2D rigid;
    public Animator animator;

    public Enemy_Weapon_Manager weaponmanager;
    public bool isMove = true;
    Vector2 movement = new Vector2(0,0);


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        direction.x = Random.Range(-1.0f, 1.0f);
        direction.y = Random.Range(-1.0f, 1.0f);
    }


    void Update()
    {
        
        if (targetTransform != null)
        {
            if (weaponmanager.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Unarmd || weaponmanager.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Onehand || weaponmanager.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Twohand || weaponmanager.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Fist)
            {
                if (isMove)
                {
                    Vector2 dir = targetTransform.position - transform.position;
                    transform.Translate(dir.normalized * MovementSpeed * Time.deltaTime);
                    animator.SetFloat("MoveX", dir.x);
                    animator.SetFloat("MoveY", dir.y);
                }
            }
            else if(weaponmanager.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.OnehandGun || weaponmanager.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.TwohandGun || weaponmanager.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.DoubleGun || weaponmanager.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.HeavyArms)
            {
                Vector2 dir = targetTransform.position - transform.position;
                transform.Translate(dir.normalized * 0.000001f);
                animator.SetFloat("LastMoveX", dir.x);
                animator.SetFloat("LastMoveY", dir.y);
            }


        }

        if(Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(weaponmanager.AttackAI());
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(weaponmanager.ReroldOn());
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           

            FOV.target = collision.gameObject.transform;
            targetTransform = collision.gameObject.transform;
            Warning = true;

            weaponmanager.PlayerPos = targetTransform;

            if (!enabled) return;

            if (targetTransform != null && weaponmanager.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Unarmd || weaponmanager.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Onehand || weaponmanager.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Twohand || weaponmanager.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Fist)
            {
                isMove = true;
                StartCoroutine(weaponmanager.AttackAI());


            }
            else if (targetTransform != null && weaponmanager.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.OnehandGun || weaponmanager.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.TwohandGun || weaponmanager.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.DoubleGun || weaponmanager.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.HeavyArms)
            {
                StartCoroutine(weaponmanager.AttackAI());


            }

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            


        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isMove = true;
            FOV.target = null;
            Warning = false;
            targetTransform = null;
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", 0);
            GetComponent<CircleCollider2D>().radius = Radius;

            weaponmanager.PlayerPos = null;
        }
    }

    public void WarningOn(Collider2D Target)
    {
        Warning = true;
        //targetTransform = Target.gameObject.transform;
        GetComponent<CircleCollider2D>().radius = WarningRadius;

    }
    /*
    public void WarningOff()
    {
        Warning = false;
        targetTransform = null;
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", 0);
        
    }
    */

    public void StopMove()
    {
        rigid.velocity = Vector2.zero;
        isMove = false;
        animator.SetBool("isWalk", false);
    }

    public void StopChase()
    {
        rigid.velocity = Vector2.zero;
        MovementSpeed = 0f;
        AlertSpeed = 0f;
        targetTransform = null;
        FOV.target = null;
        Warning = false;
        isMove = false;
        animator.SetBool("isChase", false);
        animator.SetBool("isWalk", false);
        animator.SetBool("isFistAtk", false);
    }
    public void Kill()
    {
        Destroy(gameObject);
    }

    public void atk()
    {
        StartCoroutine(weaponmanager.AttackAI());
    }
}
