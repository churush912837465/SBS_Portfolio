using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class Enemy : EnemyParent
{
    // EnemyFSM ��� �ް��־�� enemyFSM �迭�� ������ ���� �� ����
    // Enemy�� ������ �־�� �� ��
    // 1. Enemy DB
    // 2. FSM �迭


    public bool _onEnable = true;                   // onEnable üũ
    public bool _disEnable = true;                  // disEnable üũ

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
        if (_onEnable)           
        {
            _onEnable = false;
            return;
        }
        // �� ó�� pooling�� ���������, ��� OnEnable �Ǿ������� �����ϱ� ���ؼ�
        // flag ���� �ɾ����

        if (enemyMachine == null)
            Debug.LogWarning("Enemy�� HeadMachine�� null");

        _myEnemyHp = myEnemyDB.HP;              // hp ���� ����
        enemyMachine.SetState(enemyFSM[(int)Enemy_State.Tracking]);
        enemyMachine.H_Begin();                 // Mahine�� ����Ǿ� �ִ� ������ Begin �޼��� ����
        
        StartCoroutine(FSM_Run());              // �ڷ�ƾ���� �������� ����
    }

    public void OnDisable()                     // pool�� �� �� (������ ��)
    {
        if (_disEnable)
        {
            _disEnable = false;
            return;
        }
        // �� ó�� pooling�� ���������, ��� OnDisable �Ǿ������� �����ϱ� ���ؼ�
        // flag ���� �ɾ����

        Debug.Log("����");
        StopCoroutine(FSM_Run());               // ���� ���ư����ִ�  �ڷ�ƾ (update) ����
    }

    // �浹����
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))   // �Ѿ˿� ������
        {
            if (_myEnemyHp <= 0)
                return;

            Debug.Log("Enemy�� ���ݴ���");
            _myEnemyHp -= GameManager.instance.playerManager.GetEnemyBulletDamage();
            // Player�� �� Bullet �������� return

            StartCoroutine(damageText());               // ������ ǥ�� �ڷ�ƾ
            getDamagePlayer();                          // �÷��̾�� ���� �ִϸ��̼� ����                               

        }
    }

    void OnDrawGizmos()
    {
        if (myEnemyDB == null)
            return;

        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, myEnemyDB.Sight);
        // DrawSphere( �߽� ��ġ , ������ )
    }


}
