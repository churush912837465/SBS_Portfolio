using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortionData
{
    // �ʵ�
    [SerializeField]
    float _healingAmont;

    // ������Ƽ
    public float HealingAmont { get => _healingAmont; }

    // �� ����
    public void setPortionDataField(float heal) 
    {
        this._healingAmont = heal;
    }
    
}
