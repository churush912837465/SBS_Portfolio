using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Countable : Item
{
    // 셀 수 있는 아이템 데이터베이스
    protected CountableData _countableData;

    // 프로퍼티
    public CountableData countableData { get => _countableData; }

    // 생성자
    public Countable(ItemData itemData, CountableData countableData) : base(itemData)
    {
        this._countableData = countableData;
    }

    public void AccessAndSetAmount(int v_amount)
    {
        _countableData.Amount = v_amount;
    }

    
    // 하위클래스 에서 구현 
    public abstract void ItemUse();

}
