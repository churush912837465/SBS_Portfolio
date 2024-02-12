using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    EnemyDB myDB;

    // 생성자로 DB 넣기
    public Enemy(EnemyDB myDB) 
    {
        this.myDB = myDB; 
    }
}
