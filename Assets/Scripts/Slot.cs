using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item; //흭득한 아이템
    public int itemCount; //얻은 아이템 개수
    public Image itemImage; // 아이템의 이미지
    public WeaponManager theWeaponManager;
    public Player player;
    public Inventory theInventory;
    public QuickSlotControl QuickSlotControl;
    public QuickSlot[] QuickSlotList;

    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject CountImage;

    public bool isSlotUse = true; //인벤에서 슬롯 눌러서 템 장착 등 행동 가능 여부
    public int selectedQuickSlotNum;

    void Start()
    {
        theWeaponManager = FindObjectOfType<WeaponManager>();
        player = FindObjectOfType<Player>();
        theInventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        //Debug.Log(selectedQuickSlotNum);
        if (Input.GetKey(KeyCode.Alpha1))
        {
            selectedQuickSlotNum = 0;
            isSlotUse = false;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            selectedQuickSlotNum = 1;
            isSlotUse = false;
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            selectedQuickSlotNum = 2;
            isSlotUse = false;
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            selectedQuickSlotNum = 3;
            isSlotUse = false;
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            selectedQuickSlotNum = 4;
            isSlotUse = false;
        }
        else if (Input.GetKey(KeyCode.Alpha6))
        {
            selectedQuickSlotNum = 5;
            isSlotUse = false;
        }
        else if (Input.GetKey(KeyCode.Alpha7))
        {
            selectedQuickSlotNum = 6;
            isSlotUse = false;
        }
        else if (Input.GetKey(KeyCode.Alpha8))
        {
            selectedQuickSlotNum = 7;
            isSlotUse = false;
        }
        else if (Input.GetKey(KeyCode.Alpha9))
        {
            selectedQuickSlotNum = 8;
            isSlotUse = false;
        }
        else
        {
            selectedQuickSlotNum = 99; //99는 아무것도 선택 안함=안누르고 있는 상태
            isSlotUse = true;
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

        if(item.itemType != Item.ItemType.Equip)
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

        if(itemCount <= 0)
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
            
            Debug.Log("닿았다!!!!");
            if (item != null)
            {
                if (item.itemType == Item.ItemType.Equip)//무기면 장착
                {
                    QuickSlotList = FindObjectsOfType<QuickSlot>();
                    switch (selectedQuickSlotNum)
                    {
                        case 0:
                            QuickSlotList[8].AddItem(item, 1);
                            Debug.Log("퀵슬롯 1에 무기 할당");
                            break;

                        case 1:
                            QuickSlotList[7].AddItem(item, 1);
                            Debug.Log("퀵슬롯 2에 무기 할당");
                            break;
                        case 2:
                            QuickSlotList[6].AddItem(item, 1);
                            Debug.Log("퀵슬롯 3에 무기 할당");
                            break;
                        case 3:
                            QuickSlotList[5].AddItem(item, 1);
                            Debug.Log("퀵슬롯 4에 무기 할당");
                            break;
                        case 4:
                            QuickSlotList[4].AddItem(item, 1);
                            Debug.Log("퀵슬롯 5에 무기 할당");
                            break;
                        case 5:
                            QuickSlotList[3].AddItem(item, 1);
                            Debug.Log("퀵슬롯 6에 무기 할당");
                            break;
                        case 6:
                            QuickSlotList[2].AddItem(item, 1);
                            Debug.Log("퀵슬롯 7에 무기 할당");
                            break;
                        case 7:
                            QuickSlotList[1].AddItem(item, 1);
                            Debug.Log("퀵슬롯 8에 무기 할당");
                            break;
                        case 8:
                            QuickSlotList[0].AddItem(item, 1);
                            Debug.Log("퀵슬롯 9에 무기 할당");
                            break;

                        default:
                            theWeaponManager.SwhichWeapon(item.itemPrefab);
                            Debug.Log("아이템 장착");
                            break;



                    }



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
