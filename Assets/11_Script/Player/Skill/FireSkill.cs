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

    //  생성자 , new 될 때 Init
    /*
    public FireSkill() : base()
    {
    }
    */

    protected override void Init()
    {
        this._SkillName = "종말의 날";
        this._aniName = "FireSKill";
        this._minDamage = 40f;
        this._maxDamage = 50f;
        this._coolTime = 3f;
        this._icon = sp;
    }

    public override void SkillUse(Animator ani , Transform v_startPosi) 
    {
        base.SkillUse(ani, v_startPosi);

        Debug.Log("FireSkill 사용합니다");

        // 스킬이 시작하는 위치 : v_startPosi
        GameObject gameObject = Instantiate(_fireBall, v_startPosi.position, Quaternion.identity);
    }
}
