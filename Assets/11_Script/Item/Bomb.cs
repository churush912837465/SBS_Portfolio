using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Countable
{
    [SerializeField]
    private BombData _bombdata;

    public Bomb(ItemData itemData, CountableData countableData, BombData bombData) : base(itemData, countableData)
    {
        this._bombdata = bombData;
    }

    public override Item CreateItem()
    {
        CountableData _deepCountable = new CountableData();
        _deepCountable.setCountableDataField(1, _countableData.MaxAmount, _countableData.CoolTime);

        Item _returnDomb = new Bomb(_itemData, _deepCountable, _bombdata);

        return _returnDomb;
    }
}
