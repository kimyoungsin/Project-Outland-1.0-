using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    static public CameraScript instance;

    // Start is called before the first frame update
    void Awake()
    {
        
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    public void BoundingShape(PolygonCollider2D newCol)
    {
        GetComponent<CinemachineConfiner>().m_BoundingShape2D = newCol;
    }
}
