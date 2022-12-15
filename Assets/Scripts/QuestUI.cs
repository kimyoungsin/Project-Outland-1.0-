using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public GameObject QuestScreenObj;
    public GameObject[] QuestList;

    public static bool QuestUIActivated = false; // ��ȭ�� Ȱ��ȭ ����(true�� �ٸ� �ൿ, Ű�Է� ����)
    public GameObject OtherUI; //�ٸ� ui�� ������
    public PlayerMovement playermove;
    public Weapons weapon;
    public WeaponManager weaponManager;
    public Inventory theInventory;
    public Player player;
    public Quests quests;

    void Start()
    {
        player = FindObjectOfType<Player>();
        playermove = FindObjectOfType<PlayerMovement>();
        weapon = FindObjectOfType<Weapons>();
        weaponManager = FindObjectOfType<WeaponManager>();
        theInventory = FindObjectOfType<Inventory>();
        quests = FindObjectOfType<Quests>();
    }

    void Update()
    {
        TryOpenQuest();
    }

    private void TryOpenQuest()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuestUIActivated = !QuestUIActivated;

            if (QuestUIActivated)
            {
                player = FindObjectOfType<Player>();
                OpenQuest();
                playermove.StopMove();
                weapon = FindObjectOfType<Weapons>();
                weaponManager.StopAtk();
            }
            else
            {

                CloseQuest();
                player = FindObjectOfType<Player>();
                playermove.StopMove();
                weapon = FindObjectOfType<Weapons>();
                weaponManager.StopAtk();
            }
        }
    }

    private void OpenQuest()
    {

        QuestScreenObj.SetActive(true);
        OtherUI.SetActive(false);
    }

    private void CloseQuest()
    {
        QuestScreenObj.SetActive(false);
        OtherUI.SetActive(true);
    }
}
