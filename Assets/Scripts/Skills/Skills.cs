using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "New Skill/Skill")]
public class Skills : ScriptableObject
{
    public int SkillLv; // 스킬 레벨
    public int MaxSkillLv; //스킬 최대 레벨
    public string SkillName; // 스킬 이름

    [TextArea]
    public string Explain; // 스킬 설명
}
