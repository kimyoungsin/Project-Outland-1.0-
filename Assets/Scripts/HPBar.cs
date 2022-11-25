using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Player playerhp;

    public Image HPmeterImage;

    void Start()
    {
        HPmeterImage = GetComponent<Image>();

        playerhp = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        HPmeterImage.fillAmount = playerhp.hp / playerhp.Maxhp;
    }
}
