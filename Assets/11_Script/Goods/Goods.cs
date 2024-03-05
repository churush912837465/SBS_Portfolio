using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Goods : MonoBehaviour
{

    [SerializeField]
    protected string _name;
    [SerializeField]
    protected int _count;
    [SerializeField]
    protected Sprite _sp;

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

    // 각 재화 초기화
    protected abstract void InitGoods();

}
