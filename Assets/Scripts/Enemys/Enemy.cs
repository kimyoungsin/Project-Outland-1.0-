using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public int Maxhp;
    public int Armor;
    public int Damage;
    public int Exp;
    public float MovementSpeed;
    public bool isHit;
    public Player player;

    public GameObject[] DropItemPrefab;
    public GameObject UnconditionalDropItemPrefab;

    
    public GameObject DamageText;
    public Transform textPos;
    

    public int rand;
    public int DropCount;

    public Transform targetTransform = null;
    public Rigidbody2D rigid;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    Vector2 movement = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rand = Random.Range(0, 3);
        player = FindObjectOfType<Player>();
    }


    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        

    }

    public void Hit(int Damage)
    {
        isHit = true;
        SoundManager.SharedInstance.PlaySE("Bullet_Hit");
        hp -= (Damage - Armor);
        GameObject text = Instantiate(DamageText);
        text.transform.position = textPos.position;
        text.GetComponent<FloatingDamageText>().damage = Damage;
        if (hp <= 0)
        {
            StopAllCoroutines();
            spriteRenderer.color = new Color(1, 1, 1, 1f);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.tag = "Die";
            animator.SetBool("isDie", true);

            player = FindObjectOfType<Player>();
            player.GetExp(Exp);

        }
        else
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            Invoke("HitBlink", 0.25f);

        }


    }

    public void MeleeHit(int Damage, Transform playerPos)
    {
        isHit = true;
        SoundManager.SharedInstance.PlaySE("Bullet_Hit");
        hp -= (Damage - Armor);
        GameObject text = Instantiate(DamageText);
        text.transform.position = textPos.position;
        text.GetComponent<FloatingDamageText>().damage = Damage;
   

        if (hp <= 0)
        {
            StopAllCoroutines();
            spriteRenderer.color = new Color(1, 1, 1, 1f);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.tag = "Die";
            animator.SetBool("isDie", true);

            player = FindObjectOfType<Player>();
            player.GetExp(Exp);
        }
        else
        {

            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            Invoke("HitBlink", 0.25f);

            //ÇÇ°Ý ½Ã ³Ë¹é
            rigid.velocity = Vector2.zero;
            Vector2 direction = (playerPos.transform.position - this.transform.position).normalized;
            rigid.AddForce(-direction.normalized * 5f, ForceMode2D.Impulse);
        }


    }

    void OnTriggerExit2D(Collider2D collision)
    {
       
    }


    void HitBlink()
    {
        isHit = false;
        rigid.velocity = Vector2.zero;
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }

    public void Destruction()
    {
        Instantiate(UnconditionalDropItemPrefab, this.transform.position, Quaternion.identity);
        switch (rand)
        {
            case 0:
                Instantiate(DropItemPrefab[0], this.transform.position, Quaternion.identity);
                break;
            case 1:
                Instantiate(DropItemPrefab[1], this.transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(DropItemPrefab[2], this.transform.position, Quaternion.identity);
                break;

        }
        
    }

}
