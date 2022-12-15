using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public GameObject QuestScreenObj;
    public GameObject[] QuestList;

    public static bool QuestUIActivated = false; // 맵화면 활성화 여부(true면 다른 행동, 키입력 멈춤)
    public GameObject OtherUI; //다른 ui들 가리기
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
