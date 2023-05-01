using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal_Count_Set : MonoBehaviour
{
    public Item Metal_Item;
    public Player player;
    public int MetalMinValue;
    public int MetalMaxValue;

    void Start()
    {
        Metal_Item.DropItemCount = Random.Range(MetalMinValue, MetalMaxValue);
    }


    void Update()
    {
        
    }
}
