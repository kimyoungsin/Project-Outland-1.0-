using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false; // �κ��丮 Ȱ��ȭ ����(true�� �ٸ� �ൿ, Ű�Է� ����)

    [SerializeField]
    private GameObject InventoryBase;
    [SerializeField]
    private GameObject SlotsParent;

    private Slot[] slots; //���� �迭

    public GameObject OtherUI;
    public GameObject QucikSoltUI;

    public PlayerMovement playermove;
    public Weapons weapon;
    public WeaponManager weaponManager;
    public QuickSlotControl QucikSlotCon;

    public GameObject ItemExplainUI; //���콺 ������ ǥ���ϴ� ���� ui
    public Text ItemExplain;
    public Text ItemValue;
    public Text MoneyText;

    public int bulletCount; //�Ѿ� ��
    public int Metal; //���, ĸ, �Ӵ�...

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
        QucikSlotCon.isQucikSlotSwhichable = false; //�κ��丮 �������� ������ ��ư ������ ��� �ȵǰ� �ϰ� ������ �����ϰ�
        OtherUI.SetActive(false);
    }

    private void CloseInventory()
    {
        InventoryBase.SetActive(false);
        QucikSlotCon.isQucikSlotSwhichable = true;
        OtherUI.SetActive(true);
    }

    public void AcquireItem(Item _item, int _count = 1) //1�� ����Ʈ��
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

    public void CheckBullet(Item _item) //�κ��� �ش� ź���� �ֳ� üũ
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

    public void UseBullet(Item _item, int _count) //�κ����ִ� �ش� ź�� �Ҹ�
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
        ItemValue.text = "����: " + _itemValue;
    }

    public void ItemExplainOff()
    {
        ItemExplainUI.SetActive(false);
        ItemExplain.text = "";
        ItemValue.text = "";
    }

}
