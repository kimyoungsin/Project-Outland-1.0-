using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "New Quest/Quest")]
public class Quest : ScriptableObject
{

    public enum QuestType
    {
        Main,  //메인퀘스트
        Side,  //사이드(보조) 퀘스트
        Replay, //일일퀘(반복가능)
        ETC //기타
    }
    public enum QuestState
    {
        None,
        UnAccept, //아직 수락 안함(=수주 가능한 상태)
        Ongoing, //진행중
        Finish, //완료된 퀘스트
        ETC

    }

    public QuestType questType; // 퀘스트 유형
    public QuestState questState; // 퀘스트 상태
    public int QuestID; // 퀘스트 번호(id, CSV넘버용)
    public string QuestName; // 퀘스트 이름
    public int QuestPhase; //퀘스트 진행 단계(ex-0: 없음, 1:시작-2:...)
    public int QuestMaxPhase; //퀘스트의 마지막 단계 지정
    public Item[] Rewards; //퀘스트 보상(해당 퀘스트 완료시 인벤에 들어옴)
    //public Text QuestNameText;
    //public TMP_Text QuestLogText;

    [TextArea]
    public string QuestExplain; // 퀘스트 설명(디버그용)


}
