using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MapTravel : MonoBehaviour, IPointerClickHandler
{
    public GameObject FestTravelUI;

    public int CurSceneNum;
    public int NextSceneNum;
    public string TravelSceneName;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FestTravelUI.SetActive(true);
        FestTravelUI.transform.position = this.transform.position;
    }

    public void FestTravelUIOff()
    {
        FestTravelUI.SetActive(false);
    }

    public void FestTravel()
    {
        //player.PreviousMapNum = CurSceneNum;
        SceneManager.LoadScene(TravelSceneName);
    }

}
