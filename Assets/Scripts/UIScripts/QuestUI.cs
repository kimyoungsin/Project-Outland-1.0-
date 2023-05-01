using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public GameObject QuestScreenObj;


    public GameObject OtherUI; //다른 ui들 가리기
    public PlayerMovement playermove;
    public Weapons weapon;
    public WeaponManager weaponManager;
    public Inventory theInventory;
    public Player player;
    public Quest quest;

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
       
    }



    public void OpenQuest()
    {
        player = FindObjectOfType<Player>();
        weapon = FindObjectOfType<Weapons>();

        QuestScreenObj.SetActive(true);
        OtherUI.SetActive(false);
    }

    public void CloseQuest()
    {
        player = FindObjectOfType<Player>();
        weapon = FindObjectOfType<Weapons>();

        QuestScreenObj.SetActive(false);
        OtherUI.SetActive(true);
    }
}
