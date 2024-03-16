using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
    [SerializeField]
    private float _additionalHp;    // �߰� ü��
    [SerializeField]
    private float _phyDefencity;    // ���� ����
    [SerializeField]
    private float _masicDefencity;  // ���� ����
    [SerializeField]
    private float _counter;         // ġ��Ÿ��

    // ������Ƽ
    public float HP { get { return _hp; } set { _hp = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float ATK { get { return _ATK; } set { _ATK = value; } }
    public float EXP { get { return _EXP; } set { _EXP = value; } }
    public float AdditionalHp { get { return _additionalHp; } set { _additionalHp = value; } }
    public float PhyDefencity { get {  return _phyDefencity; } set { _phyDefencity = value; } }
    public float MasicDefencity { get { return _masicDefencity;  } set { _masicDefencity = value;  } }
    public float Counter { get { return _counter; } set { _counter = value; } }
}
