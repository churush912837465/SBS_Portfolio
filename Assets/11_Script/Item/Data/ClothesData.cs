using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesData
{
    // 필드
    [SerializeField]
    float _physicDefencity;     // 물리 방어력
    [SerializeField]
    float _masicDefencity;      // 마법 방어력

    // 프로퍼티
    public float physicDefencity { get => _physicDefencity; }
    public float masicDefencity { get => _masicDefencity; }

    public void setClothesDataField(float v_phy , float v_mas) 
    { 
        this._physicDefencity = v_phy;
        this._masicDefencity = v_mas;
    }

}
