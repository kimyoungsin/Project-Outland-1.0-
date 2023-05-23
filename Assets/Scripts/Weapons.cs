using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public string Name; //무기 이름
    public Sprite WeaponImage; //무기 이미지
    public float AttackSpeed; // 공격 딜레이(공속)
    public int Damage; // 무기 대미지
    public float Critical; // 무기 치명타확률
    public float CriticalDamage; // 무기 치명타 피해 계수
    public float Range; // 무기 유효 사거리(사거리를 넘어가면 피해량 감소)
    public float BulletSpeed; //탄속(투사체속도)
    public float RelordSpeed; // 총기 재장전속도
    public Item BulletType; // 탄종(장착시 인벤토리에서 해당 탄약 확인)
    public int MaxRound; // 최대 탄창수
    public int Round; // 현재 탄창수
    public int UseRound; // 사용한(=발사한)탄환수(Round - MaxRound)
    public float Spread; // 산탄도
    public float Weight; // 무게

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

    public Sprite WeaponSprite; //무기 이미지 스프라이트
    public GameObject BulletPrefab; // 생성할 총알
    public Transform FirePos; // 투사체 생성 위치
    public Bullet bullet; //총알 스크립트(아군)
    public PlayerMovement_FSM FSM;
    public GameObject enemyBulletPrefab; // 생성할 총알(적)
    public Enemy_Bullet enemybullet; //총알 스크립트(적)



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
