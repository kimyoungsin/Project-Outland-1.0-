using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuickSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item; //ŉ���� ������
    public int itemCount; //���� ������ ����
    public Image itemImage; // �������� �̹���
    public WeaponManager theWeaponManager;
    public Player player;
    public Inventory theInventory;

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject CountImage;

    [SerializeField] private RectTransform basRect; // �κ��丮 ���̽��� ����
    [SerializeField] RectTransform quickSlotBaseRect; // �������� ����(content ������Ʈ�� �Ҵ�)

    public bool isQuickSlotSetting = false;
    public int selectedQuickSlotNum;

    void Start()
    {
        theWeaponManager = FindObjectOfType<WeaponManager>();
        player = FindObjectOfType<Player>();
        theInventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            selectedQuickSlotNum = 0;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            selectedQuickSlotNum = 1;
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            selectedQuickSlotNum = 2;
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            selectedQuickSlotNum = 3;
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            selectedQuickSlotNum = 4;
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            selectedQuickSlotNum = 5;
        }
        else if (Input.GetKey(KeyCode.Alpha7))
        {
            selectedQuickSlotNum = 6;
        }
        else if (Input.GetKey(KeyCode.Alpha8))
        {
            selectedQuickSlotNum = 7;
        }
        else if (Input.GetKey(KeyCode.Alpha9))
        {
            selectedQuickSlotNum = 8;
        }
        else
        {
            selectedQuickSlotNum = 99; //99�� �ƹ��͵� ���� ����=�ȴ����� �ִ� ����
        }
    }

    public void InventoryQuickSlotSetting()
    {

    }

    private void SetColor(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }

    public void AddItem(Item _item, int _count = 1) //�κ��丮�� ������ ���� �߰�
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        if (item.itemType != Item.ItemType.Equip)
        {
            CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            CountImage.SetActive(false);
        }

        SetColor(1);
    }

    public void SetSlotCount(int _count) //�ش� ���� ������ ���� ������Ʈ
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
        {
            ClearSlot();
        }
    }

    private void ClearSlot() //�ش� ���� �ϳ� ����
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        CountImage.SetActive(false);
    }

    //PointerEventData: ���콺 Ȥ�� ��ġ �Է� �̺�Ʈ�� ���� �������� ��� �ִ�. (�̺�Ʈ�� ���� ��ư, Ŭ�� ��, ���콺 ��ġ, ���� ���콺 �����̰� �ִ��� ����)
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                if (item.itemType == Item.ItemType.Equip)//����� ����
                {
                    //theWeaponManager.SwhichWeapon(item.WeaponStats);
                }
                if (item.itemType == Item.ItemType.Used) //�Ҹ�ǰ�̸� �Ҹ�
                {
                    player.hp += item.HPEffect;
                    player.tp += item.TPEffect;
                    SetSlotCount(-1);
                    Debug.Log("�Ҹ�ǰ ����ߴ�!!");
                }
                else
                {
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData2)
    {
        if (item != null)
        {
            theInventory.ItemExplainOn(item.Explain, item.Value.ToString());
        }
    }

    public void OnPointerExit(PointerEventData eventData3)
    {
        theInventory.ItemExplainOff();
    }

    /* ����� OnPointerClick(PointerEventData eventData)
     public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                if (item.itemType == Item.ItemType.Equip)//����� ����
                {
                    theWeaponManager.SwhichWeapon(item.itemPrefab);
                }
                if (item.itemType == Item.ItemType.Used) //�Ҹ�ǰ�̸� �Ҹ�
                {
                    player.hp += item.HPEffect;
                    player.tp += item.TPEffect;
                    SetSlotCount(-1);
                    Debug.Log("�Ҹ�ǰ ����ߴ�!!");
                }
                else
                {
                }
            }
        }
    }
     */

}
