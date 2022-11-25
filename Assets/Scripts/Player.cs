using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float hp = 100;
    public float Maxhp = 100;
    public float tp = 60;
    public float Maxtp = 60;
    public int Armor = 0;
    public float MovementSpeed = 4;
    public bool TPRefresh = true;
    public bool invincible = false;
    public float invincibletime = 1f;


    public int PreviousMapNum;
    static public Player instance;




    // Start is called before the first frame update
    void Awake()
    {

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
                hp -= 6;
                invincible = true;
                Invoke("invincibleoff", invincibletime);
            }
        }

    }

    void invincibleoff()
    {
        invincible = false;
    }



}
