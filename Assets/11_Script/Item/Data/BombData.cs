using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombData
{
    // 필드
    [SerializeField]
    float _damage;
    [SerializeField]
    float _forceAmount;

    // 프로퍼티
    public float Damage { get => _damage; }
    public float ForceAmount { get => _forceAmount;}

    // 값 세팅
    public BombData(float v_damage, float v_forc) 
    {
        this._damage = v_damage;
        this._forceAmount = v_forc;
    }

}
