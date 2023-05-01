using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item; //�Ǹ����� ������
    public int itemCount; //���� ������ ����
    public Image itemImage; // �������� �̹���
    public WeaponManager theWeaponManager;
    public Player player;
    public Inventory theInventory;
    public ShopUI shopUI;
    public NPCShopStockList stocklist;

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject CountImage;

    void Start()
    {
        theWeaponManager = FindObjectOfType<WeaponManager>();
        player = FindObjectOfType<Player>();
        theInventory = FindObjectOfType<Inventory>();
        shopUI = FindObjectOfType<ShopUI>();
    }

    void Update()
    {

    }


    private void SetColor(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }



    public void AddItem(Item _item, int _count) //�κ��丮�� ������ ���� �߰�
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

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            theInventory = FindObjectOfType<Inventory>();
            Debug.Log("��Ҵ�!!!!");
            if (item != null)
            {
                if (theInventory.Metal > item.Value) //���� ��Ż���� �� ���� ���
                {
                    SoundManager.SharedInstance.PlaySE("Shop_Buy");
                    theInventory.Metal -= item.Value;
                    theInventory.AcquireItem(item, item.DropItemCount); //�κ��丮�� ������ �ֱ�
                    shopUI.ItemBuy(item, item.DropItemCount);
                    SetSlotCount(-1);
                    Debug.Log("������ ����.");


                }
                else
                {
                    SoundManager.SharedInstance.PlaySE("Shop_Buy_Fail");
                    Debug.Log("���� ��Ż���� �����ϴ� �̾�!!!");
                }
            }
        }

    }

    public void OnPointerEnter(PointerEventData eventData2)
    {
        if (item != null)
        {
            shopUI.ItemExplainOn(item.Explain, item.Value.ToString());
        }
    }

    public void OnPointerExit(PointerEventData eventData3)
    {
        shopUI.ItemExplainOff();
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
