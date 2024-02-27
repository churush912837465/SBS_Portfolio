using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkill : Skill
{
    [Header("FireSkill")]
    [SerializeField]
    private Sprite sp;
    [SerializeField]
    GameObject _fireBall;

    //  ������ , new �� �� Init
    /*
    public FireSkill() : base()
    {
    }
    */

    protected override void Init()
    {
        this._SkillName = "������ ��";
        this._aniName = "FireSKill";
        this._minDamage = 40f;
        this._maxDamage = 50f;
        this._coolTime = 3f;
        this._icon = sp;
    }

    public override void SkillUse(Animator ani , Transform v_startPosi) 
    {
        base.SkillUse(ani, v_startPosi);

        Debug.Log("FireSkill ����մϴ�");

        // ��ų�� �����ϴ� ��ġ : v_startPosi
        GameObject gameObject = Instantiate(_fireBall, v_startPosi.position, Quaternion.identity);
    }
}
