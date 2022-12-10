using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public GameObject SkillTreeUI;

    public static bool SkillTreeActivated = false; // 맵화면 활성화 여부(true면 다른 행동, 키입력 멈춤)
    public GameObject OtherUI; //다른 ui들 가리기
    public PlayerMovement playermove;
    public Weapons weapon;
    public WeaponManager weaponManager;
    public Inventory theInventory;

    void Start()
    {
        playermove = FindObjectOfType<PlayerMovement>();
        weapon = FindObjectOfType<Weapons>();
        weaponManager = FindObjectOfType<WeaponManager>();
        theInventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        TryOpenSkillTree();
    }

    private void TryOpenSkillTree()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SkillTreeActivated = !SkillTreeActivated;

            if (SkillTreeActivated)
            {

                OpenSkillTree();
                playermove.StopMove();
                weapon = FindObjectOfType<Weapons>();
                weaponManager.StopAtk();
            }
            else
            {

                CloseSkillTree();
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
