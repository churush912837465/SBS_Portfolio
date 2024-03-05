using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected int _skillNum;           // 스킬 num
    [SerializeField] protected string _skillName;       // 스킬 이름
    [SerializeField] protected string _aniName;         // 애니메이션 이름
    [SerializeField] protected float _minDamage;        // 최소 대미지
    [SerializeField] protected float _maxDamage;        // 최대 대미지
    [SerializeField] protected float _coolTime;         // 쿨타임
    [SerializeField] protected int _endSkillSecond;     // 스킬 종료까지 시간
    [SerializeField] protected Sprite _icon;            // 스킬 아이콘


    public int SkillNum     { get => _skillNum; }
    public string SKillName { get => _skillName; }
    public string AniName   { get => _aniName; }
    public float MinDamage  { get => _minDamage; }
    public float MaxDamage  { get => _maxDamage; }
    public int EndSkillSecond { get => _endSkillSecond; }
    public float CoolTime   { get => _coolTime; }

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
    public virtual void SkillUse(Animator v_ani, Transform v_startPosi)     // 애니메이션을 실행 할 주체를 넣어줘야함 (player의 animator)
    {
        v_ani.SetTrigger(_aniName);    // 애니메이션 실행

        SkillBall obj = SkillBallPooling.instance.GetSkillBall(_skillNum); // SkillBall get
        obj.transform.position = v_startPosi.position;                     // 위치 조정
    }


    
}
