using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class SkillSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int SkillLv; // 스킬 레벨
    public int MaxSkillLv; //스킬 최대 레벨
    public string SkillName; // 스킬 이름
    public GameObject SkillExplainUI;
    public TMP_Text SkillExplainText;
    public TMP_Text CurrentSkillLvText;
    public TMP_Text CurrentSkillLvExplainUIText;
    public TMP_Text MaxSkillLvText;


    [TextArea]
    public string Explain; // 스킬 설명
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
        CurrentSkillLvExplainUIText.text = "티어: " + SkillLv;
        MaxSkillLvText.text = "최대: " + MaxSkillLv;
        SkillExplainUI.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData3)
    {
        SkillExplainUI.SetActive(false);
    }
}
