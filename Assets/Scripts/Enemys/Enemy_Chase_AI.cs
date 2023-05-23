using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Chase_AI : MonoBehaviour
{
    public float MovementSpeed; //기본이속
    public float AlertSpeed; //전투시 이속
    public int CollisionDamage; //공격 대미지
    public FieldOfView FOV; //개체 시야
    public Vector2 direction; //이동방향
    public bool Alert = false; //경계상태
    public bool Warning = false; //발각=위험 상태
    public float Radius; //서클 콜라이더 반지름=원래 시야범위
    public float AlertRadius; //경계 시 시야범위
    public float WarningRadius; //발각 시 시야범위

    public Enemy enemy;
    public Transform targetTransform = null;
    public Rigidbody2D rigid;
    public Animator animator;

    Vector2 movement = new Vector2(0, 0);
    public int rand;  // 0: 이동, 1: 대기, 2: 이동
    public bool isMove; //AI 코루틴용

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        direction.x = Random.Range(-1.0f, 1.0f);
        direction.y = Random.Range(-1.0f, 1.0f);
        enemy = GetComponent<Enemy>();

        rand = Random.Range(0, 3); // 0: 이동, 1: 대기, 2: 이동
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
        Debug.Log("적 AI가동");
        rand = Random.Range(0, 3); // 0: 이동, 1: 대기, 2: 이동
        Debug.Log("rand굴림결과: " + rand);
        if (rand == 0)
        {
            Debug.Log("적 이동");
            StartCoroutine(Move());
        }
        else if (rand == 1)
        {
            Debug.Log("적 대기");
            StartCoroutine(Wait());
        }
        else if (rand == 2)
        {
            Debug.Log("적 이동");
            StartCoroutine(Move());
        }

    }

    public IEnumerator Move()
    {
        yield return new WaitForSeconds(0.25f);
        isMove = true;
        Debug.Log("적 이동함");

        yield return new WaitForSeconds(2f);
        StartCoroutine(AttackAI());
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.25f);
        isMove = false;
        Debug.Log("적 대기중");

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
