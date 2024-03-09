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

        // ó������, pool�� ���� ���� pre ���´� idle or die , �׷��� tracking���� ���º�ȭ
        enemyMachine.ChangeState( enemyFSM[(int)Enemy_State.Tracking]); 
        // FSM ����
        enemyMachine.H_Begin();                 // Mahine�� ����Ǿ� �ִ� ������ Begin �޼��� ����
        StartCoroutine(FSM_Run());              // �ڷ�ƾ���� �������� ����

    }

    /*
    public void OnDisable()
    {
        if (_disEnable) 
        {
            _disEnable = false;
            return;
        }

        // enemy�� die -> ������Ʈ �� -> �ڷ�ƾ ���� -> pool�� ��������
        // ������Ʈ ���� �� idle�� ���º�ȭ
        enemyMachine.ChangeState(enemyFSM[(int)Enemy_State.Idle]);
    }
    */

    // �浹����
    private void OnCollisionEnter(Collision collision)
    {
        // player���� �ε�����
        if (collision.gameObject.CompareTag("Player")) 
        {
            // �÷��̾��� hp ��� �Լ� ȣ��
            GameManager.instance.playerManager.PlayerGetDamage(_myEnemyDB.Damage);       
        }

        // Skill ������Ʈ�� �ε�����
        if (collision.gameObject.CompareTag("Skill")) 
        {
            HiEnemy();                                  // Enemy�� �ǰ� ������ ��      
        }
    }

    // Player�� ��ų �浹 ����
        //  player�� ��ų ��ƼŬ�� �浹�ϸ�
        //  -> enemy���� �������� ��
    private void OnParticleCollision(GameObject other)
    {
        HiEnemy();                                  // Enemy�� �ǰ� ������ ��      
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
