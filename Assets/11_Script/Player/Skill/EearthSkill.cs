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

    //  ������ , new �� �� Init
    /*
    public WindSkill() : base()
    {
    }
    */

    protected override void Init()
    {
        this._SkillName = "������ �θ�";
        this._aniName = "EearthSkill";
        this._minDamage = 40f;
        this._maxDamage = 50f;
        this._coolTime = 5f;
        this._icon = sp;
    }

    public override void SkillUse(Animator ani, Transform v_startPosi)  // ��ġ : �÷��̾� ��ġ
    {
        base.SkillUse(ani , v_startPosi);

        Debug.Log("EearthSkill ����մϴ�");

        // ��ƼŬ �ý��� get
        _cloneObj = Instantiate(_partiObj);
        _paricleSytem = _cloneObj.GetComponent<ParticleSystem>();

        // ��ƼŬ �ý��� ��ġ ����
        _paricleSytem.transform.position          = v_startPosi.gameObject.transform.position + new Vector3(0,0.2f,0.5f);
        // ��ƼŬ �ý��� ȸ�� ����
        _paricleSytem.transform.rotation          = v_startPosi.gameObject.transform.rotation;

        _paricleSytem.Play();
        Invoke("WaitAndDestory", 3f);       // ���� �ð� �� ����

    }

    public void WaitAndDestory() 
    {
        Destroy(_cloneObj);
    }

}
