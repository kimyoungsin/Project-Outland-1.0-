using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotControl : MonoBehaviour
{
    [SerializeField] public QuickSlot[] quickSlots; //Äü½½·Ôµé(9°³)
    [SerializeField] private Transform tf_parent; //Äü½½·ÔµéÀÇ ºÎ¸ð ¿ÀºêÁ§Æ®

    private int selectedSlot; //¼±ÅÃµÈ Äü½½·Ô ÀÎµ¦½º(0~8)
    [SerializeField] private GameObject selectedImage; //¼±ÅÃµÈ Äü½½·Ô ÀÌ¹ÌÁö

    [SerializeField]
    private WeaponManager theWeaponManager;

    public bool isQucikSlotSwhichable = true; //true¸é ¹öÆ° ´©¸¦ ½Ã ÇØ´ç Äü½½·Ô ÅÛ »ç¿ë

    void Start()
    {
        quickSlots = tf_parent.GetComponentsInChildren<QuickSlot>();
        selectedSlot = 0;
        theWeaponManager = FindObjectOfType<WeaponManager>();
    }

    void Update()
    {
        TryInputNumber();
    }

    private void TryInputNumber()
    {
        if(isQucikSlotSwhichable == true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeSlot(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeSlot(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeSlot(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ChangeSlot(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                ChangeSlot(4);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                ChangeSlot(5);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                ChangeSlot(6);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                ChangeSlot(7);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                ChangeSlot(8);
            }
        }
        
    }

    
    private void ChangeSlot(int _num)
    {
        SelectedSlot(_num);
        Execute();
    }

    private void SelectedSlot(int _num)
    {
        selectedSlot = _num; //¼±ÅÃµÈ ½½·Ô
        selectedImage.transform.position = quickSlots[selectedSlot].transform.position;


    }

    private void Execute()
    {
        if(quickSlots[selectedSlot].item != null)
        {
            if(quickSlots[selectedSlot].item.itemType == Item.ItemType.Equip)
            {
                theWeaponManager.SwhichWeapon(quickSlots[selectedSlot].item.itemPrefab);
            }
            else if(quickSlots[selectedSlot].item.itemType == Item.ItemType.Used)
            {

            }
            else
            {

            }
        }


    }


}
