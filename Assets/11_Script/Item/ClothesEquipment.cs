using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesEquipment : Equipment
{
    [SerializeField]
    private ClothesData _clothsData;

    public ClothesEquipment(ItemData itemData, EquipmentData equipmentData , ClothesData clothsData) : base(itemData, equipmentData)
    {
        _clothsData = clothsData;
    }

    public override Item CreateItem()
    {
        // 장비 아이템은 같은 데이터를 return 해도됨
        return new ClothesEquipment(_itemData , _equipmentData , _clothsData);
    }
}
