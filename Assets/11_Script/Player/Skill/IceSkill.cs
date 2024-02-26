using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSkill : Skill
{
    [SerializeField]
    Sprite sp;

    //  생성자 , new 될 때 Init
    public IceSkill() : base()
    {
    }

    protected override void Init()
    {
        this._SkillName = "종말의 날";
        this._aniName = "IceSkill";
        this._maxDamage = 50f;
        this._maxDamage = 60f;
        this._coolTime = 3f;
        this._icon = sp;
    }

    public override void SkillUse(Animator ani)
    {
        base.SkillUse(ani);
        Debug.Log("IceSkill 사용합니다");
    }
}
