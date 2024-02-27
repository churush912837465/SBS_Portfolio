using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EearthSkill : Skill
{
    [SerializeField]
    private Sprite sp;

    [SerializeField]
    GameObject _partiObj;
    [SerializeField]
    GameObject _cloneObj;

    [SerializeField]
    ParticleSystem _paricleSytem;

    //  생성자 , new 될 때 Init
    /*
    public WindSkill() : base()
    {
    }
    */

    protected override void Init()
    {
        this._SkillName = "대지의 부름";
        this._aniName = "EearthSkill";
        this._minDamage = 40f;
        this._maxDamage = 50f;
        this._coolTime = 5f;
        this._icon = sp;
    }

    public override void SkillUse(Animator ani, Transform v_startPosi)  // 위치 : 플레이어 위치
    {
        base.SkillUse(ani , v_startPosi);

        Debug.Log("EearthSkill 사용합니다");

        // 파티클 시스템 get
        _cloneObj = Instantiate(_partiObj);
        _paricleSytem = _cloneObj.GetComponent<ParticleSystem>();

        // 파티클 시스템 위치 조정
        _paricleSytem.transform.position          = v_startPosi.gameObject.transform.position + new Vector3(0,0.2f,0.5f);
        // 파티클 시스템 회전 조정
        _paricleSytem.transform.rotation          = v_startPosi.gameObject.transform.rotation;

        _paricleSytem.Play();
        Invoke("WaitAndDestory", 3f);       // 일정 시간 후 삭제

    }

    public void WaitAndDestory() 
    {
        Destroy(_cloneObj);
    }

}
