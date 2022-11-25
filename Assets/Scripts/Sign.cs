using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public string SignName;
    public int SignDialogueStartCount;
    private UIText UItext;

    void Start()
    {
        UItext = FindObjectOfType<UIText>();
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UItext.InteractionName = SignName;
            if (Input.GetKey(KeyCode.E))
            {
                gameObject.tag = "DialogueNPC";
                DialogueDatabase.SharedInstance.DialogueNpcName = SignName;
                DialogueDatabase.SharedInstance.CurrentDialogueStartCount = SignDialogueStartCount;
                UItext.DialogueStart();

            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UItext.InteractionName = "";
        }
    }
}
