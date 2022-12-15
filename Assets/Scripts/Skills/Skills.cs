using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "New Skill/Skill")]
public class Skills : ScriptableObject
{
    public int SkillLv; // ��ų ����
    public int MaxSkillLv; //��ų �ִ� ����
    public string SkillName; // ��ų �̸�

    [TextArea]
    public string Explain; // ��ų ����
}
