using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TPBar : MonoBehaviour
{
    public Player playertp;

    public Image TPmeterImage;

    public TMP_Text TPText;

    void Start()
    {
        playertp = FindObjectOfType<Player>();
        TPmeterImage = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        TPmeterImage.fillAmount = playertp.tp / playertp.Maxtp;
        TPText.text = "" + ((int)playertp.tp);
    }
}
