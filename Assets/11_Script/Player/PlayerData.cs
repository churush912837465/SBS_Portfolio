using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData 
{
    // �ʵ�
    [SerializeField]
    private float _hp;          // hp
    [SerializeField]
    private float _moveSpeed;   // ������ �ӵ�
    [SerializeField]
    private float _ATK;         // Attack , ���ݷ�
    [SerializeField]
    private float _EXP;         // ����ġ

    // ������Ƽ
    public float HP { get { return _hp; } set { _hp = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float ATK { get { return _ATK; } set { _ATK = value; } }
    public float EXP { get { return _EXP; } set { _EXP = value; } }

}
