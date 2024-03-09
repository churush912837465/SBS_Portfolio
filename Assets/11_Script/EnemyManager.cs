using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;    // ΩÃ±€≈Ê

    [SerializeField]
    List<GameObject> enemyObj;      // Enemy «¡∏Æ∆’
    [SerializeField]
    List<EnemyDB> enemyDBList;      // Enemy DB

    public int _enemyCnt;

    // «¡∑Œ∆€∆º
    public int enemyCnt { get => _enemyCnt; }

    private void Awake()
    {
        instance = this;
        _enemyCnt = enemyDBList.Count;
    }


    public GameObject returnEnemyObj(int idx)
    {
        return enemyObj[idx];
    }

    public EnemyDB returnEnemyDB(int idx)
    {
        return enemyDBList[idx];
    }
}
