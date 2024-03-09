using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Die : FSM
{
    private EnemyParent enemy;

    public Enemy_Die(EnemyParent enemyManager)
    {
        this.enemy = enemyManager;
    }

    public override void Begin()
    {
        //Debug.Log("Enemy 상태 : Die ");
        enemy.currState = Enemy_State.Die;
        enemy.changeEnemyState(Enemy_State.Idle);   // pool에 넣기 전 idle로 변환

        enemy.EnemyDieAndPlayerGetItem();           // 플레이어 아이템 획득
        enemy.EnemyDieAndPlayerGetGoods();          // 플레이어 재화 획득

        enemy.gameObject.SetActive(false);          // 오브젝트 끄기
        enemy.stopCorutineEnemy();                  // 코루틴 종료
        EnemyPooling.instance.returnEnemy(enemy, enemy.myEnemyDB.ID);   // pool로 return
        DungeonManager.instance.nowCnt -= 1;        // 던전에서 현재 생성된 몬스터 갯수 --
        

    }

    public override void Run()
    {
        Debug.Log("enemy의 die 상태가 계속 돌아가고있음");
    }
    public override void End()
    {
        //Debug.Log("die상태의 end");
        enemy.preSate = Enemy_State.Die;
    }
}
