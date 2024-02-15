using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class EnemyPooling : MonoBehaviour
{
    public static EnemyPooling instance;

    [SerializeField]
    List<Transform> enemyPool;           // enemy 오브젝트가 담길 곳
    [SerializeField]
    List<List<EnemyParent>> enemies;
    // List[0] : List<Eenemy> Earth Enemy
    // List[1] : List<Enemy> Fire Enemy
    // List[2] : List<Enemy> ice Enemy

    List<EnemyParent> earthEnemy;
    List<EnemyParent> fireEnemy;
    List<EnemyParent> iceEnemy;

    [SerializeField]
    int _initCnt;

    private void Awake()
    {
        instance = this;                        // 싱글톤 
    }

    private void Start()
    {
        enemies     = new List<List<EnemyParent>>();  // enemies 초기화
        earthEnemy  = new List<EnemyParent>();
        fireEnemy   = new List<EnemyParent>();
        iceEnemy    = new List<EnemyParent>();

        enemies.Add(earthEnemy);                // earhtEnemy List 추가
        enemies.Add(fireEnemy);                 // fireEmeny List 추가
        enemies.Add(iceEnemy);                  // iceEnemy List 추가

        _initCnt = 1;
        initEnemy();
    }

    public void initEnemy() 
    {
        // Enemy Manager가 생성이 되고 나서 Init 이 생성되어야 함
        if (EnemyManager.instance.enemyCnt == 0)                // 몬스터 종류가 없을 때
            return;

        for (int i = 0; i < EnemyManager.instance.enemyCnt; i++) // 몬스터 종류만큼    
        {
            for(int j = 0; j < _initCnt; j ++) 
            {
                Enemy obj = createEnemis(i);
                enemies[i].Add(obj);                            // 해당 LIst에 Add 하기
            }
        }
    }

    // create enemy
    public Enemy createEnemis(int idx) 
    {
        if (EnemyManager.instance == null)      // EnemyManager가 없을 때 예외처리
            return null;

        // 인덱스에 해당하는 오브젝트 생성하기 
        Enemy newObj    = Instantiate(EnemyManager.instance.returnEnemyObj(idx)).GetComponent<Enemy>();
        // 인덱스에 해당하는 DB에 접근하기
        EnemyDB newDB   = EnemyManager.instance.returnEnemyDB(idx);           

        newObj.getEnemyDB(newDB);               // 새로 생성된 Enemy안에 DB를 넣기
        newObj.gameObject.SetActive(false);                           // 끄기
        newObj.gameObject.transform.parent = enemyPool[idx].transform;  // 부모지정

        return newObj;
    }

    // get Enemy
    public GameObject getEnemy(int idx) 
    {
        EnemyParent enemy = null;

        if (enemies[idx].Count > 0)             // enemies에 있는 List안에 오브젝트가 있다면
        {
            // 해당 List에서 값 빼기 (queue의 dequee에 해당)
            enemy = enemies[idx][0];
            enemies[idx].RemoveAt(0);           // 뺀 값 List에서 삭제하기
        }
        else if (enemies[idx].Count <= 0 )      // 값이 없으면
        {
            enemy = createEnemis(idx);          // 새로생성 (켜져있음)
        }

        enemy.gameObject.SetActive(true);       // 켜기
        return enemy.gameObject;
    }

    // return Enemy 
    public void returnEnemy(EnemyParent obj , int idx) 
    {
        if (obj == null)                         // 혹시 모를 예외처리
        {
            Debug.Log("EnemeyFSM 오류");
            return;
        
        }

        obj.gameObject.transform.parent = enemyPool[idx];           // 부모 설정
        obj.gameObject.SetActive(false);                            // 끄기 -> Enemy의 OnDisable 실행됨
        // 해당 List에 add
        enemies[idx].Add(obj);

    }

}
