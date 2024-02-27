using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSkill : Skill
{
    [SerializeField]
    private Sprite sp;

    //  생성자 , new 될 때 Init
    /*
    public IceSkill() : base()
    {
    }
    */

    protected override void Init()
    {
        this._SkillName = "종말의 날";
        this._aniName = "WaterSkill";
        this._minDamage = 50f;
        this._maxDamage = 60f;
        this._coolTime = 3f;
        this._icon = sp;
    }

    public override void SkillUse(Animator ani, Transform v_startPosi)
    {
        base.SkillUse(ani, v_startPosi);

        Debug.Log("WaterSkill 사용합니다");

    }
}
