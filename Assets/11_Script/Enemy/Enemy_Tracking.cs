using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Tracking : FSM
{
    private EnemyFSM enemy;

    public Enemy_Tracking(EnemyFSM enemyManager)
    {
        this.enemy = enemyManager;
    }

    public override void Begin()
    {
        Debug.Log("Monster : Tracking 시작");
        enemy.currState = Enemy_State.Tracking;
    }

    public override void Run()
    {
        Vector3 myVec       = enemy.gameObject.transform.position;
        Vector3 targetVec   = GameManager.instance.player.transform.position;
        float moveSpeed     = Time.deltaTime * enemy.myDB.Speed;

        enemy.gameObject.transform.position
            = Vector3.MoveTowards(myVec, targetVec, moveSpeed);
        // 내 위치 , 목표 위치 , 속도

        // 상태 변화 조건
        // 범위 안에 들어오면 Enemy_Attack으로 변화
        if (enemy.searchRangePlayer())
            enemy.changeEnemyState(Enemy_State.Attack);

    }

    public override void End()
    {
        Debug.Log("Monster : Tracking 끝");
        enemy.preSate = Enemy_State.Tracking;
    }


}
