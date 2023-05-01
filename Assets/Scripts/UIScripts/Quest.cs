using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "New Quest/Quest")]
public class Quest : ScriptableObject
{

    public enum QuestType
    {
        Main,  //��������Ʈ
        Side,  //���̵�(����) ����Ʈ
        Replay, //������(�ݺ�����)
        ETC //��Ÿ
    }
    public enum QuestState
    {
        None,
        UnAccept, //���� ���� ����(=���� ������ ����)
        Ongoing, //������
        Finish, //�Ϸ�� ����Ʈ
        ETC

    }

    public QuestType questType; // ����Ʈ ����
    public QuestState questState; // ����Ʈ ����
    public int QuestID; // ����Ʈ ��ȣ(id, CSV�ѹ���)
    public string QuestName; // ����Ʈ �̸�
    public int QuestPhase; //����Ʈ ���� �ܰ�(ex-0: ����, 1:����-2:...)
    public int QuestMaxPhase; //����Ʈ�� ������ �ܰ� ����
    public Item[] Rewards; //����Ʈ ����(�ش� ����Ʈ �Ϸ�� �κ��� ����)
    //public Text QuestNameText;
    //public TMP_Text QuestLogText;

    [TextArea]
    public string QuestExplain; // ����Ʈ ����(����׿�)


}
