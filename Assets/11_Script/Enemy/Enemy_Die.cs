using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Die : FSM
{
    private EnemyParent enemy;

    public Enemy_Die(EnemyParent enemyManager)
    {
        this.enemy = enemyManager;
    }

    public override void Begin()
    {
        //Debug.Log("Enemy ���� : Die ");
        enemy.currState = Enemy_State.Die;
        enemy.changeEnemyState(Enemy_State.Idle);   // pool�� �ֱ� �� idle�� ��ȯ

        enemy.EnemyDieAndPlayerGetItem();           // �÷��̾� ������ ȹ��
        enemy.EnemyDieAndPlayerGetGoods();          // �÷��̾� ��ȭ ȹ��

        enemy.gameObject.SetActive(false);          // ������Ʈ ����
        enemy.stopCorutineEnemy();                  // �ڷ�ƾ ����
        EnemyPooling.instance.returnEnemy(enemy, enemy.myEnemyDB.ID);   // pool�� return
        DungeonManager.instance.nowCnt -= 1;        // �������� ���� ������ ���� ���� --
        

    }

    public override void Run()
    {
        Debug.Log("enemy�� die ���°� ��� ���ư�������");
    }
    public override void End()
    {
        //Debug.Log("die������ end");
        enemy.preSate = Enemy_State.Die;
    }
}
