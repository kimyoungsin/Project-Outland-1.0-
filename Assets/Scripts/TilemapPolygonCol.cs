using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapPolygonCol : MonoBehaviour
{
    public CameraScript Cam; 
    // Start is called before the first frame update
    void Start()
    {
        Cam = FindObjectOfType<CameraScript>(); //시네머신 카메라 찾기
        Cam.BoundingShape(GetComponent<PolygonCollider2D>());
        // 타일맵에 붙은 폴콘을 시네머신 카메라 'CinemachineConfiner'의 'BoundingShape2D'에 대입해줘
        // 경계밖으로 카메라 못나가게 함.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
