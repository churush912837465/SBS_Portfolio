using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkill : Skill
{
    [SerializeField]
    Sprite sp;

    //  생성자 , new 될 때 Init
    public FireSkill() : base()
    {
    }

    protected override void Init()
    {
        this._SkillName = "종말의 날";
        this._aniName = "FireSKill";
        this._maxDamage = 40f;
        this._maxDamage = 50f;
        this._coolTime = 3f;
        this._icon = sp;
    }

    public override void SkillUse(Animator ani) 
    {
        base.SkillUse(ani);
        Debug.Log("FireSkill 사용합니다");
    }
}
