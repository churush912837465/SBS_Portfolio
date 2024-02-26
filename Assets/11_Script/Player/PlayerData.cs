using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData 
{
    // 필드
    [SerializeField]
    private float _hp;          // hp
    [SerializeField]
    private float _moveSpeed;   // 움직임 속도
    [SerializeField]
    private float _ATK;         // Attack , 공격력
    [SerializeField]
    private float _EXP;         // 경험치

    // 프로퍼티
    public float HP { get { return _hp; } set { _hp = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float ATK { get { return _ATK; } set { _ATK = value; } }
    public float EXP { get { return _EXP; } set { _EXP = value; } }

}
