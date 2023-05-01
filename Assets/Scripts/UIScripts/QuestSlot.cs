using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public QuestManager questManager;
    public Quest quest; //���� ����Ʈ
    public int questID; //����Ʈ ID(csv���� �ѹ�=�ش� ����Ʈ ID)
    public int QuestPhase; //���� ����Ʈ ���൵
    public Text questNameText; // ����Ʈ ���� �ؽ�Ʈ
    public Inventory theInventory;
    public Image image;
    public Color color;



    void Start()
    {
        theInventory = FindObjectOfType<Inventory>();
        questManager = FindObjectOfType<QuestManager>();
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

    public void QuestAccept()
    {
        questID = quest.QuestID;
    }


    //PointerEventData: ���콺 Ȥ�� ��ġ �Է� �̺�Ʈ�� ���� �������� ��� �ִ�. (�̺�Ʈ�� ���� ��ư, Ŭ�� ��, ���콺 ��ġ, ���� ���콺 �����̰� �ִ��� ����)
    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("����Ʈ �̸� Ŭ���ߴ�!!");
            if (quest != null)
            {
                questManager.QuestTextOn(QuestPhase);
            }

        }

    }

    public void OnPointerEnter(PointerEventData eventData2)
    {
        Debug.Log("����Ʈ �̸��� ��Ҵ�!!");
    }

    public void OnPointerExit(PointerEventData eventData3)
    {
        
    }


}

