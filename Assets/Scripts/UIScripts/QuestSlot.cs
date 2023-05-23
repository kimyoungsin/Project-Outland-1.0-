using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public QuestManager questManager;
    public Quest quest; //현재 퀘스트
    public int questID; //퀘스트 ID(csv파일 넘버=해당 퀘스트 ID)
    public int QuestPhase; //현재 퀘스트 진행도
    public Text questNameText; // 퀘스트 제목 텍스트
    public Inventory theInventory;
    public UIText uitext;
    public Image image;
    public Color color;



    void Start()
    {
        theInventory = FindObjectOfType<Inventory>();
        questManager = FindObjectOfType<QuestManager>();
        uitext = FindObjectOfType<UIText>();
        image = GetComponent<Image>();
    }

    void Update()
    {
       if(quest == null)
        {
            color = image.color;
            color.a = 0f;
            image.color = color;
        }
       else
        {
            color = image.color;
            color.a = 255f;
            image.color = color;
        }
    }


    //PointerEventData: 마우스 혹은 터치 입력 이벤트에 관한 정보들이 담겨 있다. (이벤트가 들어온 버튼, 클릭 수, 마우스 위치, 현재 마우스 움직이고 있는지 여부)
    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("퀘스트 이름 클릭했다!!");
            if (quest != null)
            {
                questManager.QuestTextOn(questID);
            }

        }

    }

    public void OnPointerEnter(PointerEventData eventData2)
    {
        Debug.Log("퀘스트 이름에 닿았다!!");
    }

    public void OnPointerExit(PointerEventData eventData3)
    {
        
    }

    public void QuestAccept()
    {
        questID = quest.QuestID;
        uitext.QuestAcceptPopUpStart(quest.QuestName);
    }

    public void QuestPhaseUp()
    {
        QuestPhase += 1;
        quest.QuestPhase += 1;
        uitext.QuestPopUpStart(quest.QuestName);
    }


}

