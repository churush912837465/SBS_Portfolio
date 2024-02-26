using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessory : Equipment
{
    [SerializeField]
    private AccessoryData _accessoryData;

    // 프로퍼티
    public AccessoryData accessoryData { get => _accessoryData; }

    public Accessory(ItemData itemData, EquipmentData equipmentData , AccessoryData accessoryData) : base(itemData, equipmentData)
    {
        this._accessoryData = accessoryData;
    }

    public override Item CreateItem()
    {
        return new Accessory(_itemData , _equipmentData , _accessoryData);
    }
}
