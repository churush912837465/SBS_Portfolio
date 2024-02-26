using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    // �ֻ��� ������ �����ͺ��̽�
    protected ItemData _itemData;

    public ItemData itemData{ get => _itemData;  }

    // ������
    public Item(ItemData itemData)
    {
        this._itemData = itemData;
    }

    public abstract Item CreateItem();
}
