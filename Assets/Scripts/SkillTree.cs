using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTree : MonoBehaviour
{
    public GameObject SkillTreeUI;

    public static bool SkillTreeActivated = false; // 맵화면 활성화 여부(true면 다른 행동, 키입력 멈춤)
    public GameObject OtherUI; //다른 ui들 가리기
    public PlayerMovement playermove;
    public Weapons weapon;
    public WeaponManager weaponManager;
    public Inventory theInventory;
    public Player player;

    public TMP_Text PerkPointUI;//플레이어 퍽포인트 표시
    public TMP_Text LvTextUI;//플레이어 레벨 표시

    void Start()
    {
        player = FindObjectOfType<Player>();
        playermove = FindObjectOfType<PlayerMovement>();
        weapon = FindObjectOfType<Weapons>();
        weaponManager = FindObjectOfType<WeaponManager>();
        theInventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        TryOpenSkillTree();
        PerkPointUI.text = "퍽 포인트: " + player.PerkPoint;
        LvTextUI.text = "레벨: " + player.Lv;
    }

    private void TryOpenSkillTree()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SkillTreeActivated = !SkillTreeActivated;

            if (SkillTreeActivated)
            {
                player = FindObjectOfType<Player>();
                OpenSkillTree();
                playermove.StopMove();
                weapon = FindObjectOfType<Weapons>();
                weaponManager.StopAtk();
            }
            else
            {

                CloseSkillTree();
                player = FindObjectOfType<Player>();
                playermove.StopMove();
                weapon = FindObjectOfType<Weapons>();
                weaponManager.StopAtk();
            }
        }
    }

    private void OpenSkillTree()
    {
        
        SkillTreeUI.SetActive(true);
        OtherUI.SetActive(false);
    }

    private void CloseSkillTree()
    {
        SkillTreeUI.SetActive(false);
        OtherUI.SetActive(true);
    }
}
