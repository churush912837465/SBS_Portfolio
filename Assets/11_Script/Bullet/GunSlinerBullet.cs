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
    /// pooling 방법 생각
    /// 1. bulletbase만 가지고 있다가 (X)
    ///     get : playerManager에서 index가 넘어오면 bulletDB의 List에 접근해서 해당 BulletDB를 붙여서 보내기 
    ///     return : BulletDB를 삭제 후 return
    ///     장점 : List가 한개 밖에 없다
    ///     단점 : 컴포넌트를 remove하면 비효율적인거 아닌가?
    ///     
    /// 2. 3종에 해당하는 총알을 미리 만들어둠 (0)
    ///     get : playerManager에서 index가 넘어오면 해당 총알의 List에 접근해서 List[0]을 보내기
    ///     return : 해당 List에 return
    ///     장점 : index로만 접근할 수 있어서 편하다
    ///     단점 : 초기 생성할 때 할게많다
    ///     
    /// pooling 구현 생각
    /// 1. List<> 배열로 구현 (x)
    ///     get : List[0];으로 get하면 됨
    ///           List에서 그 값을 remove 해야함
    ///     return : List.add (0)
    /// 2. Queue<> 큐로 구현
    ///     get : enqueue();
    ///     returb : dequeue(); 
    /// 
    /// </summary>

    [SerializeField]
    List<BulletDB> bulletDB;                // bullet Scriptable 데이터베이스
    // [0] handGun DB
    // [1] shootGun DB
    // [2] Lifle DB

    [SerializeField]
    GameObject _bulletbase;                 // 총알 base

    [SerializeField]
    Transform bulletPool;                   // base bullet이 담겨있을 상위 Pool

    [SerializeField]
    Queue<GameObject> bulletQueue;          // 총알이 담겨있을 큐

    private int initCreate;

    private void Awake()
    {
        Instance = this;                // 싱글톤

        bulletQueue = new Queue<GameObject>();
        bulletPool = gameObject.transform;
        initCreate = 5;
        initGunsBullet();               // 총알 초기 세팅
    }

    public void initGunsBullet() 
    {
        for (int i = 0; i < initCreate; i++)                    // 초기 생성 갯수만큼
        {
            bulletQueue.Enqueue(createBullet());
        }
    }

    // create에서 BulletDB를 add 해야함
    public GameObject createBullet() 
    {
        GameObject obj = Instantiate(_bulletbase);          // bulletBase 를 기반으로 오브젝트 새로 생성
        //obj.AddComponent<Bullet>();                       // 충돌감지를 위한 Bullet 스크립트 넣기
        obj.transform.parent = bulletPool;                  // bulletPool의 위치로          
        obj.gameObject.SetActive(false);                    // 꺼놓기

        return obj;
    }

    // bullet을 가져다 쓸 때
    public GameObject getBullet()
    {
        GameObject bullet = null;
        if (bulletQueue.Count > 0)
        {
            bullet = bulletQueue.Dequeue();                 // 큐에서 꺼내기 
        }
        else 
        {
            bullet = createBullet();                       // 새로 생성 , queue에 넣는건 return 할때
        }

        bullet.SetActive(true);                             // 켜기
        return bullet;
    }

    // bullet 을 pool로 돌려보낼 때
    public void returnBullet(GameObject returnBull)
    {
        returnBull.transform.parent = bulletPool;       // 부모지정
        returnBull.SetActive(false);                    // 끄기
        bulletQueue.Enqueue(returnBull);                // 큐에 넣기
    }

    public BulletDB retunBulletDB(int idx)              // idx에 해당하는 DB return
    {
        return bulletDB[idx];
    }
}

