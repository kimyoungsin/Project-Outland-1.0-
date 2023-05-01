using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item; //판매중인 아이템
    public int itemCount; //얻은 아이템 개수
    public Image itemImage; // 아이템의 이미지
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



    public void AddItem(Item _item, int _count) //인벤토리에 아이템 슬롯 추가
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

    public void SetSlotCount(int _count) //해당 슬롯 아이템 갯수 업데이트
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
        {
            ClearSlot();
        }
    }

    private void ClearSlot() //해당 슬롯 하나 삭제
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        CountImage.SetActive(false);
    }

    //PointerEventData: 마우스 혹은 터치 입력 이벤트에 관한 정보들이 담겨 있다. (이벤트가 들어온 버튼, 클릭 수, 마우스 위치, 현재 마우스 움직이고 있는지 여부)
    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            theInventory = FindObjectOfType<Inventory>();
            Debug.Log("닿았다!!!!");
            if (item != null)
            {
                if (theInventory.Metal > item.Value) //보유 메탈량이 더 많을 경우
                {
                    SoundManager.SharedInstance.PlaySE("Shop_Buy");
                    theInventory.Metal -= item.Value;
                    theInventory.AcquireItem(item, item.DropItemCount); //인벤토리에 아이템 넣기
                    shopUI.ItemBuy(item, item.DropItemCount);
                    SetSlotCount(-1);
                    Debug.Log("아이템 구입.");


                }
                else
                {
                    SoundManager.SharedInstance.PlaySE("Shop_Buy_Fail");
                    Debug.Log("소지 메탈량이 부족하다 이야!!!");
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

    /* 백업용 OnPointerClick(PointerEventData eventData)
     public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                if (item.itemType == Item.ItemType.Equip)//무기면 장착
                {
                    theWeaponManager.SwhichWeapon(item.itemPrefab);
                }
                if (item.itemType == Item.ItemType.Used) //소모품이면 소모
                {
                    player.hp += item.HPEffect;
                    player.tp += item.TPEffect;
                    SetSlotCount(-1);
                    Debug.Log("소모품 사용했다!!");
                }
                else
                {
                }
            }
        }
    }
     */

}
