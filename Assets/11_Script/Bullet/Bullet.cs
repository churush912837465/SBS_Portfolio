using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    float myLifeTime; 

    private void OnEnable()     // 켜졋을 때 -> pool에서 빠져나와 실행되기 시작할 때
    {
        if (PlayerManager.Instance.MyBulletDb == null)              // 예외처리
            return;

        myLifeTime = PlayerManager.Instance.MyBulletDb.LifeTime;    // 생존시간 후에 return
        Invoke("getreTurnPool", myLifeTime);
        // 따로 변수 빼야지 다른 총의 cool Time을 가져오는 오류가 발생 안할듯?
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            // PlayerManager.Instance.MyBulletDb.AniName;
            getreTurnPool();
        }
    }
    void getreTurnPool()
    {
        GunSlinerBullet.Instance.returnBullet(this.gameObject);
    }

    /* 
    // 많은 도움이 된
    [SerializeField]
    BulletDB _bullet;

    public void InitBullet(BulletDB bullet)
    {
        _bullet = bullet;
    }

    public BulletDB bullet { get => _bullet;  }

    */
}
