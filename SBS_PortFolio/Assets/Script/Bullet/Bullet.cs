using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    float myLifeTime; 

    private void OnEnable()     // �Ѡ��� �� -> pool���� �������� ����Ǳ� ������ ��
    {
        if (PlayerManager.Instance.MyBulletDb == null)              // ����ó��
            return;

        myLifeTime = PlayerManager.Instance.MyBulletDb.LifeTime;    // �����ð� �Ŀ� return
        Invoke("getreTurnPool", myLifeTime);
        // ���� ���� ������ �ٸ� ���� cool Time�� �������� ������ �߻� ���ҵ�?
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
    // ���� ������ ��
    [SerializeField]
    BulletDB _bullet;

    public void InitBullet(BulletDB bullet)
    {
        _bullet = bullet;
    }

    public BulletDB bullet { get => _bullet;  }

    */
}
