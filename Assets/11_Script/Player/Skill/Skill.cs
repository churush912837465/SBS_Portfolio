using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected int _skillNum;           // ��ų num
    [SerializeField] protected string _skillName;       // ��ų �̸�
    [SerializeField] protected string _aniName;         // �ִϸ��̼� �̸�
    [SerializeField] protected float _minDamage;        // �ּ� �����
    [SerializeField] protected float _maxDamage;        // �ִ� �����
    [SerializeField] protected float _coolTime;         // ��Ÿ��
    [SerializeField] protected int _endSkillSecond;     // ��ų ������� �ð�
    [SerializeField] protected Sprite _icon;            // ��ų ������


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
    public virtual void SkillUse(Animator v_ani, Transform v_startPosi)     // �ִϸ��̼��� ���� �� ��ü�� �־������ (player�� animator)
    {
        v_ani.SetTrigger(_aniName);    // �ִϸ��̼� ����

        SkillBall obj = SkillBallPooling.instance.GetSkillBall(_skillNum); // SkillBall get
        obj.transform.position = v_startPosi.position;                     // ��ġ ����
    }


    
}
