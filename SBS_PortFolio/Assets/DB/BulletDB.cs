using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Bullet Data", menuName = "Scriptable Object/Bullet", order = int.MaxValue)]

public class BulletDB : ScriptableObject
{
    [SerializeField]
    string _bulletName;     // 핸드건, 샷건, 라이플 총 3개 이름
    [SerializeField]
    string _aniName;        // 애니메이션 이름
    [SerializeField]
    float _minDamage;       // 최소 대미지
    [SerializeField]
    float _maxDamage;       // 최대 대미지
    [SerializeField]
    float _coolTime;        // 쿨타임'
    [SerializeField]
    float _bulletSpeed;     // 총알 속도
    [SerializeField]
    float _lifeTime;         // 생존 속도 (총알이 얼마만큼 생존하다가 Destroy될지)

    public string BulletName { get => _bulletName;  }
    public string AniName { get => _aniName; }
    public float MinDamage { get => _minDamage;  }
    public float MaxDamage { get => _maxDamage; }
    public float CoolTime { get => _coolTime; }
    public float BulletSpeed { get => _bulletSpeed; }
    public float LifeTime { get => _lifeTime; }

}
