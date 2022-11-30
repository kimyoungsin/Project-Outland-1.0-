using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Reward : MonoBehaviour
{
    [SerializeField]
    private Inventory theInventory;

    public Item[] Rewards;
    void Start()
    {
        theInventory = FindObjectOfType<Inventory>();
    }


    void Update()
    {
        
    }

    public void RewardPay()
    {
        theInventory = FindObjectOfType<Inventory>();
        for(int i = 0; i < 9; i++)
        {
            if (Rewards[i] != null)
            {
                theInventory.AcquireItem(Rewards[i]);
            }
            else
            {
                break;
            }
            
        }
        
    }
}
