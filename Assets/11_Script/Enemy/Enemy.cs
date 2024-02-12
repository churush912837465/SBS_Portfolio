using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyManager
{
    // EnemeyManaher을 상속 받고있어야 enemyFSM 배열을 가지고 있을 수 있음
    // 

    [SerializeField]
    EnemyDB myDB;

    // 생성자로 DB 넣기
    public Enemy(EnemyDB myDB) 
    {
        this.myDB = myDB; 
    }

    public void Start()
    {
        FSM_Init();                 //FSM 초기화 (맨처음 1회면 됨.)
        enemySetIdle();             // 현재 상태를 idle로 설정

        enemyMachine.H_Begin();     // Mahine에 저장되어 있는 상태의 Begin 메서드 실행
    }
    public void Update() 
    {
        enemyMachine.H_Run();       // Machine에 저장되어 있는 상태의 Run() 메서드 실행 
    }

    /*
    public void OnEnable()          // pool에서 빠져나왔을 때 (켜졌을 때)
    {
        StartCoroutine(FSM_Run());
        enemySetIdle();             // 현재 상태를 idle로 설정

    }

    public void OnDisable()         // pool에 들어갈 때 (꺼졌을 때)
    {
        StopCoroutine(FSM_Run());    // 현재 돌아가고있는  코루틴 (update) 중지    
    }

    IEnumerator FSM_Run() 
    {
        enemyMachine.H_Run();       // update문 대신 현재 상태의 run을 매프레임 실행
        yield return null;
    }
    */
}
