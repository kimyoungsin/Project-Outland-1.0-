using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_Title : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }
    public void GameLoad()
    {
        DataManager.SharedInstance.GameLoad();
    }
    public void GameSetting()
    {

    }
    public void GameExit()
    {
        Application.Quit();
    }
}
