using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TPBar : MonoBehaviour
{
    public Player playertp;

    public Image TPmeterImage;

    void Start()
    {
        TPmeterImage = GetComponent<Image>();

        playertp = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        TPmeterImage.fillAmount = playertp.tp / playertp.Maxtp;
    }
}
