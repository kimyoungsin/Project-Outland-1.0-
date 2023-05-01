using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EXPBar : MonoBehaviour
{
    public Player player;

    public Image EXPmeterImage;

    public TMP_Text EXPText;

    void Start()
    {
        player = FindObjectOfType<Player>();
        EXPmeterImage = GetComponent<Image>();


    }

    // Update is called once per frame
    void Update()
    {
        EXPmeterImage.fillAmount = player.Exp / player.MaxExp;
        EXPText.text = "" + ((float)player.Exp);
    }
}
