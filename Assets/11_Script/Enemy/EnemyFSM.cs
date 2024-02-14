using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Enemy_State
{
    Idle,
    Tracking,
    Attack,
    Die
}

public class EnemyFSM : MonoBehaviour
{
    // FSM
    [SerializeField]
    public FSM[] enemyFSM = new FSM[System.Enum.GetValues(typeof(Enemy_State)).Length];
    [SerializeField]
    public HeadMachine enemyMachine;        // HeadMachin 
    public Enemy_State currState;                   // 현재 상태
    public Enemy_State preSate;                     // 과거 상태

    // 컴포넌트
    [SerializeField]
    protected EnemyDB _myDB;                   // EnemyPooling에서 생성할 때 DB를 할당해준다.
    [SerializeField]
    protected Animator _animator;
    [SerializeField]
    protected Collider _attackCollider;

    //변수
    [SerializeField]
    bool _endAttack;
    [SerializeField]
    protected float _myHp;                                // 각 enemy 는 공통된 DB를 가지고 있는데,
                                                // 계속 변해야 하는 hp는 따로 변수를 가지고 있는게 편할듯

    // 프로퍼티
    public EnemyDB myEnemyDB { get => _myDB; }
    public Animator animator { get => _animator;  }
    public bool EndAttack { get => _endAttack;  }

    //FSM init
    public void FSM_Init()
    {
        enemyMachine = new HeadMachine();

        enemyFSM[(int)Enemy_State.Idle]         = new Enemy_Idle(this);         // Enemy_Idle 생성자
        enemyFSM[(int)Enemy_State.Tracking]     = new Enemy_Tracking(this);     // Enemy_Walk 생성자
        enemyFSM[(int)Enemy_State.Attack]       = new Enemy_Attack(this);       // Enemey_Attack 생성자
        enemyFSM[(int)Enemy_State.Die]          = new Enemy_Die(this);          // Enemy_Die 생성자   

        enemyMachine.SetState(enemyFSM[(int)Enemy_State.Idle]);

        // 컴포넌트 가져오기
        _animator               = gameObject.GetComponent<Animator>();
        _attackCollider.enabled = false;                        // 콜라이더 끄기
    }

    public void changeEnemyState(Enemy_State state) 
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(Enemy_State)).Length; i++)
        {
            if ((int)state == i)                        // for문 돌면서 같은 상태를 찾으면
                enemyMachine.ChangeState(enemyFSM[i]);  // 그 상태로 바꿈
        }
    }

    // 시야 내 Player를 탐색하는
    // tracking -> attack
    public bool searchRangePlayer() 
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        Vector3 center = new Vector3(x, y, z);     // 범위의 중심 (즉, enemy)

        Collider[] colliders = Physics.OverlapSphere(center, myEnemyDB.Sight ); //시작 위치 , 범위

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    // Attack 상태일 때, 공격 시작
    public void startAttackPlayer() 
    {
        _endAttack = false;
        _attackCollider.enabled = true;             // 콜라이더 켜기
        _animator.SetTrigger(myEnemyDB.AttackAni);
    }

    // Attak이 끝날 떄, 애니메이션 이벤트로 실행
    public void endAttackplayer()
    {
        _endAttack = true;

        _attackCollider.enabled = false;            // 콜라이더 끄기
        Debug.Log("공격이 끝 , 애니메이션 이벤트로 실행");
    }

    // Enemy가 피격 당했을 때
    public void getDamagePlayer() 
    {
        _animator.SetTrigger(myEnemyDB.GetDamageAni);
    }


}
