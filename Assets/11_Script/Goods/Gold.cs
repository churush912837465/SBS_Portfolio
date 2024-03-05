using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Goods
{
    [SerializeField]
    Sprite _goldIcon;

    public void Start()
    {
        InitGoods();    // ������ �ʱ�ȭ
    }

    protected override void InitGoods()
    {
        this.name = this.ToString();
        this._count = 0;
        this._sp = _goldIcon;
    }
}
