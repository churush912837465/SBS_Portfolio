using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipment : Item
{
    // 데이터
    [SerializeField]
    protected EquipmentData _equipmentData;

    // 프로퍼티
    public EquipmentData EquipmentData { get => _equipmentData; }

    public Equipment(ItemData itemData , EquipmentData equipmentData ) : base(itemData)
    {
        this._equipmentData = equipmentData;
    }

}
