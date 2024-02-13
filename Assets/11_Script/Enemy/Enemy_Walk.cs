using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Walk : FSM
{
    private EnemyFSM enemyManager;

    public Enemy_Walk(EnemyFSM enemyManager)
    {
        this.enemyManager = enemyManager;
    }

    public override void Begin()
    {
        Debug.Log("Monster : Tracking(walk) Ω√¿€");
        enemyManager.currState = Enemy_State.Walk;
    }

    public override void Run()
    {

    }

    public override void End()
    {
        Debug.Log("Monster : Tracking(walk) ≥°");
        enemyManager.preSate = Enemy_State.Walk;
    }


}
