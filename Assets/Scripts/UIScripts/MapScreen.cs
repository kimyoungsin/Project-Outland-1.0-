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


    public GameObject OtherUI; //다른 ui들 가리기
    public PlayerMovement_FSM FSM;
    public Weapons weapon;
    public WeaponManager weaponManager;
    public Inventory theInventory;
    public MapData mapData;
    public UIText UItext;


    void Start()
    {
        FSM = FindObjectOfType<PlayerMovement_FSM>();
        weapon = FindObjectOfType<Weapons>();
        weaponManager = FindObjectOfType<WeaponManager>();
        theInventory = FindObjectOfType<Inventory>();
        mapData = FindObjectOfType<MapData>();
        UItext = FindObjectOfType<UIText>();
    }

    
    void Update()
    {
        
    }



    public void OpenMapScreen()
    {
        mapData = FindObjectOfType<MapData>();
        weapon = FindObjectOfType<Weapons>();

        PlayerMarker.MarkerPos();
        WorldMapImage.SetActive(true);
        OtherUI.SetActive(false);
    }

    public void CloseMapScreen()
    {
        mapData = FindObjectOfType<MapData>();
        weapon = FindObjectOfType<Weapons>();

        WorldMapImage.SetActive(false);
        OtherUI.SetActive(true);
    }

    public void FestTavelCloseMapScreen()
    {
        FSM.StopMove();
        weapon = FindObjectOfType<Weapons>();
        weaponManager.StopAtk();
        WorldMapImage.SetActive(false);
        OtherUI.SetActive(true);
        UItext.FestTravel();
    }

    public void CancelFestTravel()
    {
        CancelFestTravelUI.SetActive(false);
    }
}
