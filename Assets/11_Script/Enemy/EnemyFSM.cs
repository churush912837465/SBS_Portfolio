using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Enemy_State
{
    Idle,
    Walk,
    Attack,
    GetDamage,
    Die
}

public class EnemyFSM : MonoBehaviour
{
    // FSM
    [SerializeField]
    public FSM[] enemyFSM = new FSM[System.Enum.GetValues(typeof(Enemy_State)).Length];
    public HeadMachine enemyMachine;        // HeadMachin 
    public Enemy_State currState;                   // ���� ����
    public Enemy_State preSate;                     // ���� ����

    //FSM init
    public void FSM_Init()
    {
        enemyMachine = new HeadMachine();

        enemyFSM[(int)Enemy_State.Idle]         = new Enemy_Idle(this);         // Enemy_Idle ������
        enemyFSM[(int)Enemy_State.Walk]         = new Enemy_Walk(this);         // Enemy_Walk ������
        enemyFSM[(int)Enemy_State.Attack]       = new Enemy_Attack(this);       // Enemey_Attack ������
        enemyFSM[(int)Enemy_State.GetDamage]    = new Enemy_GetDamage(this);    // Enemy_GetDamage ������
        enemyFSM[(int)Enemy_State.Die]          = new Enemy_Die(this);          // Enemy_Die ������   
    }

    public void enemySetIdle()
    {
        enemyMachine.SetState(enemyFSM[(int)Enemy_State.Idle]);                 // idle�� ���� ���� ����
    }

}
