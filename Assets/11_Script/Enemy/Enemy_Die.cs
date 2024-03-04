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
        Debug.Log("Enemy�� ����");

        EnemyPooling.instance.returnEnemy(enemy, enemy.myEnemyDB.ID);
        DungeonManager.instance.nowCnt -= 1;        // �������� ���� ������ ���� ���� --
        
        enemy.currState = Enemy_State.Die;

    }

    public override void Run()
    {

    }
    public override void End()
    {
        enemy.preSate = Enemy_State.Die;
    }
}
