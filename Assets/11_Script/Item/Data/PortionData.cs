using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortionData
{
    // 필드
    [SerializeField]
    float _healingAmont;

    // 프로퍼티
    public float HealingAmont { get => _healingAmont; }

    // 값 세팅
    public void setPortionDataField(float heal) 
    {
        this._healingAmont = heal;
    }
    
}
