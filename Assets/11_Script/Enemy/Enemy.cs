using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : EnemyFSM
{
    // EnemyFSM 상속 받고있어야 enemyFSM 배열을 가지고 있을 수 있음
    // Enemy가 가지고 있어야 할 것
    // 1. Enemy DB
    // 2. FSM 배열

    public bool flag = true;                    // onEnable 체크
    public Text _damageText;                    // 데미지가 적힐 text

    public void getEnemyDB(EnemyDB DB) 
    {
        _myDB = DB; 
    }

    public void Awake()
    {
        FSM_Init();                             // FSM 초기화 (맨처음 1회면 됨.)
    }
    
    public void OnEnable()                      // pool에서 빠져나왔을 때 (켜졌을 때)
    {
        if (flag)
        {
            flag = false;
            return;
        }
        // pooling 에서 새로 instantiate하는 애들은 처음 onEnable 상태를 체크할 필요 x
        // pooling 할 때 flag를 false로 수정함

        if (enemyMachine == null)
            Debug.LogWarning("Enemy의 HeadMachine은 null");

        _myHp = myEnemyDB.HP;                   // hp 따로 저장
        enemyMachine.SetState(enemyFSM[(int)Enemy_State.Tracking]);
        enemyMachine.H_Begin();                 // Mahine에 저장되어 있는 상태의 Begin 메서드 실행
        
        StartCoroutine(FSM_Run());              // 코루틴으로 매프레임 실행
    }

    public void OnDisable()                     // pool에 들어갈 때 (꺼졌을 때)
    {
        StopCoroutine(FSM_Run());               // 현재 돌아가고있는  코루틴 (update) 중지    
    }

    IEnumerator FSM_Run() 
    {
        while (true) 
        {
            enemyMachine.H_Run();               // update문 대신 현재 상태의 run을 매프레임 실행
            yield return null;
        }
    }

    // 충돌감지
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))   // 총알에 맞으면
        {
            Debug.Log("Enemy가 공격당함");
            _myHp -= GameManager.instance.playerManager.GetEnemyBulletDamage();     
            // Player가 쏜 Enemy의 데미지를 return
            getDamagePlayer();                                      
            // 플레이어에게 맞은 애니메이션 실행 
        }
    }

    void OnDrawGizmos()
    {
        if (myEnemyDB == null)
            return;

        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, myEnemyDB.Sight);
        // DrawSphere( 중심 위치 , 반지름 )
    }


}
