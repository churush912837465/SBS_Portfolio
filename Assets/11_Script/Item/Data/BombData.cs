using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombData
{
    // �ʵ�
    [SerializeField]
    float _damage;
    [SerializeField]
    float _forceAmount;

    // ������Ƽ
    public float Damage { get => _damage; }
    public float ForceAmount { get => _forceAmount;}

    // �� ����
    public BombData(float v_damage, float v_forc) 
    {
        this._damage = v_damage;
        this._forceAmount = v_forc;
    }

}
