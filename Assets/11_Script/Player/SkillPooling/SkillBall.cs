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
    /// Skill Ball 에 기본적으로 붙어있는
    /// 상위 SkillBall Obj
    ///     하위 1 : 시작 particle
    ///     하위 2 : 실행 particle
    ///     하위 3 : 끝 particle
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

    // 프로퍼티
    public int SkillBallIdx { set { _skillBallIdx = value; } }
    public int EndSKillSecond { set { _endSkillSecond = value;  } }

    // 초기화
    public void InitParticle(GameObject[] _objArr) 
    {
        // 파티클 시스템 초기화
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

    // SKill Ball이 켜졌을 때
    private void OnEnable()
    {
        // 처음 켜졌을 때 방지
        if (_onEnable)
        { 
            _onEnable = false;
            return;
        }

        // SKill Ball의 중력 조정
        _rb = GetComponent<Rigidbody>();
        _rb.mass = 10f;

        StartParticle(_startIdx);       // 0번째 파티클 실행
        StartParticle(_playIdx);        // 1번째 파티클 실행

        // 현재 Player에 저장되어있는 second를 가져옴
        _endSkillSecond = GameManager.instance.playerManager.CurrSkill.EndSkillSecond;
    }

    // 충돌 검사
    private void OnCollisionEnter(Collision collision)
    {
        // floor와 충돌
        if (collision.gameObject.CompareTag("Floor"))
        {
            StartParticle(_endIdx);                     // end particle 실행
            Invoke("EndSkill", _endSkillSecond);        // N 초 뒤 스킬 끝
        }

        // enemy와 충돌
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            StartParticle(_endIdx);                     // end particle 실행
            Invoke("EndSkill", _endSkillSecond);        // N 초 뒤 스킬 끝
        }
    }

    // 스킬이 끝날 때 실행
    private void EndSkill() 
    {
        _rb.mass = 0f;                          // 질량 0
        SkillBallPooling.instance.ReturnSkillBall(this, _skillBallIdx);

        for (int i = 0; i < 3; i++) 
        {
            // 모든 파티클 active 를 false로
            EndParticle(i);
        }
    }

    // Particle 실행
    private void StartParticle(int v_idx) 
    {
        if (_paricleSytem[v_idx] == null)
            return;

        _paricleSytem[v_idx].gameObject.SetActive(true);        // 켜기
        _paricleSytem[v_idx].Play();                            // 실행
    }

    // Particle 끝내기
    private void EndParticle(int v_idx) 
    {
        if (_paricleSytem[v_idx] == null)
            return;

        _paricleSytem[v_idx].gameObject.SetActive(false);       // 끄기
        _paricleSytem[v_idx].Stop();                            // 멈추기
    }
}
