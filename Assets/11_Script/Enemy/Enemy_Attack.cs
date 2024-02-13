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
        Debug.Log("Monster : Attack ����");
        enemy.currState = Enemy_State.Attack;

        // ���� �Լ� ����
        enemy.startAttackPlayer();
    }

    public override void Run()
    {
        if (enemy.EndAttack)                                // ������ ������ (�ִϸ��̼� �̺�Ʈ)
            enemy.changeEnemyState(Enemy_State.Tracking);   // tracking���� ���º�ȭ
    }

    public override void End()
    {
        enemy.preSate = Enemy_State.Attack;
        Debug.Log("Monster : Attack ��");
    }
}
