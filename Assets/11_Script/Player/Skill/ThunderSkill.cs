using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSkill : Skill
{
    [SerializeField]
    Sprite sp;

    //  생성자 , new 될 때 Init
    public ThunderSkill() : base()
    {
    }

    protected override void Init()
    {
        this._SkillName = "천벌";
        this._aniName = "ThunderSkill";
        this._maxDamage = 70f;
        this._maxDamage = 100f;
        this._coolTime = 10f;
        this._icon = sp;
    }

    public override void SkillUse(Animator ani)
    {
        base.SkillUse(ani);
        Debug.Log("ThunderSkill 사용합니다");
    }
}
