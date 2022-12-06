using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    Color TextColor;
    public PlayerMovement playermove;
    public Weapons weapons;
    public WeaponManager weaponManager;
    public Text StealthText; // 은신상태 텍스트
    public Text InteractionText; // 상호작용(npc이름, 문, 이동할 지역 등) 텍스트
    public Text ItemPickUpText; // 줍는 아이템 텍스트
    public GameObject DiaUI;
    public DialogueData DialogueData;
    public GameObject WeaponImage;
    public Text WeaponAmmoText;
    public string InteractionName;
    public string ItemName;

    static public UIText instance;
    public NPC DialogueNPC;


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
        playermove = FindObjectOfType<PlayerMovement>();
        weapons = FindObjectOfType<Weapons>();
        weaponManager = FindObjectOfType<WeaponManager>();
    }
 
    public void DialogueStart()
    {
        DialogueStopMove();
        DiaUI.SetActive(true);
        DialogueData.DialogueStartCount();
        DialogueNPC = GameObject.FindGameObjectWithTag("DialogueNPC").GetComponent<NPC>();


    }

    public void DialogueStopMove()
    {
        playermove = FindObjectOfType<PlayerMovement>();
        playermove.StopMove();
        weapons = FindObjectOfType<Weapons>();
        weaponManager.StopAtk();
    }

    public void DialogueEnd()
    {
        DiaUI.SetActive(false);
        DialogueNPC.Talking();
        DialogueNPC = null;
    }
    

    // Update is called once per frame
    void Update()
    {
        weapons = FindObjectOfType<Weapons>();
        WeaponAmmoText.text = ""+ weapons.Round + "/" + "" + weapons.MaxRound;
        InteractionText.text = ""+ InteractionName.ToString();
        ItemPickUpText.text = "" + ItemName.ToString();
        WeaponImage.GetComponent<Image>().sprite = weapons.WeaponSprite;
        if (playermove.Stealth == true)
        {
            StealthText.text = "<color=#36FF00>" + "<은신>" + "</color>";
        }
        else
        {
            StealthText.text = "<color=##FF0000>" + "<발각>" + "</color>";
        }

    }
}
