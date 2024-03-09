using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Idle : FSM
{
    private EnemyParent enemy;

    public Enemy_Idle(EnemyParent enemyManager)
    {
        this.enemy = enemyManager;
    }

    public override void Begin()
    {
        //Debug.Log("curr ���´� idle");
        enemy.currState = Enemy_State.Idle;
    }

    public override void Run()
    {

    }

    public override void End()
    {
        //Debug.Log("pre ���´� idle");
        enemy.preSate = Enemy_State.Idle;
    }
}
