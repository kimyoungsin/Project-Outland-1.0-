using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    public TMP_Text Tutorialext; // 튜토리얼 ui 텍스트
    public Animator UIani;
    public float UIHideSpeed = 8f; //UI표시 후 사라지는 시간 설정

    void Start()
    {
        UIani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InventoryText()
    {
        UIani.SetBool("isShow", true);
        Tutorialext.text = "'I'키를 눌러 인벤토리를 여세요!";
        StartCoroutine(UIHide());
    }
    public void BattleModeText()
    {
        UIani.SetBool("isShow", true);
        Tutorialext.text = "무기 꺼내기/집어넣기: 'T'키.";
        StartCoroutine(UIHide());
    }
    public void NPCDialogueText()
    {
        UIani.SetBool("isShow", true);
        Tutorialext.text = "NPC 근처에서 'E'키를 눌러 대화를 시도하세요!";
        StartCoroutine(UIHide());
    }
    public void ItemPickUpText()
    {
        UIani.SetBool("isShow", true);
        Tutorialext.text = "아이템 줍기: 'E'키.";
        StartCoroutine(UIHide());
    }
    public void SprintText()
    {
        UIani.SetBool("isShow", true);
        Tutorialext.text = "'왼쪽 Shift'키를 누른 체 이동하면 SP를 소모하여 달릴 수 있습니다!";
        StartCoroutine(UIHide());
    }

    public IEnumerator UIShow()
    {
        yield return null;
        UIani.SetBool("isShow", true);
        StartCoroutine(UIHide());
    }

    public IEnumerator UIHide()
    {
        yield return new WaitForSeconds(UIHideSpeed);
        UIani.SetBool("isShow", false);
    }
}
