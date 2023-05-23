using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    
    public enum ItemType
    {
        Equip,
        Used,
        Quest,
        Ingredient,
        Bullet,
        Metal,
        ETC
    }
    public enum WeaponTypes
    {
        None,
        Fist,
        Unarmd,
        Onehand,
        Twohand,
        OnehandGun,
        OnehandAutoGun,
        TwohandGun,
        TwohandAutoGun,
        DoubleGun,
        Gun_Tlet,
        HeavyArms

    }
    public int ID; //아이템 ID값
    public WeaponTypes weaponTypes; // 아이템이 무기일 경우 무기타입
    public string itemName; // 아이템 이름
    public ItemType itemType; // 아이템 유형
    public Sprite itemImage; // 아이템 이미지
    public GameObject itemPrefab; //아이템 프리팹
    public Weapons WeaponStats; //무기일 경우 무기 스크립트
    //public string weaponType; //무기 유형
    public int DropItemCount; //드랍하는 개수
    public int Value; // 아이템 가격
    [TextArea]
    public string Explain; // 아이템 설명

    //아래부턴 사용 아이템 사용 시 효과 정의
    public int HPEffect; //체력 증감 효과
    public int TPEffect; //TP 증감 효과
    public float MoveSpeedEfeect; //이속 증감효과


}
