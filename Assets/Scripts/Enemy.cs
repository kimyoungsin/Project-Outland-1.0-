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

    public GameObject[] DropItemPrefab;
    public GameObject UnconditionalDropItemPrefab;

    /*
    public GameObject DamageText;
    public Transform textPos;
    */

    public int rand;
    public int DropCount;

    Transform targetTransform = null;
    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;

    Vector2 movement = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rand = Random.Range(0, 3);
    }


    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            spriteRenderer.color = new Color(1, 1, 1, 1f);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.tag = "Die";
            animator.SetBool("isDie", true);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        

    }

    public void Hit(int Damage)
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        Invoke("HitBlink", 0.25f);
        hp -= Damage;
        /*
        GameObject text = Instantiate(DamageText);
        text.transform.position = textPos.position;
        text.GetComponent<FloatingDamageText>().damage = Damage;
        */
    }

    void OnTriggerExit2D(Collider2D collision)
    {
       
    }


    void HitBlink()
    {
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
