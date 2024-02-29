using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkill : Skill
{
    [Header("FireSkill")]
    [SerializeField]
    private Sprite sp;

    //  생성자 , new 될 때 Init
    /*
    public FireSkill() : base()
    {
    }
    */

    protected override void Init()
    {
        this._skillNum = 1;
        this._skillName = "종말의 날";
        this._aniName = "FireSKill";
        this._minDamage = 40f;
        this._maxDamage = 50f;
        this._coolTime = 3f;
        this._endSkillSecond = 2;
        this._icon = sp;
    }

    public override void SkillUse(Animator ani , Transform v_startPosi) 
    {
        base.SkillUse(ani, v_startPosi);
    }
}
