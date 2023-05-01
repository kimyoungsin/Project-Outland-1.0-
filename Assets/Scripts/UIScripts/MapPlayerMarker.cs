using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayerMarker : MonoBehaviour
{
    public MapData mapData;
    void Start()
    {
        mapData = FindObjectOfType<MapData>();
    }

    public void MarkerPos()
    {
        mapData = FindObjectOfType<MapData>();
        if (mapData.CurMapNum == 1)
        {
            this.transform.localPosition = new Vector2(5.7f, 130.3f);
        }
        else if(mapData.CurMapNum == 2)
        {
            this.transform.localPosition = new Vector2(12.9f, 105.3f);
        }
        else if (mapData.CurMapNum == 3)
        {
            this.transform.localPosition = new Vector2(-187.5f, 79.5f);
        }
        else
        {
            this.transform.localPosition = new Vector2(0f, 0f);
        }
    }

}
