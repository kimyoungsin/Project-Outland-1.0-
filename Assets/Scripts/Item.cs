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
        TwohandGun,
        DoubleGun,
        Gun_Tlet,
        HeavyArms

    }

    public WeaponTypes weaponTypes; // �������� ������ ��� ����Ÿ��
    public string itemName; // ������ �̸�
    public ItemType itemType; // ������ ����
    public Sprite itemImage; // ������ �̹���
    public GameObject itemPrefab; //������ ������
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
