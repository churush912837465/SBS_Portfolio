using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    EnemyDB myDB;

    // �����ڷ� DB �ֱ�
    public Enemy(EnemyDB myDB) 
    {
        this.myDB = myDB; 
    }
}
