using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Projectile : MonoBehaviour
{
    Rigidbody2D rigid;
    public int ProjectileDamage; //ImmovableEnemy�� Damage�� �Ѱܹ޴� ����
    public float ProjectiletSpeed; //ImmovableEnemy�� AtkSpeed �Ѱܹ޴� ����



    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.Translate(Vector2.up * ProjectiletSpeed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Shoot(Vector2 Direction, float Speed)
    {
        rigid.AddForce(Direction * Speed);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Player player = collision.gameObject.GetComponent<Player>();
                player.Hit(ProjectileDamage);
                Destroy(gameObject);
            }
        }


    }
}
