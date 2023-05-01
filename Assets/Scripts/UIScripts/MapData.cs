using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    public string MapName;
    public int CurMapNum; //=CurSceneNum
    public Transform GameLoadPos; //���� �ҷ����� �� ������ġ
    private Player player;
    public TutorialUI ShowUI;
    void Start()
    {
        ShowUI = FindObjectOfType<TutorialUI>();
        ShowUI.Tutorialext.text = "" + MapName;
        ShowUI.UIShow();

        player = FindObjectOfType<Player>();
        if (DataManager.SharedInstance.CurrentMap == CurMapNum)
        {
            player.transform.position = this.transform.position;
        }
    }

    public IEnumerator GameLoad()
    {
        yield return new WaitForSeconds(0.5f);
        player = FindObjectOfType<Player>();
        player.transform.position = GameLoadPos.transform.position;
        Debug.Log("�۵����?");
    }

    

}
