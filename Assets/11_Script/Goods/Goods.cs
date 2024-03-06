using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goods
{
    // 필드
    protected string _name;
    protected int _count;
    public Sprite _sp;

    // 생성자
    public Goods(string v_s , int v_c , Sprite V_sp) 
    {
        this._name = v_s;
        this._count = v_c;
        this._sp = V_sp;
    }

    public void AddGoods(int v_cnt)
    {
        _count += v_cnt;
    }
    public void SubbGoods(int v_cnt)
    {
        if (_count <= 0)
            return;

        _count -= v_cnt;
    }

}
