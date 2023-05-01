using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ShopUI : MonoBehaviour
{
    public static bool ShopActivated = false; // 상점 창 활성화 여부(true면 다른 행동, 키입력 멈춤)

    [SerializeField]
    public GameObject ShopBase;
    [SerializeField]
    public GameObject ShopSlotsParent;
    [SerializeField]
    public GameObject InventorySlotsParent;

    public ShopSlot[] Shopslots; //상점 슬롯 배열
    public ShopInventorySlot[] Inventoryslots; //인벤토리 슬롯 배열

    public GameObject OtherUI;

    public Player player;
    public PlayerMovement_FSM playermove;
    public Weapons weapon;
    public WeaponManager weaponManager;

    public GameObject ItemExplainUI; //마우스 닿으면 표시하는 설명 ui
    public GameObject SellItemExplainUI; //마우스 닿으면 표시하는 설명 ui(판매용)
    public Text ItemExplain;
    public Text ItemValue;
    public Text SellItemExplain;
    public Text SellItemValue;
    public Text PlayerMoneyText;

    public Inventory theInventory;
    public int bulletCount; //총알 수
    public int ShopMetal; //골드, 캡, 머니...

    void Start()
    {
        Shopslots = ShopSlotsParent.GetComponentsInChildren<ShopSlot>();
        Inventoryslots = InventorySlotsParent.GetComponentsInChildren<ShopInventorySlot>();
        playermove = FindObjectOfType<PlayerMovement_FSM>();
        player = FindObjectOfType<Player>();
        weapon = FindObjectOfType<Weapons>();
        weaponManager = FindObjectOfType<WeaponManager>();
        theInventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        PlayerMoneyText.text = "M: " + theInventory.Metal.ToString();
        ShopActivated = !ShopActivated;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ItemExplainOff();
            CloseShopUI();
        }
    }

    public void OpenShopUI()
    {
        playermove.StopMove();
        weapon = FindObjectOfType<Weapons>();
        weaponManager.StopAtk();
        ShopBase.SetActive(true);
        OtherUI.SetActive(false);

        for (int i = 0; i < theInventory.slots.Length; i++)
        {
            if (theInventory.slots[i].item != null)
            {
                Inventoryslots[i].AddItem(theInventory.slots[i].item, theInventory.slots[i].itemCount);
                Inventoryslots[i].SlotNum = i;

            }
               
        }
            
    }

    public void CloseShopUI()
    {

        playermove.StopMove();
        weapon = FindObjectOfType<Weapons>();
        weaponManager.StopAtk();
        ShopBase.SetActive(false);
        OtherUI.SetActive(true);


    }

    public void ItemBuy(Item _item, int _count) //1은 디폴트값
    {
        if (Item.ItemType.Metal == _item.itemType)
        {
            theInventory.Metal += _item.DropItemCount;
            return;
        }
        else
        {
            if (Item.ItemType.Equip != _item.itemType)
            {
                for (int i = 0; i < Inventoryslots.Length; i++)
                {
                    if (Inventoryslots[i].item != null)
                    {
                        if (Inventoryslots[i].item.itemName == _item.itemName)
                        {
                            _count = _item.DropItemCount;
                            Inventoryslots[i].SetSlotCount(_count);
                            return;
                        }
                    }
                }
            }

            for (int i = 0; i < Inventoryslots.Length; i++)
            {
                if (Inventoryslots[i].item == null)
                {
                    _count = _item.DropItemCount;
                    Inventoryslots[i].AddItem(_item, _count);
                    return;
                }
            }
        }


    }

    public void AcquireItem(Item _item, int _count) //1은 디폴트값
    {
        if (Item.ItemType.Metal == _item.itemType)
        {
            theInventory.Metal += _item.DropItemCount;
            return;
        }
        else
        {
            if (Item.ItemType.Equip != _item.itemType)
            {
                for (int i = 0; i < Shopslots.Length; i++)
                {
                    if (Shopslots[i].item != null)
                    {
                        if (Shopslots[i].item.itemName == _item.itemName)
                        {
                            _count = _item.DropItemCount;
                            Shopslots[i].SetSlotCount(_count);
                            return;
                        }
                    }
                }
            }

            for (int i = 0; i < Shopslots.Length; i++)
            {
                if (Shopslots[i].item == null)
                {
                    _count = _item.DropItemCount;
                    Shopslots[i].AddItem(_item, _count);
                    return;
                }
            }
        }


    }

    public void CheckBullet(Item _item) //인벤에 해당 탄약이 있나 체크
    {
        if (Item.ItemType.Equip != _item.itemType)
        {
            for (int i = 0; i < Shopslots.Length; i++)
            {
                if (Shopslots[i].item != null)
                {
                    if (Shopslots[i].item.itemName == _item.itemName)
                    {
                        bulletCount = Shopslots[i].itemCount;
                        Debug.Log("CheckBullet: " + bulletCount);
                        return;
                    }
                }
            }
        }
    }

    public void UseBullet(Item _item, int _count) //인벤에있는 해당 탄약 소모
    {
        if (Item.ItemType.Equip != _item.itemType)
        {
            for (int i = 0; i < Shopslots.Length; i++)
            {
                if (Shopslots[i].item != null)
                {
                    if (Shopslots[i].item.itemName == _item.itemName)
                    {
                        //weapon = FindObjectOfType<Weapons>();
                        Shopslots[i].SetSlotCount(_count);
                        bulletCount = Shopslots[i].itemCount;
                        Debug.Log("usebullet: " + bulletCount);
                        Debug.Log(Shopslots[i].itemCount);
                        return;
                    }
                }
            }
        }
    }

    public int BulletCountReturn(int MaxRoundCount)
    {
        if (bulletCount >= MaxRoundCount)
        {
            return MaxRoundCount;
        }
        else
        {
            return bulletCount;
        }


    }

    public void ItemExplainOn(string _itemExp, string _itemValue)
    {
        ItemExplainUI.SetActive(true);
        ItemExplain.text = _itemExp;
        ItemValue.text = "가격: " + _itemValue;
    }


    public void SellItemExplainOn(string _itemExp, string _itemValue)
    {
        SellItemExplainUI.SetActive(true);
        SellItemExplain.text = _itemExp;
        SellItemValue.text = "판매가: " + _itemValue;
    }

    public void ItemExplainOff()
    {
        ItemExplainUI.SetActive(false);
        SellItemExplainUI.SetActive(false);
        ItemExplain.text = "";
        ItemValue.text = "";
        SellItemExplain.text = "";
        SellItemValue.text = "";
    }

}
