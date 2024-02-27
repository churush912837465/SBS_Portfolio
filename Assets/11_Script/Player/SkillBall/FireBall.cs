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
 
        #region ��ƼŬ �ý��� getComponent

        GameObject oo = Instantiate(_partiObj[0]);
        _paricleSytem[0] = oo.GetComponent<ParticleSystem>();

        GameObject oo1 = Instantiate(_partiObj[1]);
        _paricleSytem[1] = oo1.GetComponent<ParticleSystem>();

        GameObject oo2 = Instantiate(_partiObj[2]);
        _paricleSytem[2] = oo2.GetComponent<ParticleSystem>();

        #endregion

        // ��ġ ����
        for (int i = 0; i < _paricleSytem.Length; i++) 
        {
            // ��ƼŬ �ý��� ��ġ ����
            _paricleSytem[i].transform.parent = this.gameObject.transform;
            _paricleSytem[i].transform.localPosition = Vector3.zero;
        }

        _paricleSytem[0].Play();
        _paricleSytem[1].Play();
        _paricleSytem[2].Stop();
        
    }

    // �浹����
    public void OnCollisionEnter(Collision collision)
    {
        // fireBall : Enemy�� ������ ��
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            // enemy get Damage -> enemy ���� ����
            EndFireBallAlive();
        }
            
        // fireBall : �ٴ��� ������ �� �ı�
        if (collision.gameObject.CompareTag("Floor"))
        {
            EndFireBallAlive();
        }
    }

    public void EndFireBallAlive() 
    {
        _paricleSytem[1].Stop();            // play ��ƼŬ ���߱�
        _paricleSytem[2].Play();            // end ��ƼŬ ����
        Invoke("WaitAndDestory", 1f);       // ���� �ð� �� ����
    }

    public void WaitAndDestory() 
    {
        Destroy(this.gameObject);
    }
    
}
