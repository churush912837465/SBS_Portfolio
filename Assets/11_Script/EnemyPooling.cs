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
        instance = this;                        // �̱��� 
    }

    private void Start()
    {
        enemies     = new List<List<EnemyParent>>();  // enemies �ʱ�ȭ
        earthEnemy  = new List<EnemyParent>();
        fireEnemy   = new List<EnemyParent>();
        iceEnemy    = new List<EnemyParent>();

        enemies.Add(earthEnemy);                // earhtEnemy List �߰�
        enemies.Add(fireEnemy);                 // fireEmeny List �߰�
        enemies.Add(iceEnemy);                  // iceEnemy List �߰�

        _initCnt = 1;
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
        EnemyParent enemy = null;

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

        enemy.gameObject.SetActive(true);       // �ѱ�
        return enemy.gameObject;
    }

    // return Enemy 
    public void returnEnemy(EnemyParent obj , int idx) 
    {
        if (obj == null)                         // Ȥ�� �� ����ó��
        {
            Debug.Log("EnemeyFSM ����");
            return;
        
        }

        obj.gameObject.transform.parent = enemyPool[idx];           // �θ� ����
        obj.gameObject.SetActive(false);                            // ���� -> Enemy�� OnDisable �����
        // �ش� List�� add
        enemies[idx].Add(obj);

    }

}
