using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Skill : MonoBehaviour
{
    protected string _SkillName;      // 스킬 이름
    protected string _aniName;        // 애니메이션 이름
    protected float _minDamage;       // 최소 대미지
    protected float _maxDamage;       // 최대 대미지
    protected float _coolTime;        // 쿨타임
    protected Sprite _icon;           // 스킬 아이콘

    public string SKillName { get => _SkillName; }
    public string AniName { get => _aniName; }
    public float MinDamage { get => _minDamage; }
    public float MaxDamage { get => _maxDamage; }
    public float CoolTime { get => _coolTime; }

    /*
    public Skill() 
    {
        Init();
    }
    */

    
    private void Start()
    {
        Init();
    }
    
    protected abstract void Init();
    public virtual void SkillUse(Animator _ani , Transform v_startPosi)     // 애니메이션을 실행 할 주체를 넣어줘야함 (player의 animator)
    {
        
        // 스킬 사용 공통
        // 1. 애니메이션 실행
        _ani.SetBool(_aniName , true);

    }


}
