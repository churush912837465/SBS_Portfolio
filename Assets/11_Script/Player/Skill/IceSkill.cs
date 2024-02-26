using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSkill : Skill
{
    [SerializeField]
    Sprite sp;

    //  ������ , new �� �� Init
    public IceSkill() : base()
    {
    }

    protected override void Init()
    {
        this._SkillName = "������ ��";
        this._aniName = "IceSkill";
        this._maxDamage = 50f;
        this._maxDamage = 60f;
        this._coolTime = 3f;
        this._icon = sp;
    }

    public override void SkillUse(Animator ani)
    {
        base.SkillUse(ani);
        Debug.Log("IceSkill ����մϴ�");
    }
}
