using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTree : MonoBehaviour
{
    public GameObject SkillTreeUI;


    public GameObject OtherUI; //다른 ui들 가리기
    public PlayerMovement_FSM FSM;
    public Weapons weapon;
    public WeaponManager weaponManager;
    public Inventory theInventory;
    public Player player;

    public TMP_Text PerkPointUI;//플레이어 퍽포인트 표시
    public TMP_Text LvTextUI;//플레이어 레벨 표시

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
        
        PerkPointUI.text = "퍽 포인트: " + player.PerkPoint;
        LvTextUI.text = "레벨: " + player.Lv;
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
