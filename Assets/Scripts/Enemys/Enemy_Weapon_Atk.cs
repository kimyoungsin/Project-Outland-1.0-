using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Weapon_Atk : MonoBehaviour
{
    public float MovementSpeed; //기본이속
    public float AlertSpeed; //전투시 이속
    public FieldOfView FOV; //개체 시야
    public Vector2 direction; //이동방향
    public bool Alert = false; //경계상태
    public bool Warning = false; //발각=위험 상태
    public float Radius; //서클 콜라이더 반지름=원래 시야범위
    public float AlertRadius; //경계 시 시야범위
    public float WarningRadius; //발각 시 시야범위

    public Transform targetTransform = null;
    public Rigidbody2D rigid;
    public Animator animator;

    public Enemy_Weapon_Manager weapon;
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
        //Debug.Log(Vector2.Distance(weapon.MeleePos.transform.position, targetTransform.position));
        if (targetTransform != null && weapon.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Unarmd || weapon.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Onehand || weapon.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Twohand || weapon.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Fist)
        {
            if(isMove)
            {
                Vector2 dir = targetTransform.position - transform.position;
                transform.Translate(dir.normalized * MovementSpeed * Time.deltaTime);
                animator.SetFloat("MoveX", dir.x);
                animator.SetFloat("MoveY", dir.y);
            }


        }
        else if (targetTransform != null && weapon.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.OnehandGun || weapon.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.TwohandGun || weapon.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.DoubleGun || weapon.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.HeavyArms)
        {
            Vector2 dir = targetTransform.position - transform.position;
            //transform.Translate(dir.normalized * MovementSpeed * Time.deltaTime);
            animator.SetFloat("LastMoveX", dir.x);
            animator.SetFloat("LastMoveY", dir.y);
            

        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(weapon.GunFire(targetTransform));
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(weapon.ReroldOn());
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FOV.target = collision.gameObject.transform;
            targetTransform = collision.gameObject.transform;
            Warning = true;

            weapon.PlayerPos = targetTransform;

            if (targetTransform != null && weapon.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Unarmd || weapon.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Onehand || weapon.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Twohand || weapon.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.Fist)
            {
                isMove = true;
                StartCoroutine(weapon.UnarmdAttack(direction.x, direction.y));


            }
            else if (targetTransform != null && weapon.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.OnehandGun || weapon.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.TwohandGun || weapon.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.DoubleGun || weapon.CurrentWeapon.weaponTypes == Weapons.WeaponTypes.HeavyArms)
            {
                StartCoroutine(weapon.GunFire(targetTransform));


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

            weapon.PlayerPos = null;
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
}
