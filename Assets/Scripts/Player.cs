using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Lv = 1;
    public float hp = 100;
    public float Maxhp = 100;
    public float tp = 60;
    public float Maxtp = 60;
    public int Armor = 0; //방어력
    public float MovementSpeed = 4; //이속
    public int Strength = 1; //힘(소지무게 증가, 근접공격 강화, 체력 및 방어력 증가 등 관여)
    public int Knowledge = 1; //지식(캐릭터의 지능 및 대화에 관련된 능력치.)
    public int Technique = 1; //테크닉(캐릭터의 민첩성과 감각에 관련된 능력치.)
    public int PerkPoint = 0; //퍽 포인트

    public bool TPRefresh = true;
    public bool invincible = false;
    public float invincibletime = 1f;

    public int PreviousMapNum;
    static public Player instance;
    public string Player_HitSound;

    SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }


    void Update()
    {
        if (tp < Maxtp)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                
            }
            else
            {
                if(TPRefresh == true)
                {
                    tp += 0.02f;
                }
                
            }
        }
    }

    public void TPRefreshOff()
    {
        CancelInvoke("TPRefreshOn");
        TPRefresh = false;
        Invoke("TPRefreshOn", 0.5f);
    }
    public void TPRefreshOn()
    {
        TPRefresh = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (invincible == false)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                SoundManager.SharedInstance.PlaySE(Player_HitSound);
                spriteRenderer.color = new Color(1, 1, 1, 0.5f);
                hp -= 6;
                invincible = true;
                Invoke("invincibleoff", invincibletime);
            }
        }

    }

    void invincibleoff()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1f);
        invincible = false;
    }

    public void Hit(int Damage)
    {
        if(invincible == false)
        {
            SoundManager.SharedInstance.PlaySE(Player_HitSound);
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            hp -= Damage;
            invincible = true;
            Invoke("invincibleoff", invincibletime);

        }

    }



}
