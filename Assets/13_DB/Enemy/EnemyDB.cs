using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Object/Enemy", order = int.MaxValue)]

public class EnemyDB : ScriptableObject
{
    [SerializeField]
    int _ID;
    [SerializeField]
    string _name;       // 이름
    [SerializeField]
    float _hp;          // hp
    [SerializeField]
    float _damage;      // 공격 데미지
    [SerializeField]
    float _speed;       // speed
    [SerializeField]
    float _sight;       // sight
    [SerializeField]
    bool _range;        // 원거리
    [SerializeField]
    bool _meleel;       // 근거리

    //  애니메이션
    [SerializeField]
    string _idleAni;    // 기본 애니메이션
    [SerializeField]
    string _attackAni;  // 공격 애니메이션 이름
    [SerializeField]
    string _getDamageAni;  // 데미지를 받는 애니메이선 ( Die 할 땐 이거 연결)
    [SerializeField]
    string _trackingAni;    // 걷는 애니메이션

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