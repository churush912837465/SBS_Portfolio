using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentData 
{
    // 필드
    [SerializeField]
    float _addHP;

    // 프로퍼티
    public float addHP { get => _addHP; }

    public void setEquipDataField(float v_addHp ) 
    {
        this._addHP = v_addHp;
    }
}
