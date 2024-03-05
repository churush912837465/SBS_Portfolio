using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shilling : Goods
{
    [SerializeField]
    Sprite _shillingIcon;

    public void Start()
    {
        InitGoods();    // 아이템 초기화
    }

    protected override void InitGoods()
    {
        this.name = this.ToString();
        this._count = 0;
        this._sp = _shillingIcon;
    }
}
