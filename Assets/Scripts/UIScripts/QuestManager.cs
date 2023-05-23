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
    public QuestSlot[] questslots; //����Ʈ ��ϵ�
    public GameObject[] questObjs; //����Ʈ ��ϵ�(������Ʈ, on/off ��)
    public Text[] QuestNameTextObj; // ����Ʈ ����
    public TMP_Text QuestLogObj; //����Ʈ �α�
    public TMP_Text QuestGoalObj; //����Ʈ ��ǥ �ؽ�Ʈ

    string QuestNumber; // ����Ʈ ��ȣ ���ڿ��� ����
    int QuestNumberCount; // ����Ʈ ��ȣ(Number) ������ ����
    string QuestPhase; // ����Ʈ ���� ���ڿ��� ����
    int QuestPhaseCount; // ����Ʈ ����(���൵) ������ ����

    string QuestEvent; //�̺�Ʈ ���� �Ǵ�(������, �ο� ��)
    string QuestSelectEventID; // �̺�ƮID ����
    string QuestSelectNum; // ������ ���� ����

    string MoveLine; // ������ �� �� �ű����
    string SkipLine; // ������ �� �� ���̾�α׿��� ��ŵ����

    int QuestSelectEventNum; // �̺�ƮID ������ ����
    int QuestCount; // ������ ���� ������ ����
    int MoveLineCount; // ������ �� �� �ű���� ������ ����
    int SkipLineCount; // ��ŵ���� ������ ����

    public int[] Number; // ����Ʈ ID(csv���� �ѹ�=�ش� ����Ʈ ID)
    public int[] Phase; // ����Ʈ ����(���൵)(csv���� ���൵=�ش� ����Ʈ ���� ����)

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
        QuestPhase = data_Quest[Phase[0]]["���൵"].ToString();
        QuestPhaseCount = int.Parse(QuestPhase);

        QuestLogObj.text = "" + data_Quest[Number[0]]["�α�"].ToString();
        QuestGoalObj.text = "" + data_Quest[Number[0]]["��ǥ"].ToString();

        if(questslots[0].quest == null)// ����Ʈ�� ��������� �ؽ�Ʈ ����ó��
        {
            QuestNameTextObj[0].text = "";
            questObjs[0].SetActive(false);
        }
        else
        {
            QuestNameTextObj[0].text = data_Quest[questslots[0].questID]["����Ʈ"].ToString();
            questObjs[0].SetActive(true);
        }
        if (questslots[1].quest == null)
        {
            QuestNameTextObj[1].text = "";
            questObjs[1].SetActive(false);
        }
        else
        {
            QuestNameTextObj[1].text = data_Quest[questslots[1].questID]["����Ʈ"].ToString();
            questObjs[1].SetActive(true);
        }
        if (questslots[2].quest == null)
        {
            QuestNameTextObj[2].text = "";
            questObjs[2].SetActive(false);
        }
        else
        {
            QuestNameTextObj[2].text = data_Quest[questslots[1].questID]["����Ʈ"].ToString();
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
