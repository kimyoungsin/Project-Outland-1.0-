using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCall : MonoBehaviour
{

    private UIText UItext;


    public string NPCName;
    public bool isTalk;
    public bool isTalking = false;


    public GameObject AmbushEnemy;
    public Transform AmbushEnemyTransform;

    void Start()
    {
        UItext = FindObjectOfType<UIText>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isTalking == false)
            {
                if (isTalk)
                {
                    isTalking = true;
                    Instantiate(AmbushEnemy, AmbushEnemyTransform.position, Quaternion.identity);

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
            isTalk = false;
            UItext.InteractionName = "";
        }
    }

}
