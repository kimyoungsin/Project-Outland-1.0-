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

    public GameObject CancelFestTravelUI; //�����̵� ���ҽ� �ߴ� ui(�� ��� ��)
    public MapPlayerMarker PlayerMarker; //����� �÷��̾� ��Ŀ

    public static bool MapActivated = false; // ��ȭ�� Ȱ��ȭ ����(true�� �ٸ� �ൿ, Ű�Է� ����)
    public GameObject OtherUI; //�ٸ� ui�� ������
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
