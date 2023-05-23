using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueData : MonoBehaviour
{
    public UIText UItext;
    public QuestManager questmanager; //����Ʈ ���� �� ������
    public Item_Reward ItemReward;
    public NPCShopStockList NPCShop;

    public Text DialogueTextObj;
    public Text DialogueNameTextObj;
    public Text DialogueSelectText_1;
    public Text DialogueSelectText_2;
    public Text DialogueSelectText_3;
    public Text DialogueSelectText_4;
    public Text DialogueSelectText_5;
    public Text DialogueSelectText_6;
    public GameObject SelectDiaUI;
    public GameObject DiaSelectTextBox_1;
    public GameObject DiaSelectTextBox_2;
    public GameObject DiaSelectTextBox_3;
    public GameObject DiaSelectTextBox_4;
    public GameObject DiaSelectTextBox_5;
    public GameObject DiaSelectTextBox_6;
    public ScrollView scrollView;
    public GameObject scrollViewBarUI;

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
    public bool CanDialogue = true; //true�� Ű ������ ��ȭ �ѱ�� ����

    string QuestID; // CSV�� �ִ� '����Ʈ'ĭ�� ���� ����
    int QuestNumber; // ���ڷ� ���� �� ����

    void Start()
    {
        UItext = FindObjectOfType<UIText>();
        CanDialogue = true;
    }


    void Update()
    {

        List<Dictionary<string, object>> data_Dialogue = CSVReader.Read("Dialogue");
        List<Dictionary<string, object>> data_DialogueSelect = CSVReader.Read("DialogueSelect");

        DialogueNumber = data_Dialogue[Count]["Number"].ToString();
        DialogueNumberCount = int.Parse(DialogueNumber);
        DialogueNameTextObj.text = data_Dialogue[Count]["�̸�"].ToString();
        DialogueTextObj.text = "" + data_Dialogue[Count]["��ȭ"].ToString();
        DialogueEvent = data_Dialogue[Count]["�̺�Ʈ"].ToString();
        DialogueSelectEventID = data_Dialogue[Count]["������"].ToString();
        DialogueSelectNum = data_Dialogue[Count]["����������"].ToString();
        QuestID = data_Dialogue[Count]["����ƮID"].ToString();





        if (CanDialogue == true)
        {
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
                else if (DialogueEvent == "�������̵�")
                {
                    SkipLine = data_Dialogue[Count]["��ŵ����"].ToString();

                    SkipLineCount = int.Parse(SkipLine);
                    Count = SkipLineCount;
                    print(SkipLineCount);

                    DialogueNPC = GameObject.FindGameObjectWithTag("DialogueNPC").GetComponent<NPC>();
                    DialogueNPC.DialogueStartCount = SkipLineCount;
                    DialogueNPC.FadeInStart();

                    UItext.DialogueStopMove();
                    UItext.DialogueEnd();
                    Count = 0;
                }
                else if (DialogueEvent == "������")
                {
                    SkipLine = data_Dialogue[Count]["��ŵ����"].ToString();

                    SkipLineCount = int.Parse(SkipLine);
                    Count = SkipLineCount;
                    print(SkipLineCount);

                    DialogueNPC = GameObject.FindGameObjectWithTag("DialogueNPC").GetComponent<NPC>();
                    DialogueNPC.DialogueStartCount = SkipLineCount;
                    DialogueNPC.AmbushEvent();

                    UItext.DialogueStopMove();
                    UItext.DialogueEnd();
                    Count = 0;
                }
                else if (DialogueEvent == "�������")
                {
                    SkipLine = data_Dialogue[Count]["��ŵ����"].ToString();

                    SkipLineCount = int.Parse(SkipLine);
                    Count = SkipLineCount;
                    print(SkipLineCount);

                    DialogueNPC = GameObject.FindGameObjectWithTag("DialogueNPC").GetComponent<NPC>();
                    DialogueNPC.DialogueStartCount = SkipLineCount;
                    DialogueNPC.Opening();

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
                else if (DialogueEvent == "������")
                {
                    CanDialogue = false;
                    Count += 1;
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
                            DiaSelectTextBox_4.SetActive(false);
                            DiaSelectTextBox_5.SetActive(false);
                            DiaSelectTextBox_6.SetActive(false);
                            break;

                        case 2:
                            DiaSelectTextBox_1.SetActive(true);
                            DiaSelectTextBox_2.SetActive(true);
                            DialogueSelectText_1.text = "" + data_DialogueSelect[DialogueSelectEventNum]["���ô��"].ToString();
                            DialogueSelectText_2.text = "" + data_DialogueSelect[DialogueSelectEventNum + 1]["���ô��"].ToString();
                            DiaSelectTextBox_3.SetActive(false);
                            DiaSelectTextBox_4.SetActive(false);
                            DiaSelectTextBox_5.SetActive(false);
                            DiaSelectTextBox_6.SetActive(false);
                            break;

                        case 3:
                            DiaSelectTextBox_1.SetActive(true);
                            DiaSelectTextBox_2.SetActive(true);
                            DiaSelectTextBox_3.SetActive(true);
                            DiaSelectTextBox_4.SetActive(false);
                            DiaSelectTextBox_5.SetActive(false);
                            DiaSelectTextBox_6.SetActive(false);
                            DialogueSelectText_1.text = "" + data_DialogueSelect[DialogueSelectEventNum]["���ô��"].ToString();
                            DialogueSelectText_2.text = "" + data_DialogueSelect[DialogueSelectEventNum + 1]["���ô��"].ToString();
                            DialogueSelectText_3.text = "" + data_DialogueSelect[DialogueSelectEventNum + 2]["���ô��"].ToString();
                            break;

                        case 4:
                            DialogueSelectScrollViewSet(4);
                            DiaSelectTextBox_1.SetActive(true);
                            DiaSelectTextBox_2.SetActive(true);
                            DiaSelectTextBox_3.SetActive(true);
                            DiaSelectTextBox_4.SetActive(true);
                            DiaSelectTextBox_5.SetActive(false);
                            DiaSelectTextBox_6.SetActive(false);
                            DialogueSelectText_1.text = "" + data_DialogueSelect[DialogueSelectEventNum]["���ô��"].ToString();
                            DialogueSelectText_2.text = "" + data_DialogueSelect[DialogueSelectEventNum + 1]["���ô��"].ToString();
                            DialogueSelectText_3.text = "" + data_DialogueSelect[DialogueSelectEventNum + 2]["���ô��"].ToString();
                            DialogueSelectText_4.text = "" + data_DialogueSelect[DialogueSelectEventNum + 3]["���ô��"].ToString();
                            break;

                        case 5:
                            DialogueSelectScrollViewSet(5);
                            DiaSelectTextBox_1.SetActive(true);
                            DiaSelectTextBox_2.SetActive(true);
                            DiaSelectTextBox_3.SetActive(true);
                            DiaSelectTextBox_4.SetActive(true);
                            DiaSelectTextBox_5.SetActive(true);
                            DiaSelectTextBox_6.SetActive(false);
                            DialogueSelectText_1.text = "" + data_DialogueSelect[DialogueSelectEventNum]["���ô��"].ToString();
                            DialogueSelectText_2.text = "" + data_DialogueSelect[DialogueSelectEventNum + 1]["���ô��"].ToString();
                            DialogueSelectText_3.text = "" + data_DialogueSelect[DialogueSelectEventNum + 2]["���ô��"].ToString();
                            DialogueSelectText_4.text = "" + data_DialogueSelect[DialogueSelectEventNum + 3]["���ô��"].ToString();
                            DialogueSelectText_5.text = "" + data_DialogueSelect[DialogueSelectEventNum + 4]["���ô��"].ToString();
                            break;

                        case 6:
                            DialogueSelectScrollViewSet(6);
                            DiaSelectTextBox_1.SetActive(true);
                            DiaSelectTextBox_2.SetActive(true);
                            DiaSelectTextBox_3.SetActive(true);
                            DiaSelectTextBox_4.SetActive(true);
                            DiaSelectTextBox_5.SetActive(true);
                            DiaSelectTextBox_6.SetActive(true);
                            DialogueSelectText_1.text = "" + data_DialogueSelect[DialogueSelectEventNum]["���ô��"].ToString();
                            DialogueSelectText_2.text = "" + data_DialogueSelect[DialogueSelectEventNum + 1]["���ô��"].ToString();
                            DialogueSelectText_3.text = "" + data_DialogueSelect[DialogueSelectEventNum + 2]["���ô��"].ToString();
                            DialogueSelectText_4.text = "" + data_DialogueSelect[DialogueSelectEventNum + 3]["���ô��"].ToString();
                            DialogueSelectText_5.text = "" + data_DialogueSelect[DialogueSelectEventNum + 4]["���ô��"].ToString();
                            DialogueSelectText_6.text = "" + data_DialogueSelect[DialogueSelectEventNum + 5]["���ô��"].ToString();
                            break;

                        default:
                            break;
                    }
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
                    DialogueNPC.Attacking();
                }
                else if (DialogueEvent == "����")
                {
                    SkipLine = data_Dialogue[Count]["��ŵ����"].ToString();

                    SkipLineCount = int.Parse(SkipLine);
                    Count = SkipLineCount;
                    print(SkipLineCount);

                    NPCShop = FindObjectOfType<NPCShopStockList>();
                    NPCShop.ShopOpen();

                    DialogueNPC = GameObject.FindGameObjectWithTag("DialogueNPC").GetComponent<NPC>();
                    DialogueNPC.DialogueStartCount = SkipLineCount;

                    UItext.DialogueStopMove();
                    UItext.DialogueEnd();
                    Count = 0;
                }
                else if (DialogueEvent == "����Ʈ����")
                {
                    QuestNumber = int.Parse(QuestID);
                    for (int i = 0; i < 1; i++)
                    {
                        if (questmanager.questslots[i].quest != null)
                        {
                            questmanager.questslots[i].quest = DialogueNPC.quest;
                        }
                        else
                        {
                            //Debug.Log("����! ����Ʈ ��� ��ħ!!");
                        }
                    }
                   

                    Count += 1;
                }
                else if (DialogueEvent == "����Ʈ����")
                {
                    QuestNumber = int.Parse(QuestID);
                    Debug.Log("����Ʈ ID: " + QuestNumber);
                    for (int i = 0; i < 1; i++)
                    {
                        if (questmanager.questslots[i].quest.QuestID == QuestNumber)
                        {
                            Debug.Log("����Ʈ ������ ����. ");
                            questmanager.Number[i] += 1;
                            questmanager.questslots[i].QuestPhaseUp();
                        }
                        else
                        {
                           
                        }
                    }


                    Count += 1;
                }
                else
                {
                    SelectDiaUI.SetActive(false);
                    scrollViewBarUI.SetActive(false);
                    Count += 1;
                }
            }
           
        }
    }

    public void DialogueStartCount()
    {
        Count = DialogueDatabase.SharedInstance.CurrentDialogueStartCount;
    }

    public void DialogueSelectScrollViewSet(int _count)
    {
        scrollViewBarUI.SetActive(true);
        //ȣ�� �� ������ 1ĭ ��ŭ�� ����(30)����
        switch (_count)
        {
            case 4:
                scrollView.SetContentSize();
                break;
            case 5:
                scrollView.SetContentSize();
                scrollView.SetContentSize();
                break;
            case 6:
                scrollView.SetContentSize();
                scrollView.SetContentSize();
                scrollView.SetContentSize();
                break;
        }
        
    }

    public void Select_1()
    {
        SelectDiaUI.SetActive(false);
        scrollViewBarUI.SetActive(false);
        CanDialogue = true;
        List<Dictionary<string, object>> data_DialogueSelect = CSVReader.Read("DialogueSelect");
        MoveLine = data_DialogueSelect[DialogueSelectEventNum]["�ű����"].ToString();
        MoveLineCount = int.Parse(MoveLine);
        Count = MoveLineCount;
    }
    public void Select_2()
    {
        SelectDiaUI.SetActive(false);
        scrollViewBarUI.SetActive(false);
        CanDialogue = true;
        List<Dictionary<string, object>> data_DialogueSelect = CSVReader.Read("DialogueSelect");
        MoveLine = data_DialogueSelect[DialogueSelectEventNum+1]["�ű����"].ToString();
        MoveLineCount = int.Parse(MoveLine);
        Count = MoveLineCount;
    }
    public void Select_3()
    {
        SelectDiaUI.SetActive(false);
        scrollViewBarUI.SetActive(false);
        CanDialogue = true;
        List<Dictionary<string, object>> data_DialogueSelect = CSVReader.Read("DialogueSelect");
        MoveLine = data_DialogueSelect[DialogueSelectEventNum + 2]["�ű����"].ToString();
        MoveLineCount = int.Parse(MoveLine);
        Count = MoveLineCount;
    }
    public void Select_4()
    {
        SelectDiaUI.SetActive(false);
        scrollViewBarUI.SetActive(false);
        CanDialogue = true;
        List<Dictionary<string, object>> data_DialogueSelect = CSVReader.Read("DialogueSelect");
        MoveLine = data_DialogueSelect[DialogueSelectEventNum + 3]["�ű����"].ToString();
        MoveLineCount = int.Parse(MoveLine);
        Count = MoveLineCount;
    }
    public void Select_5()
    {
        SelectDiaUI.SetActive(false);
        scrollViewBarUI.SetActive(false);
        CanDialogue = true;
        List<Dictionary<string, object>> data_DialogueSelect = CSVReader.Read("DialogueSelect");
        MoveLine = data_DialogueSelect[DialogueSelectEventNum + 4]["�ű����"].ToString();
        MoveLineCount = int.Parse(MoveLine);
        Count = MoveLineCount;
    }
    public void Select_6()
    {
        SelectDiaUI.SetActive(false);
        scrollViewBarUI.SetActive(false);
        CanDialogue = true;
        List<Dictionary<string, object>> data_DialogueSelect = CSVReader.Read("DialogueSelect");
        MoveLine = data_DialogueSelect[DialogueSelectEventNum + 5]["�ű����"].ToString();
        MoveLineCount = int.Parse(MoveLine);
        Count = MoveLineCount;
    }
}
