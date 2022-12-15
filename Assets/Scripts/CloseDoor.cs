using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseDoor : MonoBehaviour
{
    public int CurSceneNum;
    public int NextSceneNum;
    public string SceneName;
    public bool isClose = false; //���������� ������

    private Player player;
    private UIText DoorInteractionName;

    void Start()
    {
        player = FindObjectOfType<Player>();
        DoorInteractionName = FindObjectOfType<UIText>();

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DoorInteractionName.InteractionName = SceneName;
            //Debug.Log("'E'Ű�� ������ �̵��� �� �ֽ��ϴ�.");
            if (Input.GetKey(KeyCode.F))
            {
                player.PreviousMapNum = CurSceneNum;
                SceneManager.LoadScene(NextSceneNum);
                //SceneManager.LoadScene("House_1");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DoorInteractionName.InteractionName = "";
        }
    }
}

