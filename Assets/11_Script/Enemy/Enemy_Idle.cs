using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Idle : FSM
{
    private EnemyFSM enemyManager;

    public Enemy_Idle(EnemyFSM enemyManager)
    {
        this.enemyManager = enemyManager;
    }

    public override void Begin()
    {
        Debug.Log("Monster : Idle Ω√¿€");
        enemyManager.currState = Enemy_State.Idle;
    }
    public override void Run()
    {

    }

    public override void End()
    {
        Debug.Log("Monster : Idle ≥°");
        enemyManager.currState = Enemy_State.Idle;
    }


}
