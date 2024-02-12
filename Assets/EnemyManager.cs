using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum Enemy_State 
{
    Idle, 
    Walk,
    Attack,
    GetDamage,
    Die
}

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;    // 싱글톤

    [SerializeField]
    List<EnemyDB> enemyDBList;      // Enemy DB
    [SerializeField]
    List<GameObject> enemyObj;      // Enemy 프리팹

    // FSM
    [SerializeField]
    public FSM[] enemyFSM = new FSM[System.Enum.GetValues(typeof(Enemy_State)).Length];
    public HeadMachine enemyMachine;        // HeadMachin 
    public Enemy_State currState;                   // 현재 상태
    public Enemy_State preSate;                     // 과거 상태

    //FSM init
    public void FSM_Init() 
    {
        enemyMachine = new HeadMachine();

        enemyFSM[(int)Enemy_State.Idle]         = new Enemy_Idle(this);         // Enemy_Idle 생성자
        enemyFSM[(int)Enemy_State.Walk]         = new Enemy_Walk(this);         // Enemy_Walk 생성자
        enemyFSM[(int)Enemy_State.Attack]       = new Enemy_Attack(this);       // Enemey_Attack 생성자
        enemyFSM[(int)Enemy_State.GetDamage]    = new Enemy_GetDamage(this);    // Enemy_GetDamage 생성자
        enemyFSM[(int)Enemy_State.Die]          = new Enemy_Die(this);          // Enemy_Die 생성자   
    }

    public void enemySetIdle() 
    {
        enemyMachine.SetState(enemyFSM[(int)Enemy_State.Idle]);                 // idle로 현재 상태 설정
    }
    
    private void Awake()
    {
        instance = this;
    }



}
