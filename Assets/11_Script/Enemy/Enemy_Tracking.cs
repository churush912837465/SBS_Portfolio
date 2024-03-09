using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Tracking : FSM
{
    private EnemyParent enemy;

    public Enemy_Tracking(EnemyParent enemyManager)
    {
        this.enemy = enemyManager;
    }

    public override void Begin()
    {
        //Debug.Log("Monster : Tracking ����");
        enemy.currState = Enemy_State.Tracking;
    }

    public override void Run()
    {
        if (enemy.checkHp())                                // hp�� 0���ϸ�
            enemy.changeEnemyState(Enemy_State.Die);        // die�� ���º�ȭ

        FSMTracking_MoveEnemy();

        // ���� ��ȭ ����
        if (enemy.searchRangePlayer())                      // �÷��̾ ���� �ȿ� ������
            enemy.changeEnemyState(Enemy_State.Attack);     // attack���� ���º�ȭ
    }

    public override void End()
    {
        //Debug.Log("Monster : Tracking ��");
        enemy.preSate = Enemy_State.Tracking;
    }

    public void FSMTracking_MoveEnemy() 
    {
        Vector3 myVec = enemy.gameObject.transform.position;
        Vector3 targetVec = GameManager.instance.player.transform.position;
        float moveSpeed = Time.deltaTime * enemy.myEnemyDB.Speed;

        enemy.gameObject.transform.position
            = Vector3.MoveTowards(myVec, targetVec, moveSpeed);
        // �� ��ġ , ��ǥ ��ġ , �ӵ�

        enemy.gameObject.transform.LookAt(targetVec);       // player�� �Ĵٺ���
    }

}
