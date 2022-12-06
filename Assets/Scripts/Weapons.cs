using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public string Name; //���� �̸�
    public float AttackSpeed; // ���� ������(����)
    public int Damage; // ���� �����
    public float Critical; // ���� ġ��ŸȮ��
    public float CriticalDamage; // ���� ġ��Ÿ ���� ���
    public float Range; // ���� ��ȿ ��Ÿ�(��Ÿ��� �Ѿ�� ���ط� ����)
    public float BulletSpeed; //ź��(����ü�ӵ�)
    public float RelordSpeed; // �ѱ� �������ӵ�
    public Item BulletType; // ź��(������ �κ��丮���� �ش� ź�� Ȯ��)
    public int MaxRound; // �ִ� źâ��
    public int Round; // ���� źâ��
    public int UseRound; // �����(=�߻���)źȯ��(Round - MaxRound)
    public float Spread; // ��ź��
    public float Weight; // ����

    static public Weapons instance;
    public string pistol_shot_sound;

    public enum WeaponTypes
    {
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
    public WeaponTypes weaponTypes;

    public Sprite WeaponSprite; //���� �̹��� ��������Ʈ
    public GameObject BulletPrefab; // ������ �Ѿ�
    public Transform FirePos; // ����ü ���� ��ġ
    public Bullet bullet;
    public PlayerMovement PlayerMovement;
    //public Slot theSlot;


    // Start is called before the first frame update
    void Start()
    {
        //theSlot = FindObjectOfType<Slot>();
        PlayerMovement = FindObjectOfType<PlayerMovement>();
    }

    public void BulletSetting()
    {
        bullet.BulletSpeed = BulletSpeed;
        bullet.BulletDamage = Damage;
    }

}
