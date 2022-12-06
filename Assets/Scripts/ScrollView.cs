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
        //ȣ�� �� ������ 1ĭ ��ŭ�� ����(30)����
        float width = scrollRect.content.sizeDelta.x;
        float height = scrollRect.content.sizeDelta.y;

        scrollRect.content.sizeDelta = new Vector2(width, height + 30.0f);
    }

}
