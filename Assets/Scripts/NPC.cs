using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class NPC : MonoBehaviour
{

    private UIText UItext;


    public string NPCName;
    public int DialogueStartCount;
    public int HP;
    public int Armor;
    public int Speed;
    public bool isTalk;
    public bool isTalking = false;
    public bool isenemy = false;
    public Quest quest; //NPC가 주는 퀘스트

    public Rigidbody2D rigid;
    public Animator animator;

    public FadeInOut Fadeinout;
    public GameObject FadeObj;
    public CinemachineVirtualCamera vcam;
    public EventManager eventmanager;

    public GameObject[] AmbushEnemys;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        UItext = FindObjectOfType<UIText>();
        eventmanager = FindObjectOfType<EventManager>();

        Fadeinout = FadeObj.GetComponent<FadeInOut>();
        vcam = FindObjectOfType<CinemachineVirtualCamera>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(isTalking == false)
            {
                if (isTalk)
                {
                    isTalking = true;
                    gameObject.tag = "DialogueNPC";
                    DialogueDatabase.SharedInstance.DialogueNpcName = NPCName;
                    DialogueDatabase.SharedInstance.CurrentDialogueStartCount = DialogueStartCount;
                    UItext.DialogueStart();
                }
            }
            
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UItext.InteractionName = NPCName;
            isTalk = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(isenemy == false)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                gameObject.tag = "NPC";
                UItext.InteractionName = "";
                isTalk = false;
            }
        }
        else
        {
            gameObject.tag = "Enemy";
            UItext.InteractionName = "";
            isTalk = false;
        }

    }
    public void Talking()
    {
        if (isTalking == true)
        {
            isTalking = false;
        }
        else if (isTalking == false)
        {
            isTalking = true;
        }
    }

    public void Attacking()
    {
        //gameObject.GetComponent<Enemy_Chase>().enabled = true;
        gameObject.GetComponent<Enemy>().enabled = true;
        gameObject.GetComponent<Animator>().enabled = true;
        gameObject.GetComponent<Enemy_Weapon_Atk>().enabled = true;
        isenemy = true;
        gameObject.tag = "Enemy";
    }

    public void FadeInStart()
    {
        Instantiate(FadeObj, transform.position, Quaternion.identity);
        StartCoroutine(FadeInEvent());
    }
    public void AmbushEvent()
    {
        Instantiate(FadeObj, transform.position, Quaternion.identity);
        StartCoroutine(Ambush());
    }

    public void Opening()
    {
        gameObject.GetComponent<NPC>().enabled = false;
        gameObject.GetComponent<Door>().enabled = true;
    }

    public IEnumerator FadeInEvent()
    {

        yield return new WaitForSeconds(1f);
        transform.Translate(Vector2.up * 64f * Time.deltaTime);

    }

    public IEnumerator Ambush()
    {
        yield return new WaitForSeconds(1.1f);
        for (int i = 0; i < AmbushEnemys.Length; i++)
        {
            AmbushEnemys[i].SetActive(true);
        }
    }

}
