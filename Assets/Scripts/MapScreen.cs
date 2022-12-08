using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapScreen : MonoBehaviour
{
    public int MapID;
    public GameObject WorldMapImage;
    public GameObject SquareMap;
    public GameObject SandalleyMap;
    public GameObject BecaMap;
    public GameObject ConcertoMap;
    public GameObject AREAreaMap;

    public static bool MapActivated = false; // 맵화면 활성화 여부(true면 다른 행동, 키입력 멈춤)
    public GameObject OtherUI; //다른 ui들 가리기
    public PlayerMovement playermove;
    public Weapons weapon;
    public WeaponManager weaponManager;

    void Start()
    {
        playermove = FindObjectOfType<PlayerMovement>();
        weapon = FindObjectOfType<Weapons>();
        weaponManager = FindObjectOfType<WeaponManager>();
    }

    
    void Update()
    {
        TryOpenMapScreen();
    }

    private void TryOpenMapScreen()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            MapActivated = !MapActivated;

            if (MapActivated)
            {

                OpenMapScreen();
                playermove.StopMove();
                weapon = FindObjectOfType<Weapons>();
                weaponManager.StopAtk();
            }
            else
            {
                CloseMapScreen();
                playermove.StopMove();
                weapon = FindObjectOfType<Weapons>();
                weaponManager.StopAtk();
            }
        }
    }

    private void OpenMapScreen()
    {
        WorldMapImage.SetActive(true);
        OtherUI.SetActive(false);
    }

    private void CloseMapScreen()
    {
        WorldMapImage.SetActive(false);
        OtherUI.SetActive(true);
    }
}
