using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSkill : Skill
{
    [SerializeField]
    Sprite sp;

    //  ������ , new �� �� Init
    public WindSkill() : base()
    {
    }

    protected override void Init()
    {
        this._SkillName = "Ȥ���� �θ�";
        this._aniName = "WindSkill";
        this._maxDamage = 40f;
        this._maxDamage = 50f;
        this._coolTime = 5f;
        this._icon = sp;
    }

    public override void SkillUse(Animator ani)
    {
        base.SkillUse(ani);
        Debug.Log("WindSkill ����մϴ�");
    }
}
