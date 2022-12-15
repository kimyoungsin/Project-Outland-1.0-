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
    public Player player;

    [TextArea]
    public string Explain; // 스킬 설명

    public float Health = 0;
    public float AbdominalBreathing = 0;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    
    void Update()
    {
        CurrentSkillLvText.text = "" + SkillLv;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(player.PerkPoint > 0)
        {
            if(SkillLv < MaxSkillLv)
            {
                player.PerkPoint -= 1;
                SkillLv += 1;
                CurrentSkillLvExplainUIText.text = "티어: " + SkillLv;
                MaxSkillLvText.text = "최대: " + MaxSkillLv;
                switch (SkillName)
                {
                    case "운동":
                        Health += 5;
                        player.SkillLvUp(Health, SkillName);
                        break;
                    case "복식호흡":
                        AbdominalBreathing += 5;
                        player.SkillLvUp(AbdominalBreathing, SkillName);
                        break;
                    default:
                        break;
                }

            }
            else
            {

            }

        }
    }

    public void OnPointerEnter(PointerEventData eventData2)
    {
        SkillExplainUI.transform.position = (this.transform.position + new Vector3(200f, -40f));
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
