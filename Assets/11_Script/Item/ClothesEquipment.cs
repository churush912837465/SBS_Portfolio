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
        // ��� �������� ���� �����͸� return �ص���
        return new ClothesEquipment(_itemData , _equipmentData , _clothsData);
    }
}
