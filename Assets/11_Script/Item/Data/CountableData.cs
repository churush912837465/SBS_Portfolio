using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountableData 
{
    // �ʵ�
    [SerializeField]
    int _amount;
    [SerializeField]
    int _maxAmount;
    [SerializeField]
    int _coolTime;


    // ������Ƽ
    public int Amount { get => _amount; set { _amount = value; } }
    public int MaxAmount { get => _maxAmount; }
    public int CoolTime { get => _coolTime; }

    // �ʵ� ���� (�ʱ� amout�� �׻� 1)
    public CountableData(int amount , int maxAmount, int coolTime) 
    {
        this._amount = amount;
        this._maxAmount = maxAmount;
        this._coolTime = coolTime;
    }

    
}
