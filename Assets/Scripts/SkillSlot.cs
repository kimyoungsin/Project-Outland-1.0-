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
    public Player player;

    [TextArea]
    public string Explain; // ��ų ����

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
                CurrentSkillLvExplainUIText.text = "Ƽ��: " + SkillLv;
                MaxSkillLvText.text = "�ִ�: " + MaxSkillLv;
                switch (SkillName)
                {
                    case "�":
                        Health += 5;
                        player.SkillLvUp(Health, SkillName);
                        break;
                    case "����ȣ��":
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
        CurrentSkillLvExplainUIText.text = "Ƽ��: " + SkillLv;
        MaxSkillLvText.text = "�ִ�: " + MaxSkillLv;
        SkillExplainUI.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData3)
    {
        SkillExplainUI.SetActive(false);
    }
}
