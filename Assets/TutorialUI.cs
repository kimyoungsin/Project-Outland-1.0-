using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    public TMP_Text Tutorialext; // Ʃ�丮�� ui �ؽ�Ʈ
    public Animator UIani;
    public float UIHideSpeed = 8f; //UIǥ�� �� ������� �ð� ����

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
        Tutorialext.text = "'I'Ű�� ���� �κ��丮�� ������!";
        StartCoroutine(UIHide());
    }
    public void BattleModeText()
    {
        UIani.SetBool("isShow", true);
        Tutorialext.text = "���� ������/����ֱ�: 'T'Ű.";
        StartCoroutine(UIHide());
    }
    public void NPCDialogueText()
    {
        UIani.SetBool("isShow", true);
        Tutorialext.text = "NPC ��ó���� 'E'Ű�� ���� ��ȭ�� �õ��ϼ���!";
        StartCoroutine(UIHide());
    }
    public void ItemPickUpText()
    {
        UIani.SetBool("isShow", true);
        Tutorialext.text = "������ �ݱ�: 'E'Ű.";
        StartCoroutine(UIHide());
    }
    public void SprintText()
    {
        UIani.SetBool("isShow", true);
        Tutorialext.text = "'���� Shift'Ű�� ���� ü �̵��ϸ� SP�� �Ҹ��Ͽ� �޸� �� �ֽ��ϴ�!";
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
