using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCShopStockList : MonoBehaviour
{
    public Item[] item; //�Ǹ����� ������ ���
    public int itemCount; //���� ������ ����
    public Image itemImage; // �������� �̹���
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
            if(item[i].itemType == Item.ItemType.Used) //�Ҹ�ǰ�� 5����
            {
                shopUI.AcquireItem(item[i], 5);
            }
            if(item[i].itemType == Item.ItemType.Bullet) //ź���� Ŭ�� 10����
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
