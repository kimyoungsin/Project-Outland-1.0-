using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Inventory : MonoBehaviour
{
   

    [SerializeField]
    public GameObject InventoryBase;
    [SerializeField]
    public GameObject SlotsParent;

    public Slot[] slots; //���� �迭

    public GameObject OtherUI;
    public GameObject QucikSoltUI;

    public PlayerMovement_FSM FSM;
    public Weapons weapon;
    public WeaponManager weaponManager;
    public QuickSlotControl QucikSlotCon;

    public GameObject ItemExplainUI; //���콺 ������ ǥ���ϴ� ���� ui
    public Text ItemExplain;
    public Text ItemValue;
    public Text MoneyText;

    public int bulletCount; //�Ѿ� ��
    public int Metal; //���, ĸ, �Ӵ�...

    public Item iteminstance;

    void Start()
    {
        slots = SlotsParent.GetComponentsInChildren<Slot>();
        FSM = FindObjectOfType<PlayerMovement_FSM>();
        weapon = FindObjectOfType<Weapons>();
        weaponManager = FindObjectOfType<WeaponManager>();
    }

    void Update()
    {

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



    public void OpenInventory()
    {
        MoneyText.text = "M: " + Metal.ToString();
        weapon = FindObjectOfType<Weapons>();

        InventoryBase.SetActive(true);
        QucikSlotCon.isQucikSlotSwhichable = false; //�κ��丮 �������� ������ ��ư ������ ��� �ȵǰ� �ϰ� ������ �����ϰ�
        OtherUI.SetActive(false);
    }

    public void CloseInventory()
    {
        ItemExplainOff();
        weapon = FindObjectOfType<Weapons>();

        InventoryBase.SetActive(false);
        QucikSlotCon.isQucikSlotSwhichable = true;
        OtherUI.SetActive(true);
    }

    public void AcquireItem(Item _item, int _count) //1�� ����Ʈ��
    {
        if(Item.ItemType.Metal == _item.itemType)
        {
            Metal += _item.DropItemCount;
            return;
        }
        else
        {
            if (Item.ItemType.Equip != _item.itemType)
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i].item != null)
                    {
                        if (slots[i].item.itemName == _item.itemName)
                        {
                            _count = _item.DropItemCount;
                            slots[i].SetSlotCount(_count);
                            return;
                        }
                    }
                }
            }

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item == null)
                {
                    _count = _item.DropItemCount;
                    slots[i].AddItem(_item, _count);
                    return;
                }
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
