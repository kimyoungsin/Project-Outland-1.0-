using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private UIText UItext;
    public Item item;

    void Start()
    {
        UItext = FindObjectOfType<UIText>();
    }


}
