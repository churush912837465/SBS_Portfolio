using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Object/Enemy", order = int.MaxValue)]

public class EnemyDB : ScriptableObject
{
    [SerializeField]
    int _ID;
    [SerializeField]
    string _name;       // �̸�
    [SerializeField]
    float _hp;          // hp
    [SerializeField]
    float _damage;      // ���� ������
    [SerializeField]
    float _speed;       // speed
    [SerializeField]
    float _sight;       // sight
    [SerializeField]
    bool _range;        // ���Ÿ�
    [SerializeField]
    bool _meleel;       // �ٰŸ�

    //  �ִϸ��̼�
    [SerializeField]
    string _idleAni;    // �⺻ �ִϸ��̼�
    [SerializeField]
    string _attackAni;  // ���� �ִϸ��̼� �̸�
    [SerializeField]
    string _getDamageAni;  // �������� �޴� �ִϸ��̼� ( Die �� �� �̰� ����)
    [SerializeField]
    string _trackingAni;    // �ȴ� �ִϸ��̼�

    // state
    public int ID { get => _ID; }
    public string Name { get => _name; }
    public float HP { get => _hp; }
    public float Damage { get => _damage; }
    public float Sight { get => _sight; }
    public float Speed { get => _speed; }
    public bool Range { get => _range; }
    public bool Melee { get => _meleel; }

    // Animation
    public string IdleAni { get => _idleAni; }
    public string AttackAni { get => _attackAni; }
    public string GetDamageAni { get => _getDamageAni; }
    public string TrackingAni { get => _trackingAni; }

}