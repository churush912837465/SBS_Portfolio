using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portion : Countable
{
    // portion 아이템 데이터베이스
    private PortionData _portionData;
 
    public Portion(ItemData itemData, CountableData countableData , PortionData portiondadta ) : base(itemData, countableData)
    {
        this._portionData = portiondadta;
    }

    public override Item CreateItem() 
    { 
        CountableData _deepCountable = new CountableData();
        _deepCountable.setCountableDataField(1 , _countableData.MaxAmount , _countableData.CoolTime);

        Item _returnPortion = new Portion(_itemData , _deepCountable , _portionData);
        
        return _returnPortion;
    }

    public override void ItemUse()
    {
        Debug.Log(this + " 사용합니다");

        // 플레이어의 HP 증가
        GameManager.instance.playerManager.UserPortion(_portionData.HealingAmont);
    }
}
