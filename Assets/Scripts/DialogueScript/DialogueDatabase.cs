using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDatabase : MonoBehaviour
{
    public static DialogueDatabase SharedInstance = null; //Instance ������ �̱������� ����, �ٸ� ������Ʈ���� ��� ����

    private UIText UItext;
    private DialogueData DialogueData;
    public GameObject DiaUI;
    public string DialogueNpcName;
    public int[] DialogueEnevt;
    public int CurrentDialogueStartCount;

    Dictionary<string, NPC> NPCData;

    void Start()
    {
        UItext = FindObjectOfType<UIText>();
        NPCData = new Dictionary<string, NPC>();


    }

    private void Awake()
    {
        if (SharedInstance == null)//Instance�� �ý��ۿ� ���� �� 
        {
            SharedInstance = this;//�ڽ��� �ν��Ͻ��� �־���
            DontDestroyOnLoad(gameObject);//OnLoad(���� �ε�Ǿ�����)�ڽ��� �ı����ϰ� ����

        }
        else
        {
            if (SharedInstance != this)//�ν��Ͻ��� �ڽ��� �ƴ϶�� �̹� �ν��Ͻ��� ����
            {
                Destroy(this.gameObject);//Awake()�� ������ �ڽ� �ı�
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
