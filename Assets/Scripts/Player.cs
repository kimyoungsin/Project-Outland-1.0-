using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Lv = 1;
    public int Exp = 0;
    public int MaxExp = 100;
    public float hp = 100;
    public float Maxhp = 100;
    public float tp = 60;
    public float Maxtp = 60;
    public int Armor = 0; //방어력
    public float MovementSpeed = 4; //이속
    public int Strength = 0; //힘(소지무게 증가, 근접공격 강화, 체력 및 방어력 증가 등 관여)
    public int Knowledge = 0; //지식(캐릭터의 지능 및 대화에 관련된 능력치.)
    public int Technique = 0; //테크닉(캐릭터의 민첩성과 감각에 관련된 능력치.)
    public int PerkPoint = 1; //퍽 포인트

    public bool TPRefresh = true;
    public bool invincible = false;
    public float invincibletime = 1f;

    public int PreviousMapNum;
    static public Player instance;
    public string Player_HitSound;

    SpriteRenderer spriteRenderer;
    public Rigidbody2D rigid;

    public UIText uitext;
    public SkillSlot skillSlot;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        skillSlot = FindObjectOfType<SkillSlot>();
        uitext = FindObjectOfType<UIText>();

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
                    tp += 0.1f;
                }
                
            }
        }

        if (hp > Maxhp)
        {
            hp = Maxhp;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetExp(20);
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

    public void GetExp(int exp)
    {
        Debug.Log("얻은 exp: " + exp);
        Debug.Log("현재 exp: " + Exp);
        Exp += exp;
        if (Exp >= MaxExp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        uitext.LevelUpPopUpStart(Lv);
        SoundManager.SharedInstance.PlaySE("Level_Up");
        Exp = (Exp - MaxExp);
        Lv += 1;
        PerkPoint += 1; //퍽 포인트
        MaxExp = (100 + Lv * 50);
    }

    public void SkillLvUp(float _up, string _name)
    {
        switch (_name)
        {
            case "운동":
                Maxhp += 5;
                hp += 5;
                break;
            case "복식호흡":
                Maxtp += 5;
                tp += 5;
                break;
            default:
                break;
        }
    }

}
