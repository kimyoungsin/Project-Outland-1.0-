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
    public int ID; //������ ID��
    public WeaponTypes weaponTypes; // �������� ������ ��� ����Ÿ��
    public string itemName; // ������ �̸�
    public ItemType itemType; // ������ ����
    public Sprite itemImage; // ������ �̹���
    public GameObject itemPrefab; //������ ������
    public Weapons WeaponStats; //������ ��� ���� ��ũ��Ʈ
    //public string weaponType; //���� ����
    public int DropItemCount; //����ϴ� ����
    public int Value; // ������ ����
    [TextArea]
    public string Explain; // ������ ����

    //�Ʒ����� ��� ������ ��� �� ȿ�� ����
    public int HPEffect; //ü�� ���� ȿ��
    public int TPEffect; //TP ���� ȿ��
    public float MoveSpeedEfeect; //�̼� ����ȿ��


}
