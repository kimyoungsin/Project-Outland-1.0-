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

    public GameObject CancelFestTravelUI; //빠른이동 못할시 뜨는 ui(돈 없어서 등)
    public MapPlayerMarker PlayerMarker; //월드맵 플레이어 마커

    public static bool MapActivated = false; // 맵화면 활성화 여부(true면 다른 행동, 키입력 멈춤)
    public GameObject OtherUI; //다른 ui들 가리기
    public PlayerMovement playermove;
    public Weapons weapon;
    public WeaponManager weaponManager;
    public Inventory theInventory;
    public MapData mapData;


    void Start()
    {
        playermove = FindObjectOfType<PlayerMovement>();
        weapon = FindObjectOfType<Weapons>();
        weaponManager = FindObjectOfType<WeaponManager>();
        theInventory = FindObjectOfType<Inventory>();
        mapData = FindObjectOfType<MapData>();
    }

    
    void Update()
    {
        TryOpenMapScreen();
    }

    private void TryOpenMapScreen()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mapData = FindObjectOfType<MapData>();
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

    public void OpenMapScreen()
    {
        PlayerMarker.MarkerPos();
        WorldMapImage.SetActive(true);
        OtherUI.SetActive(false);
    }

    public void CloseMapScreen()
    {
        WorldMapImage.SetActive(false);
        OtherUI.SetActive(true);
    }

    public void FestTavelCloseMapScreen()
    {
        playermove.StopMove();
        weapon = FindObjectOfType<Weapons>();
        weaponManager.StopAtk();
        MapActivated = !MapActivated;
        WorldMapImage.SetActive(false);
        OtherUI.SetActive(true);
    }

    public void CancelFestTravel()
    {
        CancelFestTravelUI.SetActive(false);
    }
}
