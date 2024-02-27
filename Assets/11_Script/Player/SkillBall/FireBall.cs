using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class FireBall : MonoBehaviour
{
    [SerializeField]
    GameObject[] _partiObj;

    [SerializeField]
    ParticleSystem[] _paricleSytem;

    void Start()
    {
        _paricleSytem  = new ParticleSystem[_partiObj.Length];
 
        #region 파티클 시스템 getComponent

        GameObject oo = Instantiate(_partiObj[0]);
        _paricleSytem[0] = oo.GetComponent<ParticleSystem>();

        GameObject oo1 = Instantiate(_partiObj[1]);
        _paricleSytem[1] = oo1.GetComponent<ParticleSystem>();

        GameObject oo2 = Instantiate(_partiObj[2]);
        _paricleSytem[2] = oo2.GetComponent<ParticleSystem>();

        #endregion

        // 위치 조정
        for (int i = 0; i < _paricleSytem.Length; i++) 
        {
            // 파티클 시스템 위치 조정
            _paricleSytem[i].transform.parent = this.gameObject.transform;
            _paricleSytem[i].transform.localPosition = Vector3.zero;
        }

        _paricleSytem[0].Play();
        _paricleSytem[1].Play();
        _paricleSytem[2].Stop();
        
    }

    // 충돌감지
    public void OnCollisionEnter(Collision collision)
    {
        // fireBall : Enemy를 만났을 때
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            // enemy get Damage -> enemy 에서 구현
            EndFireBallAlive();
        }
            
        // fireBall : 바닥을 만났을 때 파괴
        if (collision.gameObject.CompareTag("Floor"))
        {
            EndFireBallAlive();
        }
    }

    public void EndFireBallAlive() 
    {
        _paricleSytem[1].Stop();            // play 파티클 멈추기
        _paricleSytem[2].Play();            // end 파티클 시작
        Invoke("WaitAndDestory", 1f);       // 일정 시간 후 삭제
    }

    public void WaitAndDestory() 
    {
        Destroy(this.gameObject);
    }
    
}
