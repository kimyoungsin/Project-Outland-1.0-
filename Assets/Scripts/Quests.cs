using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class Quests : MonoBehaviour
{

    public string QuestName; // ����Ʈ �̸�
    public int QuestPhase; //����Ʈ ���� �ܰ�(ex-0: ����, 1:����-2:...)
    public Text QuestNameText;
    public TMP_Text QuestLogText;
    public Player player;

    [TextArea]
    public string[] QuestLog; // ����Ʈ �α�

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            QuestPhase += 1;
        }
    }

    public void QuestLogOn()
    {
        QuestLogText.text = QuestLog[QuestPhase];
    }

    public void QuestLogOff()
    {
        QuestLogText.text = "";
    }
}
