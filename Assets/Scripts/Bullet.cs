using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rigid;
    public int BulletDamage; //Weapons�� Damage�� �Ѱܹ޴� ����
    public float BulletSpeed; //Weapons�� BulletSpeed �Ѱܹ޴� ����



    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.Translate(Vector2.up * BulletSpeed * Time.deltaTime);
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
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                enemy.Hit(BulletDamage);
                Destroy(gameObject);
            }
        }


    }
}
