using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour
{
    ScrollRect scrollRect;
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }


    public void SetContentSize()
    {
        //호출 시 선택지 1칸 만큼의 길이(30)증가
        float width = scrollRect.content.sizeDelta.x;
        float height = scrollRect.content.sizeDelta.y;

        scrollRect.content.sizeDelta = new Vector2(width, height + 30.0f);
    }

}
