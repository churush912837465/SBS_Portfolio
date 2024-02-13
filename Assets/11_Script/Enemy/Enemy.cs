using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : EnemyFSM
{
    // EnemyFSM ��� �ް��־�� enemyFSM �迭�� ������ ���� �� ����
    // Enemy�� ������ �־�� �� ��
    // 1. Enemy DB
    // 2. FSM �迭

    public bool flag = true;                    // onEnable üũ

    public void getEnemyDB(EnemyDB DB) 
    {
        _myDB = DB; 
    }

    public void Awake()
    {
        FSM_Init();                             // FSM �ʱ�ȭ (��ó�� 1ȸ�� ��.)
    }
    
    public void OnEnable()                      // pool���� ���������� �� (������ ��)
    {
        if (flag)
        {
            flag = false;
            return;
        }
        // pooling ���� ���� instantiate�ϴ� �ֵ��� ó�� onEnable ���¸� üũ�� �ʿ� x
        // pooling �� �� flag�� false�� ������

        if (enemyMachine == null)
            Debug.LogWarning("Enemy�� HeadMachine�� null");

        enemyMachine.SetState(enemyFSM[(int)Enemy_State.Tracking]);
        enemyMachine.H_Begin();                 // Mahine�� ����Ǿ� �ִ� ������ Begin �޼��� ����
        
        StartCoroutine(FSM_Run());              // �ڷ�ƾ���� �������� ����
    }

    public void OnDisable()                     // pool�� �� �� (������ ��)
    {
        StopCoroutine(FSM_Run());               // ���� ���ư����ִ�  �ڷ�ƾ (update) ����    
    }

    IEnumerator FSM_Run() 
    {
        while (true) 
        {
            enemyMachine.H_Run();               // update�� ��� ���� ������ run�� �������� ����
            yield return null;
        }
    }

    void OnDrawGizmos()
    {
        if (myDB == null)
            return;

        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, myDB.Sight);
        // DrawSphere( �߽� ��ġ , ������ )
    }


}
