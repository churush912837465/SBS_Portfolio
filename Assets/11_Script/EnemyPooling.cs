using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class EnemyPooling : MonoBehaviour
{
    public static EnemyPooling instance;

    [SerializeField]
    List<Transform> enemyPool;           // enemy ������Ʈ�� ��� ��
    [SerializeField]
    List<List<Enemy>> enemies;
    // List[0] : List<Eenemy> Earth Enemy
    // List[1] : List<Enemy> Fire Enemy
    // List[2] : List<Enemy> ice Enemy

    List<Enemy> earthEnemy;
    List<Enemy> fireEnemy;
    List<Enemy> iceEnemy;

    [SerializeField]
    int _initCnt;

    private void Awake()
    {
        instance = this;                        // �̱��� 
    }

    private void Start()
    {
        enemies     = new List<List<Enemy>>();  // enemies �ʱ�ȭ
        earthEnemy  = new List<Enemy>();
        fireEnemy   = new List<Enemy>();
        iceEnemy    = new List<Enemy>();

        enemies.Add(earthEnemy);                // earhtEnemy List �߰�
        enemies.Add(fireEnemy);                 // fireEmeny List �߰�
        enemies.Add(iceEnemy);                  // iceEnemy List �߰�

        _initCnt = 3;
        initEnemy();
    }

    public void initEnemy() 
    {
        // Enemy Manager�� ������ �ǰ� ���� Init �� �����Ǿ�� ��
        if (EnemyManager.instance.enemyCnt == 0)                // ���� ������ ���� ��
            return;

        for (int i = 0; i < EnemyManager.instance.enemyCnt; i++) // ���� ������ŭ    
        {
            for(int j = 0; j < _initCnt; j ++) 
            {
                Enemy obj = createEnemis(i);
                enemies[i].Add(obj);                            // �ش� LIst�� Add �ϱ�
            }
        }
    }

    // create enemy
    public Enemy createEnemis(int idx) 
    {
        if (EnemyManager.instance == null)      // EnemyManager�� ���� �� ����ó��
            return null;

        // �ε����� �ش��ϴ� ������Ʈ �����ϱ� 
        Enemy newObj    = Instantiate(EnemyManager.instance.returnEnemyObj(idx)).GetComponent<Enemy>();
        // �ε����� �ش��ϴ� DB�� �����ϱ�
        EnemyDB newDB   = EnemyManager.instance.returnEnemyDB(idx);           

        newObj.getEnemyDB(newDB);               // ���� ������ Enemy�ȿ� DB�� �ֱ�
        newObj.gameObject.SetActive(false);                           // ����
        newObj.gameObject.transform.parent = enemyPool[idx].transform;  // �θ�����

        return newObj;
    }

    // get Enemy
    public GameObject getEnemy(int idx) 
    { 
        Enemy enemy = null;

        if (enemies[idx].Count > 0)             // enemies�� �ִ� List�ȿ� ������Ʈ�� �ִٸ�
        {
            // �ش� List���� �� ���� (queue�� dequee�� �ش�)
            enemy = enemies[idx][0];
            enemies[idx].RemoveAt(0);           // �� �� List���� �����ϱ�
        }
        else if (enemies[idx].Count <= 0 )      // ���� ������
        {
            enemy = createEnemis(idx);          // ���λ��� (��������)
        }

        enemy.gameObject.SetActive(true);   // �ѱ�
        return enemy.gameObject;
    }

    // return Enemy 
    public void returnEnemy() 
    {
    
    }

}
