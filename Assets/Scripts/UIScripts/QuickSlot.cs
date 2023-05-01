using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuickSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item; //흭득한 아이템
    public int itemCount; //얻은 아이템 개수
    public Image itemImage; // 아이템의 이미지
    public WeaponManager theWeaponManager;
    public Player player;
    public Inventory theInventory;

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject CountImage;

    [SerializeField] private RectTransform basRect; // 인벤토리 베이스의 영역
    [SerializeField] RectTransform quickSlotBaseRect; // 퀵슬롯의 영역(content 오브젝트가 할당)

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
            selectedQuickSlotNum = 99; //99는 아무것도 선택 안함=안누르고 있는 상태
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

    public void AddItem(Item _item, int _count = 1) //인벤토리에 아이템 슬롯 추가
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
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                if (item.itemType == Item.ItemType.Equip)//무기면 장착
                {
                    //theWeaponManager.SwhichWeapon(item.WeaponStats);
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
