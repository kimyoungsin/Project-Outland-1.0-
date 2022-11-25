using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public float SwitchDelay = 1f;
    public bool Switchable = true; // ���Ұ��� ���� true�� ����
    public GameObject[] QuickSlotWeapons;
    public Weapons[] weapons; // ��� ���⸦ ���ҷ� ������ �迭
    private Dictionary<string, Weapons> WeaponDictionary = new Dictionary<string, Weapons>();

    private string currentWeaponName; //���� ������ �̸�
    public GameObject CurrentWeapon; // ���� ����ִ� ����
    public Transform WeaponPosition;

    public PlayerMovement player;
    public Weapons weaponScript;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < weapons.Length; i++)
        {
            WeaponDictionary.Add(weapons[i].Name, weapons[i]);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if(Switchable == true)
        {   
            /*
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Switchable = false;
                Invoke("SwhichableOn", SwitchDelay);
                player.Anim.SetBool("isWalk", false);
                player.Anim.SetBool("isFist", true);
                QuickSlotWeapons[0].SetActive(true);
                QuickSlotWeapons[1].SetActive(false);
                QuickSlotWeapons[2].SetActive(false);
                weaponScript = FindObjectOfType<Weapons>();
                weaponScript.GunBulletCheck();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Switchable = false;
                Invoke("SwhichableOn", SwitchDelay);
                player.Anim.SetBool("isWalk", true);
                player.Anim.SetBool("isFist", false);
                QuickSlotWeapons[0].SetActive(false);
                QuickSlotWeapons[1].SetActive(true);
                QuickSlotWeapons[2].SetActive(false);
                weaponScript = FindObjectOfType<Weapons>();
                weaponScript.GunBulletCheck();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Switchable = false;
                Invoke("SwhichableOn", SwitchDelay);
                player.Anim.SetBool("isWalk", true);
                player.Anim.SetBool("isFist", false);

                QuickSlotWeapons[0].SetActive(false);
                QuickSlotWeapons[1].SetActive(false);
                QuickSlotWeapons[2].SetActive(true);
                weaponScript.GunBulletCheck();
                weaponScript = FindObjectOfType<Weapons>();
            }
            */
        }
        
    }
    void SwhichableOn()
    {
        Switchable = true;
    }

    public void SwhichWeapon(GameObject _weapon)
    {

        Switchable = false;
        Invoke("SwhichableOn", SwitchDelay);
        player.Anim.SetBool("isWalk", true);
        player.Anim.SetBool("isFist", false);
        QuickSlotWeapons[0].SetActive(false);
        QuickSlotWeapons[1].SetActive(false);
        QuickSlotWeapons[2].SetActive(false);
        Destroy(CurrentWeapon);
        CurrentWeapon = _weapon;
        CurrentWeapon = (GameObject)Instantiate(_weapon, WeaponPosition.transform);
        CurrentWeapon.transform.parent = gameObject.transform;
        weaponScript = FindObjectOfType<Weapons>();
        weaponScript.StopAtk();
    }
}
