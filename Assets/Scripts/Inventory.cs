using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false; // 인벤토리 활성화 여부(true면 다른 행동, 키입력 멈춤)

    [SerializeField]
    private GameObject InventoryBase;
    [SerializeField]
    private GameObject SlotsParent;

    private Slot[] slots; //슬롯 배열

    public GameObject OtherUI;
    public GameObject QucikSoltUI;

    public PlayerMovement playermove;
    public Weapons weapon;
    public WeaponManager weaponManager;
    public QuickSlotControl QucikSlotCon;

    public GameObject ItemExplainUI; //마우스 닿으면 표시하는 설명 ui
    public Text ItemExplain;
    public Text ItemValue;
    public Text MoneyText;

    public int bulletCount; //총알 수
    public int Metal; //골드, 캡, 머니...

    void Start()
    {
        slots = SlotsParent.GetComponentsInChildren<Slot>();
        playermove = FindObjectOfType<PlayerMovement>();
        weapon = FindObjectOfType<Weapons>();
        weaponManager = FindObjectOfType<WeaponManager>();
    }

    void Update()
    {
        TryOpenInventory();

        if (Input.GetKey(KeyCode.Alpha1))
        {
            QucikSoltUI.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            QucikSoltUI.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            QucikSoltUI.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            QucikSoltUI.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            QucikSoltUI.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            QucikSoltUI.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.Alpha7))
        {
            QucikSoltUI.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.Alpha8))
        {
            QucikSoltUI.SetActive(true);
        }
        else if (Input.GetKey(KeyCode.Alpha9))
        {
            QucikSoltUI.SetActive(true);
        }
        else
        {
            QucikSoltUI.SetActive(false);
        }
    }

    private void TryOpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            MoneyText.text = "M: "+ Metal.ToString();
            inventoryActivated = !inventoryActivated;

            if(inventoryActivated)
            {
                
                OpenInventory();
                playermove.StopMove();
                weapon = FindObjectOfType<Weapons>();
                weaponManager.StopAtk();
            }
            else
            {
                ItemExplainOff();
                CloseInventory();
                playermove.StopMove();
                weapon = FindObjectOfType<Weapons>();
                weaponManager.StopAtk();
            }
        }
    }

    private void OpenInventory()
    {
        InventoryBase.SetActive(true);
        QucikSlotCon.isQucikSlotSwhichable = false; //인벤토리 열었을때 퀵슬롯 버튼 눌러도 사용 안되게 하고 지정만 가능하게
        OtherUI.SetActive(false);
    }

    private void CloseInventory()
    {
        InventoryBase.SetActive(false);
        QucikSlotCon.isQucikSlotSwhichable = true;
        OtherUI.SetActive(true);
    }

    public void AcquireItem(Item _item, int _count = 1) //1은 디폴트값
    {
        if(Item.ItemType.Equip != _item.itemType)
        {
            for(int i = 0; i< slots.Length; i++)
            {
                if(slots[i].item != null)
                {
                    if(slots[i].item.itemName == _item.itemName)
                    {
                        _count = _item.DropItemCount;
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i].item == null)
            {
                _count = _item.DropItemCount;
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }

    public void CheckBullet(Item _item) //인벤에 해당 탄약이 있나 체크
    {
        if (Item.ItemType.Equip != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        bulletCount = slots[i].itemCount;
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
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        weapon = FindObjectOfType<Weapons>();
                        slots[i].SetSlotCount(_count);
                        bulletCount = slots[i].itemCount;
                        Debug.Log("usebullet: "+bulletCount);
                        Debug.Log(slots[i].itemCount);
                        return;
                    }
                }
            }
        }
    }

    public int BulletCountReturn(int MaxRoundCount)
    {
        if(bulletCount >= MaxRoundCount)
        {
            return MaxRoundCount;
        }
        else
        {
            return bulletCount;
        }


    }

    public void InventoryQuickSlotSetting()
    {
        
    }

    public void ItemExplainOn(string _itemExp, string _itemValue)
    {
        ItemExplainUI.SetActive(true);
        ItemExplain.text = _itemExp;
        ItemValue.text = "가격: " + _itemValue;
    }

    public void ItemExplainOff()
    {
        ItemExplainUI.SetActive(false);
        ItemExplain.text = "";
        ItemValue.text = "";
    }

}
