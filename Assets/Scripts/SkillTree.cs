using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTree : MonoBehaviour
{
    public GameObject SkillTreeUI;

    public static bool SkillTreeActivated = false; // ��ȭ�� Ȱ��ȭ ����(true�� �ٸ� �ൿ, Ű�Է� ����)
    public GameObject OtherUI; //�ٸ� ui�� ������
    public PlayerMovement playermove;
    public Weapons weapon;
    public WeaponManager weaponManager;
    public Inventory theInventory;
    public Player player;

    public TMP_Text PerkPointUI;//�÷��̾� ������Ʈ ǥ��
    public TMP_Text LvTextUI;//�÷��̾� ���� ǥ��

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
        PerkPointUI.text = "�� ����Ʈ: " + player.PerkPoint;
        LvTextUI.text = "����: " + player.Lv;
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
