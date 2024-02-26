using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    // 최상위 아이템 데이터베이스
    protected ItemData _itemData;

    public ItemData itemData{ get => _itemData;  }

    // 생성자
    public Item(ItemData itemData)
    {
        this._itemData = itemData;
    }

    public abstract Item CreateItem();
}
