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
    public Transform GameLoadPos; //���� �ҷ����� �� ������ġ
    public int[] InventoryItemID;
    public Item[] ItemData;

    public QuestManager QuestData;
    public SkillTree SkillData;


    public GameObject PlayerPrefab; //�÷��̾� ������
    public GameObject UICanvasPrefab; //UIĵ���� ������
    public GameObject CinemacinePrefab; //�ó׸ӽ� ī�޶� ������

    public int CurrentMap; //���� ��� ���� �ѹ�


    public static DataManager SharedInstance = null; //Instance ������ �̱������� ����, �ٸ� ������Ʈ���� ��� ����
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

            //�÷��̾� ���� ����        
            PlayerPrefs.SetFloat("PlayerMaxHp", PlayerData.Maxhp);
            PlayerPrefs.SetFloat("PlayerHp", PlayerData.hp);
            PlayerPrefs.SetFloat("PlayerMaxTp", PlayerData.Maxtp);
            PlayerPrefs.SetFloat("PlayerTp", PlayerData.tp);
            PlayerPrefs.SetInt("PlayerPerkPoint", PlayerData.PerkPoint);
            PlayerPrefs.SetInt("PlayerLV", PlayerData.Lv);
            PlayerPrefs.SetInt("PlayerExp", PlayerData.Exp);


            //�κ��丮 ������ ����
            PlayerPrefs.SetInt("Metal", InventoryData.Metal);
            for (int i = 0; i <= InventoryData.slots.Length - 1; i++)
            {
                if(InventoryData.slots[i].item != null)
                {
                    PlayerPrefs.SetInt("ItemID" + i, InventoryData.slots[i].item.ID);
                    InventoryItemID[i] = PlayerPrefs.GetInt("ItemID" + i, InventoryData.slots[i].item.ID);
                    Debug.Log("����� ������ �̸�: " + InventoryData.slots[i].item.itemName);
                    Debug.Log("����� ������ ID: " + InventoryData.slots[i].item.ID);
                }

            }

            //�ý���(�� ��ġ, ����Ʈ ���� ���� ��)
            PlayerPrefs.SetInt("MapNumber", Mapdata.CurMapNum);

        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            Mapdata = FindObjectOfType<MapData>();
            PlayerData = FindObjectOfType<Player>();
            InventoryData = FindObjectOfType<Inventory>();


            //�÷��̾� ���� ����
            PlayerData.Maxhp = PlayerPrefs.GetFloat("PlayerMaxHp");
            PlayerData.hp = PlayerPrefs.GetFloat("PlayerHp");
            PlayerData.Maxtp = PlayerPrefs.GetFloat("PlayerMaxTp");
            PlayerData.tp = PlayerPrefs.GetFloat("PlayerTp");
            PlayerData.PerkPoint = PlayerPrefs.GetInt("PlayerPerkPoint");
            PlayerData.Lv = PlayerPrefs.GetInt("PlayerLV");
            PlayerData.Exp = PlayerPrefs.GetInt("PlayerExp");


            //�κ��丮 ������ �ҷ�����
            InventoryData.Metal = PlayerPrefs.GetInt("Metal");
            for (int i = 0; i <= InventoryData.slots.Length - 1; i++)
            {
                InventoryItemID[i] = PlayerPrefs.GetInt("ItemID" + i);
                for (int j = 0; j <= ItemData.Length - 1; j++)
                {
                    if (ItemData[j].ID == InventoryItemID[i])
                    {
                        InventoryData.AcquireItem(ItemData[j], ItemData[j].DropItemCount);
                        Debug.Log("�ҷ��� ������ �̸�: " + InventoryData.slots[i].item.itemName);
                        Debug.Log("�ҷ��� ������ ID: " + InventoryData.slots[i].item.ID);
                    }
                }



            }
            //�ý��� ���� �ҷ�����
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
        //�κ��丮 ������ �ҷ�����
        for (int i = 0; i <= InventoryData.slots.Length - 1; i++)
        {

            InventoryItemID[i] = PlayerPrefs.GetInt("ItemID" + i);
            for (int j = 0; j <= ItemData.Length - 1; j++)
            {
                if (ItemData[j].ID == InventoryItemID[i])
                {
                    Debug.Log("�ҷ����� 2");
                    InventoryData.AcquireItem(ItemData[j], ItemData[j].DropItemCount);
                    Debug.Log("�ҷ��� ������ �̸�: " + InventoryData.slots[i].item.itemName);
                    Debug.Log("�ҷ��� ������ ID: " + InventoryData.slots[i].item.ID);
                }
            }



        }
        Mapdata = FindObjectOfType<MapData>();
        StartCoroutine(Mapdata.GameLoad());
        Debug.Log("�۵����?");
    }
}
