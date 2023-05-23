using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public QuestUI questUI;
    public QuestSlot[] questslots; //퀘스트 목록들
    public GameObject[] questObjs; //퀘스트 목록들(오브젝트, on/off 용)
    public Text[] QuestNameTextObj; // 퀘스트 제목
    public TMP_Text QuestLogObj; //퀘스트 로그
    public TMP_Text QuestGoalObj; //퀘스트 목표 텍스트

    string QuestNumber; // 퀘스트 번호 문자열로 저장
    int QuestNumberCount; // 퀘스트 번호(Number) 정수로 변경
    string QuestPhase; // 퀘스트 순서 문자열로 저장
    int QuestPhaseCount; // 퀘스트 순서(진행도) 정수로 변경

    string QuestEvent; //이벤트 여부 판단(선택지, 싸움 등)
    string QuestSelectEventID; // 이벤트ID 대입
    string QuestSelectNum; // 선택지 개수 대입

    string MoveLine; // 선택지 고를 시 옮길라인
    string SkipLine; // 선택지 고른 후 다이얼로그에서 스킵라인

    int QuestSelectEventNum; // 이벤트ID 정수로 변경
    int QuestCount; // 선택지 개수 정수로 변경
    int MoveLineCount; // 선택지 고를 시 옮길라인 정수로 변경
    int SkipLineCount; // 스킵라인 정수로 변경

    public int[] Number; // 퀘스트 ID(csv파일 넘버=해당 퀘스트 ID)
    public int[] Phase; // 퀘스트 순서(진행도)(csv파일 진행도=해당 퀘스트 진행 순서)

    void Start()
    {
        questUI = FindObjectOfType<QuestUI>();
        Number[0] = 1;
        Phase[0] = 1;
    }

    
    void Update()
    {
        List<Dictionary<string, object>> data_Quest = CSVReader.Read("Quest_Log");

        QuestNumber = data_Quest[Number[0]]["Number"].ToString();
        QuestNumberCount = int.Parse(QuestNumber);
        QuestPhase = data_Quest[Phase[0]]["진행도"].ToString();
        QuestPhaseCount = int.Parse(QuestPhase);

        QuestLogObj.text = "" + data_Quest[Number[0]]["로그"].ToString();
        QuestGoalObj.text = "" + data_Quest[Number[0]]["목표"].ToString();

        if(questslots[0].quest == null)// 퀘스트가 비어있으면 텍스트 공백처리
        {
            QuestNameTextObj[0].text = "";
            questObjs[0].SetActive(false);
        }
        else
        {
            QuestNameTextObj[0].text = data_Quest[questslots[0].questID]["퀘스트"].ToString();
            questObjs[0].SetActive(true);
        }
        if (questslots[1].quest == null)
        {
            QuestNameTextObj[1].text = "";
            questObjs[1].SetActive(false);
        }
        else
        {
            QuestNameTextObj[1].text = data_Quest[questslots[1].questID]["퀘스트"].ToString();
            questObjs[1].SetActive(true);
        }
        if (questslots[2].quest == null)
        {
            QuestNameTextObj[2].text = "";
            questObjs[2].SetActive(false);
        }
        else
        {
            QuestNameTextObj[2].text = data_Quest[questslots[1].questID]["퀘스트"].ToString();
            questObjs[2].SetActive(true);
        }



    }

    public void QuestEnter()
    {

    }

    public void QuestTextOn(int number)
    {
        Number[0] = number;
    }

    public void QuestTextOff()
    {
        QuestNameTextObj[0].text = "";
        QuestLogObj.text = "";
        QuestGoalObj.text = "";
    }
}
