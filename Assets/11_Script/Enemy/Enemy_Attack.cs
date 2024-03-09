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
        //Debug.Log("Monster : Attack ����");
        enemy.currState = Enemy_State.Attack;

        enemy.startAttackPlayer();                          // ���� ���� ����
    }

    public override void Run()
    {
        if (enemy.checkHp())                                // hp�� 0 ���ϸ�
            enemy.changeEnemyState(Enemy_State.Die);        // die�� ���º�ȭ

        if (enemy.EndAttack)                                // ������ ������ (�ִϸ��̼� �̺�Ʈ)
            enemy.changeEnemyState(Enemy_State.Tracking);   // tracking���� ���º�ȭ
    }

    public override void End()
    {
        enemy.endAttackplayer();                            // ���� �� ����
        enemy.preSate = Enemy_State.Attack;

        //Debug.Log("Monster : Attack ��");
    }
}
