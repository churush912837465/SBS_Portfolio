using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSkill : Skill
{
    [Header("WaterSkill")]
    [SerializeField]
    private Sprite sp;

    //  ������ , new �� �� Init
    /*
    public IceSkill() : base()
    {
    }
    */

    protected override void Init()
    {
        this._skillNum = 3;
        this._skillName = "������ ��";
        this._aniName = "WaterSkill";
        this._minDamage = 50f;
        this._maxDamage = 60f;
        this._coolTime = 3f;
        this._endSkillSecond = 2;
        this._icon = sp;
    }

    public override void SkillUse(Animator ani, Transform v_startPosi)
    {
        base.SkillUse(ani, v_startPosi);
    }
}
