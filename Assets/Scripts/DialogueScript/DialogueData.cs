using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueData : MonoBehaviour
{
    private UIText UItext;
    public Item_Reward ItemReward;

    public Text DialogueTextObj;
    public Text DialogueNameTextObj;
    public Text DialogueSelectText_1;
    public Text DialogueSelectText_2;
    public Text DialogueSelectText_3;
    public GameObject SelectDiaUI;
    public GameObject DiaSelectTextBox_1;
    public GameObject DiaSelectTextBox_2;
    public GameObject DiaSelectTextBox_3;

    string DialogueEvent; //�̺�Ʈ ���� �Ǵ�(������, �ο� ��)
    string DialogueSelectEventID; // �̺�ƮID ����
    string DialogueSelectNum; // ������ ���� ����
    string DialogueNumber; // ��ȭ ���� ���ڿ��� ����
    string MoveLine; // ������ �� �� �ű����
    string SkipLine; // ������ �� �� ���̾�α׿��� ��ŵ����
    int DialogueNumberCount; // ��ȭ ����(Number) ������ ����
    int DialogueSelectEventNum; // �̺�ƮID ������ ����
    int DialogueSelectCount; // ������ ���� ������ ����
    int MoveLineCount; // ������ �� �� �ű���� ������ ����
    int SkipLineCount; // ��ŵ���� ������ ����
    public int Count = 0; // ��ȭ ī��Ʈ(csv���� �ѹ�=��ȭ ����)
    public NPC DialogueNPC; // ���� ��ȭ���� npc


    void Start()
    {
        UItext = FindObjectOfType<UIText>();
        
    }

    // Update is called once per frame
    void Update()
    {

        List<Dictionary<string, object>> data_Dialogue = CSVReader.Read("Dialogue");
        List<Dictionary<string, object>> data_DialogueSelect = CSVReader.Read("DialogueSelect");

        

        DialogueNumber = data_Dialogue[Count]["Number"].ToString();
        DialogueNumberCount = int.Parse(DialogueNumber);
        DialogueNameTextObj.text = data_Dialogue[Count]["�̸�"].ToString();
        DialogueTextObj.text = "" + data_Dialogue[Count]["��ȭ"].ToString();
        DialogueEvent = data_Dialogue[Count]["�̺�Ʈ"].ToString();
        DialogueSelectEventID = data_Dialogue[Count]["�̺�Ʈ����Num"].ToString();
        DialogueSelectNum = data_Dialogue[Count]["����������"].ToString();
        
  

        if (DialogueEvent == "������")
        {

            DialogueSelectEventNum = int.Parse(DialogueSelectEventID);
            DialogueSelectCount = int.Parse(DialogueSelectNum);
            SelectDiaUI.SetActive(true);
            //print("��ȭ �ѹ� " + DialogueNumber);
            //print("��ȭ �ѹ�(int) " + DialogueNumberCount);
            //print("�̺�Ʈ ���� num(int) " + DialogueSelectEventNum);
            //print("������ ����(int) " + DialogueSelectCount);

            switch (DialogueSelectCount)
            {
                case 1:
                    DiaSelectTextBox_1.SetActive(true);
                    DialogueSelectText_1.text = "" + data_DialogueSelect[DialogueSelectEventNum]["���ô��"].ToString();
                    DiaSelectTextBox_2.SetActive(false);
                    DiaSelectTextBox_3.SetActive(false);
                    break;

                case 2:
                    DiaSelectTextBox_1.SetActive(true);
                    DiaSelectTextBox_2.SetActive(true);
                    DialogueSelectText_1.text = "" + data_DialogueSelect[DialogueSelectEventNum]["���ô��"].ToString();
                    DialogueSelectText_2.text = "" + data_DialogueSelect[DialogueSelectEventNum + 1]["���ô��"].ToString();
                    DiaSelectTextBox_3.SetActive(false);
                    break;

                case 3:
                    DiaSelectTextBox_1.SetActive(true);
                    DiaSelectTextBox_2.SetActive(true);
                    DiaSelectTextBox_3.SetActive(true);
                    DialogueSelectText_1.text = "" + data_DialogueSelect[DialogueSelectEventNum]["���ô��"].ToString();
                    DialogueSelectText_2.text = "" + data_DialogueSelect[DialogueSelectEventNum + 1]["���ô��"].ToString();
                    DialogueSelectText_3.text = "" + data_DialogueSelect[DialogueSelectEventNum + 2]["���ô��"].ToString();
                    break;

                case 4:

                    break;

                default:
                    break;
            }  
        }

        
        else
        {
            SelectDiaUI.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (DialogueEvent == "��ŵ")
            {
                SkipLine = data_Dialogue[Count]["��ŵ����"].ToString();

                SkipLineCount = int.Parse(SkipLine);
                Count = SkipLineCount;
            }
            else if (DialogueEvent == "����")
            {
                SkipLine = data_Dialogue[Count]["��ŵ����"].ToString();

                SkipLineCount = int.Parse(SkipLine);
                Count = SkipLineCount;
                print(SkipLineCount);

                DialogueNPC = GameObject.FindGameObjectWithTag("DialogueNPC").GetComponent<NPC>();
                DialogueNPC.DialogueStartCount = SkipLineCount;

                UItext.DialogueStopMove();
                UItext.DialogueEnd();
                Count = 0;
            }
            else if (DialogueEvent == "������")
            {
                ItemReward = GameObject.FindGameObjectWithTag("DialogueNPC").GetComponent<Item_Reward>();
                ItemReward.RewardPay();
                Count += 1;
            }
            else 
            {
                Count += 1;
            }
            
        }
    }

    public void DialogueStartCount()
    {
        Count = DialogueDatabase.SharedInstance.CurrentDialogueStartCount;
    }

    public void Select_1()
    {
        List<Dictionary<string, object>> data_DialogueSelect = CSVReader.Read("DialogueSelect");
        MoveLine = data_DialogueSelect[DialogueSelectEventNum]["�ű����"].ToString();
        MoveLineCount = int.Parse(MoveLine);
        Count = MoveLineCount;
    }
    public void Select_2()
    {
        List<Dictionary<string, object>> data_DialogueSelect = CSVReader.Read("DialogueSelect");
        MoveLine = data_DialogueSelect[DialogueSelectEventNum+1]["�ű����"].ToString();
        MoveLineCount = int.Parse(MoveLine);
        Count = MoveLineCount;
    }
    public void Select_3()
    {
        List<Dictionary<string, object>> data_DialogueSelect = CSVReader.Read("DialogueSelect");
        MoveLine = data_DialogueSelect[DialogueSelectEventNum + 2]["�ű����"].ToString();
        MoveLineCount = int.Parse(MoveLine);
        Count = MoveLineCount;
    }
}
