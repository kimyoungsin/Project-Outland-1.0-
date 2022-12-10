using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalPickUp : MonoBehaviour
{
    private UIText UItext;
    public int MetalValue;

    void Start()
    {
        UItext = FindObjectOfType<UIText>();
        MetalValue = Random.Range(4, 13);
    }
}
