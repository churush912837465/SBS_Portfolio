using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Countable : Item
{
    // �� �� �ִ� ������ �����ͺ��̽�
    protected CountableData _countableData;

    // ������Ƽ
    public CountableData countableData { get => _countableData; }

    // ������
    public Countable(ItemData itemData, CountableData countableData) : base(itemData)
    {
        this._countableData = countableData;
    }

    public void AccessAndSetAmount(int v_amount)
    {
        _countableData.Amount = v_amount;
    }

    
    // ����Ŭ���� ���� ���� 
    public abstract void ItemUse();

}
