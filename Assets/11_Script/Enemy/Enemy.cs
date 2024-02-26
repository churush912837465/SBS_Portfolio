using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class Enemy : EnemyParent
{
    // EnemyFSM 상속 받고있어야 enemyFSM 배열을 가지고 있을 수 있음
    // Enemy가 가지고 있어야 할 것
    // 1. Enemy DB
    // 2. FSM 배열

    private bool _onEnable  = true;                   // onEnable 체크
    private bool _disEnable = true;                  // disEnable 체크

    public void getEnemyDB(EnemyDB DB) 
    {
        _myEnemyDB = DB; 
    }

    public void Awake()
    {
        FSM_Init();                             // FSM 초기화 (맨처음 1회면 됨)
    }
    
    public void OnEnable()                      // pool에서 빠져나왔을 때 (켜졌을 때)
    {
        // 맨 처음 pooling에 집어넣을때, 잠시 OnEnable 되었을때를 방지하기 위해서
        // flag 조건 걸어놓음
        if (_onEnable)           
        {
            _onEnable = false;
            return;
        }

        _damageText.text = "";
        _myEnemyHp = myEnemyDB.HP;              // hp 따로 저장

        // FSM 실행
        enemyMachine.SetState(enemyFSM[(int)Enemy_State.Tracking]);
        enemyMachine.H_Begin();                 // Mahine에 저장되어 있는 상태의 Begin 메서드 실행
        StartCoroutine(FSM_Run());              // 코루틴으로 매프레임 실행
    }

    public void OnDisable()                     // pool에 들어갈 때 (꺼졌을 때)
    {
        // 맨 처음 pooling에 집어넣을때, 잠시 OnDisable 되었을때를 방지하기 위해서
        // flag 조건 걸어놓음
        if (_disEnable)
        {
            _disEnable = false;
            return;
        }

        StopAllCoroutines();
        //StopCoroutine(FSM_Run());               // 현재 돌아가고있는  코루틴 (update) 중지
    }

    // 충돌감지
    private void OnCollisionEnter(Collision collision)
    {
        // 총알에 맞으면
        if (collision.gameObject.CompareTag("Skill"))   
        {
            if (_myEnemyHp <= 0)
                return;

            Debug.Log("Enemy가 공격당함");
            _myEnemyHp -= GameManager.instance.playerManager.PlayerReturnSKillDamage();
            // Player의 현재 스킬의 데미지 return

            StartCoroutine(damageText());               // 데미지 표시 코루틴
            HiEnemy();                          // 플레이어에게 맞은 애니메이션 실행                               

        }

        // player한테 부딪히면
        if (collision.gameObject.CompareTag("Player")) 
        {
            // 플레이어의 hp 깎는 함수 호출
            GameManager.instance.playerManager.PlayerGetDamage(_myEnemyDB.Damage);       
        }

    }

    void OnDrawGizmos()
    {
        if (myEnemyDB == null)
            return;

        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, myEnemyDB.Sight);
        // DrawSphere( 중심 위치 , 반지름 )
    }


}
