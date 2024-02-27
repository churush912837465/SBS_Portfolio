using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Skill : MonoBehaviour
{
    protected string _SkillName;      // ��ų �̸�
    protected string _aniName;        // �ִϸ��̼� �̸�
    protected float _minDamage;       // �ּ� �����
    protected float _maxDamage;       // �ִ� �����
    protected float _coolTime;        // ��Ÿ��
    protected Sprite _icon;           // ��ų ������

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
    public virtual void SkillUse(Animator _ani , Transform v_startPosi)     // �ִϸ��̼��� ���� �� ��ü�� �־������ (player�� animator)
    {
        
        // ��ų ��� ����
        // 1. �ִϸ��̼� ����
        _ani.SetBool(_aniName , true);

    }


}
