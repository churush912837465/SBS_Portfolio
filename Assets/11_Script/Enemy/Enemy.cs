using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyManager
{
    // EnemeyManaher�� ��� �ް��־�� enemyFSM �迭�� ������ ���� �� ����
    // 

    [SerializeField]
    EnemyDB myDB;

    // �����ڷ� DB �ֱ�
    public Enemy(EnemyDB myDB) 
    {
        this.myDB = myDB; 
    }

    public void Start()
    {
        FSM_Init();                 //FSM �ʱ�ȭ (��ó�� 1ȸ�� ��.)
        enemySetIdle();             // ���� ���¸� idle�� ����

        enemyMachine.H_Begin();     // Mahine�� ����Ǿ� �ִ� ������ Begin �޼��� ����
    }
    public void Update() 
    {
        enemyMachine.H_Run();       // Machine�� ����Ǿ� �ִ� ������ Run() �޼��� ���� 
    }

    /*
    public void OnEnable()          // pool���� ���������� �� (������ ��)
    {
        StartCoroutine(FSM_Run());
        enemySetIdle();             // ���� ���¸� idle�� ����

    }

    public void OnDisable()         // pool�� �� �� (������ ��)
    {
        StopCoroutine(FSM_Run());    // ���� ���ư����ִ�  �ڷ�ƾ (update) ����    
    }

    IEnumerator FSM_Run() 
    {
        enemyMachine.H_Run();       // update�� ��� ���� ������ run�� �������� ����
        yield return null;
    }
    */
}
