using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public string Name; //���� �̸�
    public Sprite WeaponImage; //���� �̹���
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
    public string Weapon_Atk_Sound;
    public string Weapon_Reload_Sound;

    public enum WeaponTypes
    {
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
    public WeaponTypes weaponTypes;

    public Sprite WeaponSprite; //���� �̹��� ��������Ʈ
    public GameObject BulletPrefab; // ������ �Ѿ�
    public Transform FirePos; // ����ü ���� ��ġ
    public Bullet bullet; //�Ѿ� ��ũ��Ʈ(�Ʊ�)
    public PlayerMovement_FSM FSM;
    public GameObject enemyBulletPrefab; // ������ �Ѿ�(��)
    public Enemy_Bullet enemybullet; //�Ѿ� ��ũ��Ʈ(��)



    // Start is called before the first frame update
    void Start()
    {
        FSM = FindObjectOfType<PlayerMovement_FSM>();
    }

    public void BulletSetting()
    {
        bullet.BulletSpeed = BulletSpeed;
        bullet.BulletDamage = Damage;
    }

    public void EnemyBulletSetting()
    {
        Round = MaxRound;
        enemybullet.BulletSpeed = BulletSpeed;
        enemybullet.BulletDamage = Damage;
    }

}
