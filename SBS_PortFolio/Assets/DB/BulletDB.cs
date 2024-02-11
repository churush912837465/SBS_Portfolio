using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Bullet Data", menuName = "Scriptable Object/Bullet", order = int.MaxValue)]

public class BulletDB : ScriptableObject
{
    [SerializeField]
    string _bulletName;     // �ڵ��, ����, ������ �� 3�� �̸�
    [SerializeField]
    string _aniName;        // �ִϸ��̼� �̸�
    [SerializeField]
    float _minDamage;       // �ּ� �����
    [SerializeField]
    float _maxDamage;       // �ִ� �����
    [SerializeField]
    float _coolTime;        // ��Ÿ��'
    [SerializeField]
    float _bulletSpeed;     // �Ѿ� �ӵ�
    [SerializeField]
    float _lifeTime;         // ���� �ӵ� (�Ѿ��� �󸶸�ŭ �����ϴٰ� Destroy����)

    public string BulletName { get => _bulletName;  }
    public string AniName { get => _aniName; }
    public float MinDamage { get => _minDamage;  }
    public float MaxDamage { get => _maxDamage; }
    public float CoolTime { get => _coolTime; }
    public float BulletSpeed { get => _bulletSpeed; }
    public float LifeTime { get => _lifeTime; }

}
