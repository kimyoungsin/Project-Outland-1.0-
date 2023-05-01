using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPBar : MonoBehaviour
{
    public Player playerhp;

    public Image HPmeterImage;

    public TMP_Text HpText;

    void Start()
    {
        playerhp = FindObjectOfType<Player>();
        HPmeterImage = GetComponent<Image>();

   
    }

    // Update is called once per frame
    void Update()
    {
        HPmeterImage.fillAmount = playerhp.hp / playerhp.Maxhp;
        HpText.text = "" + ((int)playerhp.hp);
    }
}
