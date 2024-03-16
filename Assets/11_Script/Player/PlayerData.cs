using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
    [SerializeField]
    private float _additionalHp;    // 추가 체력
    [SerializeField]
    private float _phyDefencity;    // 물리 방어력
    [SerializeField]
    private float _masicDefencity;  // 마법 방어력
    [SerializeField]
    private float _counter;         // 치명타력

    // 프로퍼티
    public float HP { get { return _hp; } set { _hp = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float ATK { get { return _ATK; } set { _ATK = value; } }
    public float EXP { get { return _EXP; } set { _EXP = value; } }
    public float AdditionalHp { get { return _additionalHp; } set { _additionalHp = value; } }
    public float PhyDefencity { get {  return _phyDefencity; } set { _phyDefencity = value; } }
    public float MasicDefencity { get { return _masicDefencity;  } set { _masicDefencity = value;  } }
    public float Counter { get { return _counter; } set { _counter = value; } }
}
