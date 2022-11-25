using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDatabase : MonoBehaviour
{
    public static DialogueDatabase SharedInstance = null; //Instance 변수를 싱글톤으로 선언, 다른 오브젝트에서 사용 가능

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
        if (SharedInstance == null)//Instance가 시스템에 없을 때 
        {
            SharedInstance = this;//자신을 인스턴스로 넣어줌
            DontDestroyOnLoad(gameObject);//OnLoad(씬이 로드되었을때)자신을 파괴안하고 유지

        }
        else
        {
            if (SharedInstance != this)//인스턴스가 자신이 아니라면 이미 인스턴스가 존재
            {
                Destroy(this.gameObject);//Awake()로 생성된 자신 파괴
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
