using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Chase_AI : MonoBehaviour
{
    public float MovementSpeed; //�⺻�̼�
    public float AlertSpeed; //������ �̼�
    public int CollisionDamage; //���� �����
    public FieldOfView FOV; //��ü �þ�
    public Vector2 direction; //�̵�����
    public bool Alert = false; //������
    public bool Warning = false; //�߰�=���� ����
    public float Radius; //��Ŭ �ݶ��̴� ������=���� �þ߹���
    public float AlertRadius; //��� �� �þ߹���
    public float WarningRadius; //�߰� �� �þ߹���

    public Enemy enemy;
    public Transform targetTransform = null;
    public Rigidbody2D rigid;
    public Animator animator;

    Vector2 movement = new Vector2(0, 0);
    public int rand;  // 0: �̵�, 1: ���, 2: �̵�
    public bool isMove; //AI �ڷ�ƾ��

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        direction.x = Random.Range(-1.0f, 1.0f);
        direction.y = Random.Range(-1.0f, 1.0f);
        enemy = GetComponent<Enemy>();

        rand = Random.Range(0, 3); // 0: �̵�, 1: ���, 2: �̵�
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.isHit == false)
        {
            rigid.velocity = Vector2.zero;
        }
        if (targetTransform != null && isMove)
        {

            Vector2 dir = targetTransform.position - transform.position;
            transform.Translate(dir.normalized * MovementSpeed * Time.deltaTime);
            animator.SetFloat("MoveX", dir.x);
            animator.SetFloat("MoveY", dir.y);

        }
        if (enemy.hp <= 0)
        {
            StopAllCoroutines();
        }
    }

    public IEnumerator AttackAI()
    {
        yield return new WaitForSeconds(0.25f);
        Debug.Log("�� AI����");
        rand = Random.Range(0, 3); // 0: �̵�, 1: ���, 2: �̵�
        Debug.Log("rand�������: " + rand);
        if (rand == 0)
        {
            Debug.Log("�� �̵�");
            StartCoroutine(Move());
        }
        else if (rand == 1)
        {
            Debug.Log("�� ���");
            StartCoroutine(Wait());
        }
        else if (rand == 2)
        {
            Debug.Log("�� �̵�");
            StartCoroutine(Move());
        }

    }

    public IEnumerator Move()
    {
        yield return new WaitForSeconds(0.25f);
        isMove = true;
        Debug.Log("�� �̵���");

        yield return new WaitForSeconds(2f);
        StartCoroutine(AttackAI());
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.25f);
        isMove = false;
        Debug.Log("�� �����");

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(AttackAI());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FOV.target = collision.gameObject.transform;
            FOV.Attention = true;
            targetTransform = collision.gameObject.transform;
            GetComponent<CircleCollider2D>().radius = WarningRadius;
            Warning = true;
            StartCoroutine(AttackAI());
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player.invincible == false)
            {
                player.Hit(CollisionDamage);
            }


        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FOV.target = null;
            FOV.Attention = false;
            Warning = false;
            isMove = false;
            StopAllCoroutines();
            targetTransform = null;
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", 0);
            GetComponent<CircleCollider2D>().radius = Radius;
        }
    }

    public void WarningOn(Collider2D Target)
    {
        Warning = true;
        targetTransform = Target.gameObject.transform;
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

    public void StopChase()
    {
        rigid.velocity = Vector2.zero;
        MovementSpeed = 0f;
        AlertSpeed = 0f;
        targetTransform = null;
        FOV.target = null;
        Warning = false;
        animator.SetBool("isChase", false);
    }
    public void Kill()
    {
        Destroy(gameObject);
    }
}
