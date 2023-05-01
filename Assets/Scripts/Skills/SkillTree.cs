using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTree : MonoBehaviour
{
    public GameObject SkillTreeUI;


    public GameObject OtherUI; //�ٸ� ui�� ������
    public PlayerMovement_FSM FSM;
    public Weapons weapon;
    public WeaponManager weaponManager;
    public Inventory theInventory;
    public Player player;

    public TMP_Text PerkPointUI;//�÷��̾� ������Ʈ ǥ��
    public TMP_Text LvTextUI;//�÷��̾� ���� ǥ��

    void Start()
    {
        player = FindObjectOfType<Player>();
        FSM = FindObjectOfType<PlayerMovement_FSM>();
        weapon = FindObjectOfType<Weapons>();
        weaponManager = FindObjectOfType<WeaponManager>();
        theInventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        
        PerkPointUI.text = "�� ����Ʈ: " + player.PerkPoint;
        LvTextUI.text = "����: " + player.Lv;
    }



    public void OpenSkillTree()
    {
        player = FindObjectOfType<Player>();
        weapon = FindObjectOfType<Weapons>();

        SkillTreeUI.SetActive(true);
        OtherUI.SetActive(false);
    }

    public void CloseSkillTree()
    {
        player = FindObjectOfType<Player>();
        weapon = FindObjectOfType<Weapons>();

        SkillTreeUI.SetActive(false);
        OtherUI.SetActive(true);
    }
}
