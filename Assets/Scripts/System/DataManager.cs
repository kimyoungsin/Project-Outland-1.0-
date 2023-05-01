using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public Player PlayerData;
    public GameSetting OptionData;
    public Inventory InventoryData;
    public MapData Mapdata;
    public Transform GameLoadPos; //게임 불러오기 시 시작위치
    public int[] InventoryItemID;
    public Item[] ItemData;

    public QuestManager QuestData;
    public SkillTree SkillData;


    public GameObject PlayerPrefab; //플레이어 생성용
    public GameObject UICanvasPrefab; //UI캔버스 생성용
    public GameObject CinemacinePrefab; //시네머신 카메라 생성용

    public int CurrentMap; //저장 당시 맵의 넘버


    public static DataManager SharedInstance = null; //Instance 변수를 싱글톤으로 선언, 다른 오브젝트에서 사용 가능
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
    void Start()
    {

    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        {
            Mapdata = FindObjectOfType<MapData>();
            PlayerData = FindObjectOfType<Player>();
            InventoryData = FindObjectOfType<Inventory>();

            PlayerPrefs.DeleteAll();

            //플레이어 스텟 저장        
            PlayerPrefs.SetFloat("PlayerMaxHp", PlayerData.Maxhp);
            PlayerPrefs.SetFloat("PlayerHp", PlayerData.hp);
            PlayerPrefs.SetFloat("PlayerMaxTp", PlayerData.Maxtp);
            PlayerPrefs.SetFloat("PlayerTp", PlayerData.tp);
            PlayerPrefs.SetInt("PlayerPerkPoint", PlayerData.PerkPoint);
            PlayerPrefs.SetInt("PlayerLV", PlayerData.Lv);
            PlayerPrefs.SetInt("PlayerExp", PlayerData.Exp);


            //인벤토리 아이템 저장
            PlayerPrefs.SetInt("Metal", InventoryData.Metal);
            for (int i = 0; i <= InventoryData.slots.Length - 1; i++)
            {
                if(InventoryData.slots[i].item != null)
                {
                    PlayerPrefs.SetInt("ItemID" + i, InventoryData.slots[i].item.ID);
                    InventoryItemID[i] = PlayerPrefs.GetInt("ItemID" + i, InventoryData.slots[i].item.ID);
                    Debug.Log("저장된 아이템 이름: " + InventoryData.slots[i].item.itemName);
                    Debug.Log("저장된 아이템 ID: " + InventoryData.slots[i].item.ID);
                }

            }

            //시스템(맵 위치, 퀘스트 진행 정도 등)
            PlayerPrefs.SetInt("MapNumber", Mapdata.CurMapNum);

        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            Mapdata = FindObjectOfType<MapData>();
            PlayerData = FindObjectOfType<Player>();
            InventoryData = FindObjectOfType<Inventory>();


            //플레이어 스텟 저장
            PlayerData.Maxhp = PlayerPrefs.GetFloat("PlayerMaxHp");
            PlayerData.hp = PlayerPrefs.GetFloat("PlayerHp");
            PlayerData.Maxtp = PlayerPrefs.GetFloat("PlayerMaxTp");
            PlayerData.tp = PlayerPrefs.GetFloat("PlayerTp");
            PlayerData.PerkPoint = PlayerPrefs.GetInt("PlayerPerkPoint");
            PlayerData.Lv = PlayerPrefs.GetInt("PlayerLV");
            PlayerData.Exp = PlayerPrefs.GetInt("PlayerExp");


            //인벤토리 아이템 불러오기
            InventoryData.Metal = PlayerPrefs.GetInt("Metal");
            for (int i = 0; i <= InventoryData.slots.Length - 1; i++)
            {
                InventoryItemID[i] = PlayerPrefs.GetInt("ItemID" + i);
                for (int j = 0; j <= ItemData.Length - 1; j++)
                {
                    if (ItemData[j].ID == InventoryItemID[i])
                    {
                        InventoryData.AcquireItem(ItemData[j], ItemData[j].DropItemCount);
                        Debug.Log("불러온 아이템 이름: " + InventoryData.slots[i].item.itemName);
                        Debug.Log("불러온 아이템 ID: " + InventoryData.slots[i].item.ID);
                    }
                }



            }
            //시스템 정보 불러오기
            CurrentMap = PlayerPrefs.GetInt("MapNumber");


            SceneManager.LoadScene(CurrentMap);
            Mapdata.GameLoad();
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            InventoryData = FindObjectOfType<Inventory>();
            PlayerPrefs.HasKey("MapNumber");
            PlayerPrefs.HasKey("PlayerMaxHp");
            PlayerPrefs.HasKey("PlayerHp");
            PlayerPrefs.HasKey("PlayerMaxTp");
            PlayerPrefs.HasKey("PlayerTp");
            PlayerPrefs.HasKey("Metal");

            for (int i = 0; i <= InventoryData.slots.Length - 1; i++)
            {
                if (InventoryData.slots[i].item != null)
                {
                    PlayerPrefs.HasKey("ItemID" + i);
 
                }

            }
        }
    }

    public void GameLoad()
    {
        Instantiate(PlayerPrefab, transform.position, Quaternion.identity);
        Instantiate(UICanvasPrefab, transform.position, Quaternion.identity);
        Instantiate(CinemacinePrefab, transform.position, Quaternion.identity);

        Mapdata = FindObjectOfType<MapData>();
        CurrentMap = PlayerPrefs.GetInt("MapNumber");
        

        SceneManager.LoadScene(CurrentMap);
        PlayerData = FindObjectOfType<Player>();
        InventoryData = FindObjectOfType<Inventory>();


        PlayerData.Maxhp = PlayerPrefs.GetFloat("PlayerMaxHp");
        PlayerData.hp = PlayerPrefs.GetFloat("PlayerHp");
        PlayerData.Maxtp = PlayerPrefs.GetFloat("PlayerMaxTp");
        PlayerData.tp = PlayerPrefs.GetFloat("PlayerTp");
        InventoryData.Metal = PlayerPrefs.GetInt("Metal");


        StartCoroutine(GameLoadMove());
    }

    public IEnumerator GameLoadMove()
    {
        yield return new WaitForSeconds(0.5f);
        //인벤토리 아이템 불러오기
        for (int i = 0; i <= InventoryData.slots.Length - 1; i++)
        {

            InventoryItemID[i] = PlayerPrefs.GetInt("ItemID" + i);
            for (int j = 0; j <= ItemData.Length - 1; j++)
            {
                if (ItemData[j].ID == InventoryItemID[i])
                {
                    Debug.Log("불러오기 2");
                    InventoryData.AcquireItem(ItemData[j], ItemData[j].DropItemCount);
                    Debug.Log("불러온 아이템 이름: " + InventoryData.slots[i].item.itemName);
                    Debug.Log("불러온 아이템 ID: " + InventoryData.slots[i].item.ID);
                }
            }



        }
        Mapdata = FindObjectOfType<MapData>();
        StartCoroutine(Mapdata.GameLoad());
        Debug.Log("작동허니?");
    }
}
