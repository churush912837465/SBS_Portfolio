using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipment : Item
{
    // ������
    [SerializeField]
    protected EquipmentData _equipmentData;

    // ������Ƽ
    public EquipmentData EquipmentData { get => _equipmentData; }

    public Equipment(ItemData itemData , EquipmentData equipmentData ) : base(itemData)
    {
        this._equipmentData = equipmentData;
    }

}
