using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Tracking : FSM
{
    private EnemyParent enemy;

    public Enemy_Tracking(EnemyParent enemyManager)
    {
        this.enemy = enemyManager;
    }

    public override void Begin()
    {
        //Debug.Log("Monster : Tracking 시작");
        enemy.currState = Enemy_State.Tracking;
    }

    public override void Run()
    {
        if (enemy.checkHp())                                // hp가 0이하면
            enemy.changeEnemyState(Enemy_State.Die);        // die로 상태변화

        FSMTracking_MoveEnemy();

        // 상태 변화 조건
        if (enemy.searchRangePlayer())                      // 플레이어가 범위 안에 들어오면
            enemy.changeEnemyState(Enemy_State.Attack);     // attack으로 상태변화
    }

    public override void End()
    {
        //Debug.Log("Monster : Tracking 끝");
        enemy.preSate = Enemy_State.Tracking;
    }

    public void FSMTracking_MoveEnemy() 
    {
        Vector3 myVec = enemy.gameObject.transform.position;
        Vector3 targetVec = GameManager.instance.player.transform.position;
        float moveSpeed = Time.deltaTime * enemy.myEnemyDB.Speed;

        enemy.gameObject.transform.position
            = Vector3.MoveTowards(myVec, targetVec, moveSpeed);
        // 내 위치 , 목표 위치 , 속도

        enemy.gameObject.transform.LookAt(targetVec);       // player을 쳐다보게
    }

}
