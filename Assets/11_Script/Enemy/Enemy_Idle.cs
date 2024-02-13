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
        Debug.Log("Monster : Idle 시작");
        enemyManager.currState = Enemy_State.Idle;
    }
    public override void Run()
    {
        //Debug.Log("Monster : Idle 돌아가는중");

        // 상태 조건 idle -> walk / attack
    }

    public override void End()
    {
        Debug.Log("Monster : Idle 끝");
        enemyManager.currState = Enemy_State.Idle;
    }


}
