using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentData 
{
    // �ʵ�
    [SerializeField]
    float _addHP;

    // ������Ƽ
    public float addHP { get => _addHP; }

    public EquipmentData(float v_addHp ) 
    {
        this._addHP = v_addHp;
    }
}
