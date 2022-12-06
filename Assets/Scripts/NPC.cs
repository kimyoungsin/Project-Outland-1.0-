using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    void Start()
    {
        UItext = FindObjectOfType<UIText>();
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
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.tag = "NPC";
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

}
