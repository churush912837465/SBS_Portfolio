using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EearthSkill : Skill
{
    [Header("EearthSkill")]
    [SerializeField]
    private Sprite sp;

    //  생성자 , new 될 때 Init
    /*
    public WindSkill() : base()
    {
    }
    */

    protected override void Init()
    {
        this._skillNum = 2;
        this._skillName = "대지의 부름";
        this._aniName = "EearthSkill";
        this._minDamage = 40f;
        this._maxDamage = 50f;
        this._coolTime = 5f;
        this._endSkillSecond = 3;
        this._icon = sp;
    }

    public override void SkillUse(Animator ani, Transform v_startPosi) 
    {
        base.SkillUse(ani , v_startPosi);
    }

}
