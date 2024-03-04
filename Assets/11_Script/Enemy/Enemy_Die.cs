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
        Debug.Log("Enemy가 죽음");

        EnemyPooling.instance.returnEnemy(enemy, enemy.myEnemyDB.ID);
        DungeonManager.instance.nowCnt -= 1;        // 던전에서 현재 생성된 몬스터 갯수 --
        
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
