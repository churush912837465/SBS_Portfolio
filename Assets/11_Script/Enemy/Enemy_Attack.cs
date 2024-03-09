using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : FSM
{
    private EnemyParent enemy;

    public Enemy_Attack(EnemyParent enemyManager)
    {
        this.enemy = enemyManager;
    }

    public override void Begin()
    {
        //Debug.Log("Monster : Attack 시작");
        enemy.currState = Enemy_State.Attack;

        enemy.startAttackPlayer();                          // 공격 시작 실행
    }

    public override void Run()
    {
        if (enemy.checkHp())                                // hp가 0 이하면
            enemy.changeEnemyState(Enemy_State.Die);        // die로 상태변화

        if (enemy.EndAttack)                                // 공격이 끝나면 (애니메이션 이벤트)
            enemy.changeEnemyState(Enemy_State.Tracking);   // tracking으로 상태변화
    }

    public override void End()
    {
        enemy.endAttackplayer();                            // 공격 끝 실행
        enemy.preSate = Enemy_State.Attack;

        //Debug.Log("Monster : Attack 끝");
    }
}
