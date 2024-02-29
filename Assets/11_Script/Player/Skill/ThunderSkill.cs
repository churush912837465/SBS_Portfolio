using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSkill : Skill
{
    [Header("ThunderSkill")]
    [SerializeField]
    private Sprite sp;

    //  ������ , new �� �� Init
    /*
    public ThunderSkill() : base()
    {
    }
    */

    protected override void Init()
    {
        this._skillNum = 0;
        this._skillName = "õ��";
        this._aniName = "ThunderSkill";
        this._minDamage = 70f;
        this._maxDamage = 100f;
        this._coolTime = 10f;
        this._endSkillSecond = 2;
        this._icon = sp;
    }

    public override void SkillUse(Animator ani, Transform v_startPosi)
    {
        base.SkillUse(ani, v_startPosi);
    }
}
