using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapPolygonCol : MonoBehaviour
{
    public CameraScript Cam; 
    // Start is called before the first frame update
    void Start()
    {
        Cam = FindObjectOfType<CameraScript>(); //�ó׸ӽ� ī�޶� ã��
        Cam.BoundingShape(GetComponent<PolygonCollider2D>());
        // Ÿ�ϸʿ� ���� ������ �ó׸ӽ� ī�޶� 'CinemachineConfiner'�� 'BoundingShape2D'�� ��������
        // �������� ī�޶� �������� ��.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
