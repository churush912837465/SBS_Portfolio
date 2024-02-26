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

    private bool _onEnable  = true;                   // onEnable üũ
    private bool _disEnable = true;                  // disEnable üũ

    public void getEnemyDB(EnemyDB DB) 
    {
        _myEnemyDB = DB; 
    }

    public void Awake()
    {
        FSM_Init();                             // FSM �ʱ�ȭ (��ó�� 1ȸ�� ��)
    }
    
    public void OnEnable()                      // pool���� ���������� �� (������ ��)
    {
        // �� ó�� pooling�� ���������, ��� OnEnable �Ǿ������� �����ϱ� ���ؼ�
        // flag ���� �ɾ����
        if (_onEnable)           
        {
            _onEnable = false;
            return;
        }

        _damageText.text = "";
        _myEnemyHp = myEnemyDB.HP;              // hp ���� ����

        // FSM ����
        enemyMachine.SetState(enemyFSM[(int)Enemy_State.Tracking]);
        enemyMachine.H_Begin();                 // Mahine�� ����Ǿ� �ִ� ������ Begin �޼��� ����
        StartCoroutine(FSM_Run());              // �ڷ�ƾ���� �������� ����
    }

    public void OnDisable()                     // pool�� �� �� (������ ��)
    {
        // �� ó�� pooling�� ���������, ��� OnDisable �Ǿ������� �����ϱ� ���ؼ�
        // flag ���� �ɾ����
        if (_disEnable)
        {
            _disEnable = false;
            return;
        }

        StopAllCoroutines();
        //StopCoroutine(FSM_Run());               // ���� ���ư����ִ�  �ڷ�ƾ (update) ����
    }

    // �浹����
    private void OnCollisionEnter(Collision collision)
    {
        // �Ѿ˿� ������
        if (collision.gameObject.CompareTag("Skill"))   
        {
            if (_myEnemyHp <= 0)
                return;

            Debug.Log("Enemy�� ���ݴ���");
            _myEnemyHp -= GameManager.instance.playerManager.PlayerReturnSKillDamage();
            // Player�� ���� ��ų�� ������ return

            StartCoroutine(damageText());               // ������ ǥ�� �ڷ�ƾ
            HiEnemy();                          // �÷��̾�� ���� �ִϸ��̼� ����                               

        }

        // player���� �ε�����
        if (collision.gameObject.CompareTag("Player")) 
        {
            // �÷��̾��� hp ��� �Լ� ȣ��
            GameManager.instance.playerManager.PlayerGetDamage(_myEnemyDB.Damage);       
        }

    }

    void OnDrawGizmos()
    {
        if (myEnemyDB == null)
            return;

        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, myEnemyDB.Sight);
        // DrawSphere( �߽� ��ġ , ������ )
    }


}
