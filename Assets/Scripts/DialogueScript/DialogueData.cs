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

    string DialogueEvent; //이벤트 여부 판단(선택지, 싸움 등)
    string DialogueSelectEventID; // 이벤트ID 대입
    string DialogueSelectNum; // 선택지 개수 대입
    string DialogueNumber; // 대화 순서 문자열로 저장
    string MoveLine; // 선택지 고를 시 옮길라인
    string SkipLine; // 선택지 고른 후 다이얼로그에서 스킵라인
    int DialogueNumberCount; // 대화 순서(Number) 정수로 변경
    int DialogueSelectEventNum; // 이벤트ID 정수로 변경
    int DialogueSelectCount; // 선택지 개수 정수로 변경
    int MoveLineCount; // 선택지 고를 시 옮길라인 정수로 변경
    int SkipLineCount; // 스킵라인 정수로 변경
    public int Count = 0; // 대화 카운트(csv파일 넘버=대화 순서)
    public NPC DialogueNPC; // 현재 대화중인 npc
    public bool CanDialogue = true; //true면 키 눌러서 대화 넘기기 가능


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
        DialogueNameTextObj.text = data_Dialogue[Count]["이름"].ToString();
        DialogueTextObj.text = "" + data_Dialogue[Count]["대화"].ToString();
        DialogueEvent = data_Dialogue[Count]["이벤트"].ToString();
        DialogueSelectEventID = data_Dialogue[Count]["이벤트시작Num"].ToString();
        DialogueSelectNum = data_Dialogue[Count]["선택지개수"].ToString();
        

        if(CanDialogue == true)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (DialogueEvent == "스킵")
                {
                    SkipLine = data_Dialogue[Count]["스킵라인"].ToString();

                    SkipLineCount = int.Parse(SkipLine);
                    Count = SkipLineCount;
                }
                else if (DialogueEvent == "종료")
                {
                    SkipLine = data_Dialogue[Count]["스킵라인"].ToString();

                    SkipLineCount = int.Parse(SkipLine);
                    Count = SkipLineCount;
                    print(SkipLineCount);

                    DialogueNPC = GameObject.FindGameObjectWithTag("DialogueNPC").GetComponent<NPC>();
                    DialogueNPC.DialogueStartCount = SkipLineCount;

                    UItext.DialogueStopMove();
                    UItext.DialogueEnd();
                    Count = 0;
                }
                else if (DialogueEvent == "잠금해제")
                {
                    SkipLine = data_Dialogue[Count]["스킵라인"].ToString();

                    SkipLineCount = int.Parse(SkipLine);
                    Count = SkipLineCount;
                    print(SkipLineCount);

                    DialogueNPC = GameObject.FindGameObjectWithTag("DialogueNPC").GetComponent<NPC>();
                    DialogueNPC.DialogueStartCount = SkipLineCount;

                    UItext.DialogueStopMove();
                    UItext.DialogueEnd();
                    Count = 0;
                    DialogueNPC.Opening();
                }
                else if (DialogueEvent == "아이템")
                {
                    ItemReward = GameObject.FindGameObjectWithTag("DialogueNPC").GetComponent<Item_Reward>();
                    ItemReward.RewardPay();
                    Count += 1;
                }
                else if (DialogueEvent == "선택지")
                {
                    CanDialogue = false;
                    Count += 1;
                    DialogueSelectEventNum = int.Parse(DialogueSelectEventID);
                    DialogueSelectCount = int.Parse(DialogueSelectNum);
                    SelectDiaUI.SetActive(true);
                    //print("대화 넘버 " + DialogueNumber);
                    //print("대화 넘버(int) " + DialogueNumberCount);
                    //print("이벤트 시작 num(int) " + DialogueSelectEventNum);
                    //print("선택지 개수(int) " + DialogueSelectCount);

                    switch (DialogueSelectCount)
                    {
                        case 1:
                            DiaSelectTextBox_1.SetActive(true);
                            DialogueSelectText_1.text = "" + data_DialogueSelect[DialogueSelectEventNum]["선택대사"].ToString();
                            DiaSelectTextBox_2.SetActive(false);
                            DiaSelectTextBox_3.SetActive(false);
                            DiaSelectTextBox_4.SetActive(false);
                            DiaSelectTextBox_5.SetActive(false);
                            DiaSelectTextBox_6.SetActive(false);
                            break;

                        case 2:
                            DiaSelectTextBox_1.SetActive(true);
                            DiaSelectTextBox_2.SetActive(true);
                            DialogueSelectText_1.text = "" + data_DialogueSelect[DialogueSelectEventNum]["선택대사"].ToString();
                            DialogueSelectText_2.text = "" + data_DialogueSelect[DialogueSelectEventNum + 1]["선택대사"].ToString();
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
                            DialogueSelectText_1.text = "" + data_DialogueSelect[DialogueSelectEventNum]["선택대사"].ToString();
                            DialogueSelectText_2.text = "" + data_DialogueSelect[DialogueSelectEventNum + 1]["선택대사"].ToString();
                            DialogueSelectText_3.text = "" + data_DialogueSelect[DialogueSelectEventNum + 2]["선택대사"].ToString();
                            break;

                        case 4:
                            DialogueSelectScrollViewSet(4);
                            DiaSelectTextBox_1.SetActive(true);
                            DiaSelectTextBox_2.SetActive(true);
                            DiaSelectTextBox_3.SetActive(true);
                            DiaSelectTextBox_4.SetActive(true);
                            DiaSelectTextBox_5.SetActive(false);
                            DiaSelectTextBox_6.SetActive(false);
                            DialogueSelectText_1.text = "" + data_DialogueSelect[DialogueSelectEventNum]["선택대사"].ToString();
                            DialogueSelectText_2.text = "" + data_DialogueSelect[DialogueSelectEventNum + 1]["선택대사"].ToString();
                            DialogueSelectText_3.text = "" + data_DialogueSelect[DialogueSelectEventNum + 2]["선택대사"].ToString();
                            DialogueSelectText_4.text = "" + data_DialogueSelect[DialogueSelectEventNum + 3]["선택대사"].ToString();
                            break;

                        case 5:
                            DialogueSelectScrollViewSet(5);
                            DiaSelectTextBox_1.SetActive(true);
                            DiaSelectTextBox_2.SetActive(true);
                            DiaSelectTextBox_3.SetActive(true);
                            DiaSelectTextBox_4.SetActive(true);
                            DiaSelectTextBox_5.SetActive(true);
                            DiaSelectTextBox_6.SetActive(false);
                            DialogueSelectText_1.text = "" + data_DialogueSelect[DialogueSelectEventNum]["선택대사"].ToString();
                            DialogueSelectText_2.text = "" + data_DialogueSelect[DialogueSelectEventNum + 1]["선택대사"].ToString();
                            DialogueSelectText_3.text = "" + data_DialogueSelect[DialogueSelectEventNum + 2]["선택대사"].ToString();
                            DialogueSelectText_4.text = "" + data_DialogueSelect[DialogueSelectEventNum + 3]["선택대사"].ToString();
                            DialogueSelectText_5.text = "" + data_DialogueSelect[DialogueSelectEventNum + 4]["선택대사"].ToString();
                            break;

                        case 6:
                            DialogueSelectScrollViewSet(6);
                            DiaSelectTextBox_1.SetActive(true);
                            DiaSelectTextBox_2.SetActive(true);
                            DiaSelectTextBox_3.SetActive(true);
                            DiaSelectTextBox_4.SetActive(true);
                            DiaSelectTextBox_5.SetActive(true);
                            DiaSelectTextBox_6.SetActive(true);
                            DialogueSelectText_1.text = "" + data_DialogueSelect[DialogueSelectEventNum]["선택대사"].ToString();
                            DialogueSelectText_2.text = "" + data_DialogueSelect[DialogueSelectEventNum + 1]["선택대사"].ToString();
                            DialogueSelectText_3.text = "" + data_DialogueSelect[DialogueSelectEventNum + 2]["선택대사"].ToString();
                            DialogueSelectText_4.text = "" + data_DialogueSelect[DialogueSelectEventNum + 3]["선택대사"].ToString();
                            DialogueSelectText_5.text = "" + data_DialogueSelect[DialogueSelectEventNum + 4]["선택대사"].ToString();
                            DialogueSelectText_6.text = "" + data_DialogueSelect[DialogueSelectEventNum + 5]["선택대사"].ToString();
                            break;

                        default:
                            break;
                    }
                }
                else if (DialogueEvent == "전투")
                {
                    SkipLine = data_Dialogue[Count]["스킵라인"].ToString();

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
        //호출 시 선택지 1칸 만큼의 길이(30)증가
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
        MoveLine = data_DialogueSelect[DialogueSelectEventNum]["옮길라인"].ToString();
        MoveLineCount = int.Parse(MoveLine);
        Count = MoveLineCount;
    }
    public void Select_2()
    {
        SelectDiaUI.SetActive(false);
        scrollViewBarUI.SetActive(false);
        CanDialogue = true;
        List<Dictionary<string, object>> data_DialogueSelect = CSVReader.Read("DialogueSelect");
        MoveLine = data_DialogueSelect[DialogueSelectEventNum+1]["옮길라인"].ToString();
        MoveLineCount = int.Parse(MoveLine);
        Count = MoveLineCount;
    }
    public void Select_3()
    {
        SelectDiaUI.SetActive(false);
        scrollViewBarUI.SetActive(false);
        CanDialogue = true;
        List<Dictionary<string, object>> data_DialogueSelect = CSVReader.Read("DialogueSelect");
        MoveLine = data_DialogueSelect[DialogueSelectEventNum + 2]["옮길라인"].ToString();
        MoveLineCount = int.Parse(MoveLine);
        Count = MoveLineCount;
    }
    public void Select_4()
    {
        SelectDiaUI.SetActive(false);
        scrollViewBarUI.SetActive(false);
        CanDialogue = true;
        List<Dictionary<string, object>> data_DialogueSelect = CSVReader.Read("DialogueSelect");
        MoveLine = data_DialogueSelect[DialogueSelectEventNum + 3]["옮길라인"].ToString();
        MoveLineCount = int.Parse(MoveLine);
        Count = MoveLineCount;
    }
    public void Select_5()
    {
        SelectDiaUI.SetActive(false);
        scrollViewBarUI.SetActive(false);
        CanDialogue = true;
        List<Dictionary<string, object>> data_DialogueSelect = CSVReader.Read("DialogueSelect");
        MoveLine = data_DialogueSelect[DialogueSelectEventNum + 4]["옮길라인"].ToString();
        MoveLineCount = int.Parse(MoveLine);
        Count = MoveLineCount;
    }
    public void Select_6()
    {
        SelectDiaUI.SetActive(false);
        scrollViewBarUI.SetActive(false);
        CanDialogue = true;
        List<Dictionary<string, object>> data_DialogueSelect = CSVReader.Read("DialogueSelect");
        MoveLine = data_DialogueSelect[DialogueSelectEventNum + 5]["옮길라인"].ToString();
        MoveLineCount = int.Parse(MoveLine);
        Count = MoveLineCount;
    }
}
