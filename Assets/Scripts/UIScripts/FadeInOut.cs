using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public float Time = 0f;
    public float SetupTime = 1f; //얼마나 검정 상태로 있나 설정(기본 1f)
    public bool isPlaying = false;
    public GameObject FadeImage;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FadeOutStart");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine("FadeOutStart");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine("FadeInStart");
        }
    }
    public IEnumerator FadeOutStart()
    {
        FadeImage.SetActive(true);
        for (Time = 0f; Time < 0.5; Time += 0.01f)
        {
            Color c = FadeImage.GetComponent<Image>().color;
            c.a += Time;
            FadeImage.GetComponent<Image>().color = c;
            yield return null;

        }
        StartCoroutine("FadeInStart");
    }
    public IEnumerator FadeInStart()
    {
        FadeImage.SetActive(true);
        for (Time = 0.5f; Time > 0; Time -= 0.01f)
        {
            Color c = FadeImage.GetComponent<Image>().color;
            c.a -= Time;
            FadeImage.GetComponent<Image>().color = c;
            yield return null;
        }
    }



}
