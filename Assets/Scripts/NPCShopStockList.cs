using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCShopStockList : MonoBehaviour
{
    public Item[] item; //판매중인 아이템 목록
    public int itemCount; //얻은 아이템 개수
    public Image itemImage; // 아이템의 이미지
    public ShopUI shopUI;

    void Start()
    {
        shopUI = FindObjectOfType<ShopUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShopOpen()
    {
        shopUI = FindObjectOfType<ShopUI>();
        for(int i = 0; i < item.Length; i++)
        {
            if(item[i].itemType == Item.ItemType.Used) //소모품은 5개씩
            {
                shopUI.AcquireItem(item[i], 5);
            }
            if(item[i].itemType == Item.ItemType.Bullet) //탄약은 클립 10개씩
            {
                shopUI.AcquireItem(item[i], 10);
            }
            else
            {
                shopUI.AcquireItem(item[i], 1);
            }
            
        }
        shopUI.OpenShopUI();
    }

    public void ItemClear()
    {
        
        for (int i = 0; i < item.Length; i++)
        {
            shopUI.AcquireItem(item[i], 1);
        }
        shopUI.OpenShopUI();
    }
}
