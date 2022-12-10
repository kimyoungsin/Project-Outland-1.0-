using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MapTravel : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject FestTravelUI;
    public GameObject MapNameUI;

    public int CurSceneNum;
    public int NextSceneNum;
    public string TravelSceneName;
    private Player player;
    public MapScreen MapScreenUI;
    public Inventory theInventory;
    public Text YesText;

    public int FestTravelCost; //빠른이동 비용 계산


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        theInventory = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FestTravelCost = 1+(theInventory.Metal/100);
        FestTravelUI.SetActive(true);
        FestTravelUI.transform.position = this.transform.position;
        YesText.text = "이동: " + FestTravelCost;
    }

    public void OnPointerEnter(PointerEventData eventData2)
    {
        MapNameUI.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData3)
    {
        MapNameUI.SetActive(false);
    }

    public void FestTravelUIOff()
    {
        FestTravelUI.SetActive(false);
    }

    public void FestTravel()
    {
        if(theInventory.Metal >= FestTravelCost)
        {
            //player.PreviousMapNum = CurSceneNum;
            theInventory.Metal -= FestTravelCost;
            FestTravelUI.SetActive(false);
            SceneManager.LoadScene(TravelSceneName);
            MapScreenUI.FestTavelCloseMapScreen();
        }
        else
        {
            MapScreenUI.CancelFestTravelUI.SetActive(true);
            FestTravelUI.SetActive(false);
        }

    }





}
