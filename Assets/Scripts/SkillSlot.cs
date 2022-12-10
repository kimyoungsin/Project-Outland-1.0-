using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class SkillSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int SkillLv; // ��ų ����
    public int MaxSkillLv; //��ų �ִ� ����
    public string SkillName; // ��ų �̸�
    public GameObject SkillExplainUI;
    public TMP_Text SkillExplainText;
    public TMP_Text CurrentSkillLvText;
    public TMP_Text CurrentSkillLvExplainUIText;
    public TMP_Text MaxSkillLvText;


    [TextArea]
    public string Explain; // ��ų ����
    void Start()
    {
        
    }

    
    void Update()
    {
        CurrentSkillLvText.text = "" + SkillLv;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData2)
    {
        SkillExplainUI.transform.position = (this.transform.position + new Vector3(180f, -30f));
        SkillExplainText.text = Explain;
        CurrentSkillLvExplainUIText.text = "Ƽ��: " + SkillLv;
        MaxSkillLvText.text = "�ִ�: " + MaxSkillLv;
        SkillExplainUI.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData3)
    {
        SkillExplainUI.SetActive(false);
    }
}
