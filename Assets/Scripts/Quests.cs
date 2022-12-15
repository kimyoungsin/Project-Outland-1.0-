using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class Quests : MonoBehaviour
{

    public string QuestName; // 퀘스트 이름
    public int QuestPhase; //퀘스트 진행 단계(ex-0: 없음, 1:시작-2:...)
    public Text QuestNameText;
    public TMP_Text QuestLogText;
    public Player player;

    [TextArea]
    public string[] QuestLog; // 퀘스트 로그

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
