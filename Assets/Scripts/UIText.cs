using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    Color TextColor;
    public PlayerMovement playermove;
    public Weapons weapons;
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
    }
 
    public void DialogueStart()
    {
        DialogueStopMove();
        DiaUI.SetActive(true);
        DialogueData.DialogueStartCount();
        
    }

    public void DialogueStopMove()
    {
        playermove = FindObjectOfType<PlayerMovement>();
        playermove.StopMove();
        weapons = FindObjectOfType<Weapons>();
        weapons.StopAtk();
    }

    public void DialogueEnd()
    {
        DiaUI.SetActive(false);
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
            StealthText.text = "<color=#36FF00>" + "<����>" + "</color>";
        }
        else
        {
            StealthText.text = "<color=##FF0000>" + "<�߰�>" + "</color>";
        }

    }
}
