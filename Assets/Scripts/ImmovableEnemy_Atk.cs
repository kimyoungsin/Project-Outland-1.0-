using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmovableEnemy_Atk : MonoBehaviour
{
    public bool Alert = false; //������
    public bool Warning = false; //�߰�=���� ����
    public float Radius; //��Ŭ �ݶ��̴� ������=���� �þ߹���
    public float AlertRadius; //��� �� �þ߹���
    public float WarningRadius; //�߰� �� �þ߹���
    public float Damage; //���ݷ�
    public float AttackSpeed; //���ݼӵ�

    public Transform target = null;
    public GameObject Projectile; //���Ÿ� ���� ����ü

    public Rigidbody2D rigid;
    public Animator animator;
    //public Enemy_Projectile enemy_Projectile;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (target != null)
        {

            Vector2 dir = new Vector2(transform.position.x - target.position.x,
                transform.position.y - target.position.y);

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle - 270f, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, 5 * Time.deltaTime);
            transform.rotation = rotation;


        }
    }

    public void AttackOn()
    {
        Vector2 dir = new Vector2(transform.position.x - target.position.x,
                transform.position.y - target.position.y);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Instantiate(Projectile, transform.position, Quaternion.AngleAxis(angle - 270f, Vector3.forward));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<CircleCollider2D>().radius = WarningRadius;
            target = collision.gameObject.transform;
            InvokeRepeating("AttackOn", 0f, AttackSpeed);
            animator.SetBool("isAtk", true);
            Warning = true;

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Warning = false;
            target = null;
            CancelInvoke("AttackOn");
            animator.SetBool("isAtk", false);

            transform.rotation = Quaternion.identity;

            GetComponent<CircleCollider2D>().radius = Radius;
        }
    }

    public void Kill()
    {
        Destroy(transform.parent.gameObject);
        Destroy(gameObject);
    }
}
