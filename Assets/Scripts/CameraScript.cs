using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    static public CameraScript instance;
    static public Transform playerPos;

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

    private void Update()
    {
        if(GetComponent<CinemachineVirtualCamera>().Follow == false)
        {
            playerPos = FindObjectOfType<Player>().transform;
            GetComponent<CinemachineVirtualCamera>().Follow = playerPos;
        }

    }
}
