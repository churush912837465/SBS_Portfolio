using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Diagnostics.Tracing;

public class GunSlinerBullet : MonoBehaviour
{
    public static GunSlinerBullet Instance;

    /// <summary>
    /// pooling ��� ����
    /// 1. bulletbase�� ������ �ִٰ� (X)
    ///     get : playerManager���� index�� �Ѿ���� bulletDB�� List�� �����ؼ� �ش� BulletDB�� �ٿ��� ������ 
    ///     return : BulletDB�� ���� �� return
    ///     ���� : List�� �Ѱ� �ۿ� ����
    ///     ���� : ������Ʈ�� remove�ϸ� ��ȿ�����ΰ� �ƴѰ�?
    ///     
    /// 2. 3���� �ش��ϴ� �Ѿ��� �̸� ������ (0)
    ///     get : playerManager���� index�� �Ѿ���� �ش� �Ѿ��� List�� �����ؼ� List[0]�� ������
    ///     return : �ش� List�� return
    ///     ���� : index�θ� ������ �� �־ ���ϴ�
    ///     ���� : �ʱ� ������ �� �ҰԸ���
    ///     
    /// pooling ���� ����
    /// 1. List<> �迭�� ���� (x)
    ///     get : List[0];���� get�ϸ� ��
    ///           List���� �� ���� remove �ؾ���
    ///     return : List.add (0)
    /// 2. Queue<> ť�� ����
    ///     get : enqueue();
    ///     returb : dequeue(); 
    /// 
    /// </summary>

    [SerializeField]
    List<BulletDB> bulletDB;                // bullet Scriptable �����ͺ��̽�
    // [0] handGun DB
    // [1] shootGun DB
    // [2] Lifle DB

    [SerializeField]
    GameObject _bulletbase;                 // �Ѿ� base

    [SerializeField]
    Transform bulletPool;                   // base bullet�� ������� ���� Pool

    [SerializeField]
    Queue<GameObject> bulletQueue;          // �Ѿ��� ������� ť

    private int initCreate;

    private void Awake()
    {
        Instance = this;                // �̱���

        bulletQueue = new Queue<GameObject>();
        bulletPool = gameObject.transform;
        initCreate = 5;
        initGunsBullet();               // �Ѿ� �ʱ� ����
    }

    public void initGunsBullet() 
    {
        for (int i = 0; i < initCreate; i++)                    // �ʱ� ���� ������ŭ
        {
            bulletQueue.Enqueue(createBullet());
        }
    }

    // create���� BulletDB�� add �ؾ���
    public GameObject createBullet() 
    {
        GameObject obj = Instantiate(_bulletbase);          // bulletBase �� ������� ������Ʈ ���� ����
        //obj.AddComponent<Bullet>();                       // �浹������ ���� Bullet ��ũ��Ʈ �ֱ�
        obj.transform.parent = bulletPool;                  // bulletPool�� ��ġ��          
        obj.gameObject.SetActive(false);                    // ������

        return obj;
    }

    // bullet�� ������ �� ��
    public GameObject getBullet()
    {
        GameObject bullet = null;
        if (bulletQueue.Count > 0)
        {
            bullet = bulletQueue.Dequeue();                 // ť���� ������ 
        }
        else 
        {
            bullet = createBullet();                       // ���� ���� , queue�� �ִ°� return �Ҷ�
        }

        bullet.SetActive(true);                             // �ѱ�
        return bullet;
    }

    // bullet �� pool�� �������� ��
    public void returnBullet(GameObject returnBull)
    {
        returnBull.transform.parent = bulletPool;       // �θ�����
        returnBull.SetActive(false);                    // ����
        bulletQueue.Enqueue(returnBull);                // ť�� �ֱ�
    }

    public BulletDB retunBulletDB(int idx)              // idx�� �ش��ϴ� DB return
    {
        return bulletDB[idx];
    }
}

