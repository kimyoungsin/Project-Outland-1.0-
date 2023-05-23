using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIText : MonoBehaviour
{
    Color TextColor;
    //public PlayerMovement playermove;
    public PlayerMovement_FSM FSM;
    public GameObject weaponInstance;
    public Weapons weapons;
    public WeaponManager weaponManager;
    public Text StealthText; // ���Ż��� �ؽ�Ʈ
    public Text InteractionText; // ��ȣ�ۿ�(npc�̸�, ��, �̵��� ���� ��) �ؽ�Ʈ
    public Text ItemPickUpText; // �ݴ� ������ �ؽ�Ʈ
    public GameObject DiaUI;
    public DialogueData DialogueData;
    public GameObject WeaponImage;
    public Text WeaponAmmoText;
    public string InteractionName;
    public string ItemName;

    static public UIText instance;
    public NPC DialogueNPC;
    public TutorialUI tutorialUI; //Ʃ�丮�� ǥ�� �� UI

    //�˾�â ����
    public GameObject PopUpUI; //������ ȹ��, ����Ʈ ���� �� ǥ�ÿ� �˾�ui
    public TMP_Text PopUpText; //�˾� �ؽ�Ʈ
    public Animator PopUpUIAni;

    //�κ��丮, ����Ʈ, ��ųƮ�� �� ui�� ����
    public  bool inventoryActivated = false; // �κ��丮 Ȱ��ȭ ����(true�� �ٸ� �ൿ, Ű�Է� ����)
    public  bool MapActivated = false; // ��ȭ�� Ȱ��ȭ ����(true�� �ٸ� �ൿ, Ű�Է� ����)
    public  bool SkillTreeActivated = false; // ��ųƮ��ȭ�� Ȱ��ȭ ����(true�� �ٸ� �ൿ, Ű�Է� ����)
    public  bool QuestUIActivated = false; // ��ȭ�� Ȱ��ȭ ����(true�� �ٸ� �ൿ, Ű�Է� ����)

    public Inventory InventoryBase; // �κ��丮
    public MapScreen WorldMapImage; // �����
    public SkillTree SkillTreeUI; // ��ųƮ��
    public QuestUI QuestScreenObj; // ����Ʈ

    void Awake()
    {

        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    void Start()
    {
        //playermove = FindObjectOfType<PlayerMovement>();
        FSM = FindObjectOfType<PlayerMovement_FSM>();
        //weapons = FindObjectOfType<Weapons>();
        weaponManager = FindObjectOfType<WeaponManager>();
    }
 
    public void DialogueStart()
    {
        FSM.ChangeState(PlayerMovement_FSM.State.STAND);
        DialogueStopMove();
        DiaUI.SetActive(true);
        DialogueData.DialogueStartCount();
        DialogueNPC = GameObject.FindGameObjectWithTag("DialogueNPC").GetComponent<NPC>();


    }

    public void DialogueStopMove()
    {
        //playermove = FindObjectOfType<PlayerMovement>();
        FSM = FindObjectOfType<PlayerMovement_FSM>();
        FSM.StopMove();
        //weapons = FindObjectOfType<Weapons>();
        weaponManager.StopAtk();
    }

    public void DialogueEnd()
    {
        DiaUI.SetActive(false);
        DialogueNPC.Talking();
        DialogueNPC = null;
    }

    public void WeaponChange(Weapons _weapon)
    {
        //weaponInstance = _weapon;
        weapons = _weapon;
    }
    

    // Update is called once per frame
    void Update()
    {
        WeaponAmmoText.text = ""+ weapons.Round + "/" + "" + weapons.MaxRound;
        InteractionText.text = ""+ InteractionName.ToString();
        ItemPickUpText.text = "" + ItemName.ToString();
        WeaponImage.GetComponent<Image>().sprite = weapons.WeaponSprite;
        if (FSM.isStealth == true)
        {
            StealthText.text = "<color=#36FF00>" + "<����>" + "</color>";
        }
        else
        {
            StealthText.text = "<color=##FF0000>" + "<�߰�>" + "</color>";
        }

        TryOpenInventory();
        TryOpenMapScreen();
        TryOpenSkillTree();
        TryOpenQuest();

    }

    public void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            
            inventoryActivated = !inventoryActivated;

            if(MapActivated || SkillTreeActivated || QuestUIActivated)
            {
                FSM.StopMove();
                weaponManager.StopAtk();
                MapActivated = false;
                SkillTreeActivated = false;
                QuestUIActivated = false;
            }


            if (inventoryActivated)
            {

                InventoryBase.OpenInventory();
                WorldMapImage.CloseMapScreen();
                SkillTreeUI.CloseSkillTree();
                QuestScreenObj.CloseQuest();
                FSM.StopMove();
 
                weaponManager.StopAtk();
            }
            else
            {

                InventoryBase.CloseInventory();
                FSM.StopMove();
                weaponManager.StopAtk();
            }
        }
    }

    public void TryOpenMapScreen()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {

            MapActivated = !MapActivated;

            if (inventoryActivated || SkillTreeActivated || QuestUIActivated)
            {
                FSM.StopMove();
                weaponManager.StopAtk();
                inventoryActivated = false;
                SkillTreeActivated = false;
                QuestUIActivated = false;
            }

            if (MapActivated)
            {

                WorldMapImage.OpenMapScreen();
                InventoryBase.CloseInventory();
                SkillTreeUI.CloseSkillTree();
                QuestScreenObj.CloseQuest();
                FSM.StopMove();
                weaponManager.StopAtk();
            }
            else
            {
                WorldMapImage.CloseMapScreen();
                FSM.StopMove();
                weaponManager.StopAtk();
            }
        }
    }

    public void TryOpenSkillTree()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {

            SkillTreeActivated = !SkillTreeActivated;


            if (inventoryActivated || MapActivated || QuestUIActivated)
            {
                FSM.StopMove();
                weaponManager.StopAtk();
                inventoryActivated = false;
                MapActivated = false;
                QuestUIActivated = false;
            }

            if (SkillTreeActivated)
            {
               
                SkillTreeUI.OpenSkillTree();
                WorldMapImage.CloseMapScreen();
                InventoryBase.CloseInventory();
                QuestScreenObj.CloseQuest();
                FSM.StopMove();   
                weaponManager.StopAtk();
            }
            else
            {

                SkillTreeUI.CloseSkillTree();
                FSM.StopMove();
                weaponManager.StopAtk();
            }
        }
    }

    public void TryOpenQuest()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuestUIActivated = !QuestUIActivated;

            if (inventoryActivated || MapActivated || SkillTreeActivated)
            {
                FSM.StopMove();
                weaponManager.StopAtk();
                inventoryActivated = false;
                MapActivated = false;
                SkillTreeActivated = false;
            }

            if (QuestUIActivated)
            {
             
                QuestScreenObj.OpenQuest();
                WorldMapImage.CloseMapScreen();
                SkillTreeUI.CloseSkillTree();
                InventoryBase.CloseInventory();
                FSM.StopMove();
            
                weaponManager.StopAtk();
            }
            else
            {

                QuestScreenObj.CloseQuest();
                FSM.StopMove();
                weaponManager.StopAtk();
            }
        }
    }

    public void FestTravel()
    {
        QuestUIActivated = false;
        inventoryActivated = false;
        MapActivated = false;
        SkillTreeActivated = false;
        QuestScreenObj.CloseQuest();
        WorldMapImage.CloseMapScreen();
        SkillTreeUI.CloseSkillTree();
        InventoryBase.CloseInventory();
    }


    public void PopUpStart(string text)
    {
        PopUpUIAni.SetBool("isShow", true);
        PopUpText.text = " " + text;
        StartCoroutine(PopUpEnd(2f));
    }

    public void QuestAcceptPopUpStart(string text)
    {
        PopUpUIAni.SetBool("isShow", true);
        PopUpText.text = "����Ʈ ����: " + text;
        StartCoroutine(PopUpEnd(4f));
    }
    public void QuestPopUpStart(string text)
    {
        PopUpUIAni.SetBool("isShow", true);
        PopUpText.text = "����Ʈ ����: " + text;
        StartCoroutine(PopUpEnd(4f));
    }

    public void QuestEndStart(string text)
    {
        PopUpUIAni.SetBool("isShow", true);
        PopUpText.text = "����Ʈ �Ϸ�: " + text;
        StartCoroutine(PopUpEnd(4f));
    }

    public void LevelUpPopUpStart(int lv)
    {
        PopUpUIAni.SetBool("isShow", true);
        PopUpText.text = "���� ��! (" + (lv - 1) + "-> " + lv + ")";
        StartCoroutine(PopUpEnd(4f));
    }

    public IEnumerator PopUpEnd(float time)
    {
        yield return new WaitForSeconds(time);
        PopUpUIAni.SetBool("isShow", false);
    }

    /*
    public IEnumerator QuestPopUpEnd()
    {
        yield return new WaitForSeconds(4f);
        PopUpUIAni.SetBool("isShow", false);
    }
    */
}
