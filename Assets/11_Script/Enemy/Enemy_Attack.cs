using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : FSM
{
    private EnemyFSM enemy;

    public Enemy_Attack(EnemyFSM enemyManager)
    {
        this.enemy = enemyManager;
    }

    public override void Begin()
    {
        Debug.Log("Monster : Attack 시작");
        enemy.currState = Enemy_State.Attack;

        // 공격 함수 실행
        enemy.startAttackPlayer();
    }

    public override void Run()
    {
        if (enemy.EndAttack)                                // 공격이 끝나면 (애니메이션 이벤트)
            enemy.changeEnemyState(Enemy_State.Tracking);   // tracking으로 상태변화
    }

    public override void End()
    {
        enemy.preSate = Enemy_State.Attack;
        Debug.Log("Monster : Attack 끝");
    }
}
