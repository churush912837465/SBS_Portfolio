using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesData
{
    // �ʵ�
    [SerializeField]
    float _physicDefencity;     // ���� ����
    [SerializeField]
    float _masicDefencity;      // ���� ����

    // ������Ƽ
    public float physicDefencity { get => _physicDefencity; }
    public float masicDefencity { get => _masicDefencity; }

    public void setClothesDataField(float v_phy , float v_mas) 
    { 
        this._physicDefencity = v_phy;
        this._masicDefencity = v_mas;
    }

}
