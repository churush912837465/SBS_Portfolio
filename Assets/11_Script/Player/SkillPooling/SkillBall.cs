using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SkillBall : MonoBehaviour
{
    /// <summary>
    ///
    /// Skill Ball �� �⺻������ �پ��ִ�
    /// ���� SkillBall Obj
    ///     ���� 1 : ���� particle
    ///     ���� 2 : ���� particle
    ///     ���� 3 : �� particle
    ///     
    /// </summary>

    [SerializeField]
    private ParticleSystem[] _paricleSytem;
    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private int _skillBallIdx;
    [SerializeField]
    private int _endSkillSecond;

    private bool _onEnable = true;

    private int _startIdx = 0;
    private int _playIdx = 1;
    private int _endIdx = 2;

    // ������Ƽ
    public int SkillBallIdx { set { _skillBallIdx = value; } }
    public int EndSKillSecond { set { _endSkillSecond = value;  } }

    // �ʱ�ȭ
    public void InitParticle(GameObject[] _objArr) 
    {
        // ��ƼŬ �ý��� �ʱ�ȭ
        _paricleSytem = new ParticleSystem[_objArr.Length];

        for (int i = 0; i < _objArr.Length; i++) 
        {
            GameObject oo = Instantiate(_objArr[i]);
            _paricleSytem[i] = oo.GetComponent<ParticleSystem>();

            if (_paricleSytem[i] == null)
                return;

            _paricleSytem[i].transform.parent = this.transform;
            _paricleSytem[i].transform.localPosition =  Vector3.zero;
            _paricleSytem[i].Stop();
            _paricleSytem[i].gameObject.SetActive(false);
        }
        
    }

    // SKill Ball�� ������ ��
    private void OnEnable()
    {
        // ó�� ������ �� ����
        if (_onEnable)
        { 
            _onEnable = false;
            return;
        }

        // SKill Ball�� �߷� ����
        _rb = GetComponent<Rigidbody>();
        _rb.mass = 10f;

        StartParticle(_startIdx);       // 0��° ��ƼŬ ����
        StartParticle(_playIdx);        // 1��° ��ƼŬ ����

        // ���� Player�� ����Ǿ��ִ� second�� ������
        _endSkillSecond = GameManager.instance.playerManager.CurrSkill.EndSkillSecond;
    }

    // �浹 �˻�
    private void OnCollisionEnter(Collision collision)
    {
        // floor�� �浹
        if (collision.gameObject.CompareTag("Floor"))
        {
            StartParticle(_endIdx);                     // end particle ����
            Invoke("EndSkill", _endSkillSecond);        // N �� �� ��ų ��
        }

        // enemy�� �浹
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            StartParticle(_endIdx);                     // end particle ����
            Invoke("EndSkill", _endSkillSecond);        // N �� �� ��ų ��
        }
    }

    // ��ų�� ���� �� ����
    private void EndSkill() 
    {
        _rb.mass = 0f;                          // ���� 0
        SkillBallPooling.instance.ReturnSkillBall(this, _skillBallIdx);

        for (int i = 0; i < 3; i++) 
        {
            // ��� ��ƼŬ active �� false��
            EndParticle(i);
        }
    }

    // Particle ����
    private void StartParticle(int v_idx) 
    {
        if (_paricleSytem[v_idx] == null)
            return;

        _paricleSytem[v_idx].gameObject.SetActive(true);        // �ѱ�
        _paricleSytem[v_idx].Play();                            // ����
    }

    // Particle ������
    private void EndParticle(int v_idx) 
    {
        if (_paricleSytem[v_idx] == null)
            return;

        _paricleSytem[v_idx].gameObject.SetActive(false);       // ����
        _paricleSytem[v_idx].Stop();                            // ���߱�
    }
}
