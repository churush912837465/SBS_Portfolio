using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public enum Enemy_State
{
    Idle,
    Tracking,
    Attack,
    Die
}

public class EnemyParent : MonoBehaviour
{
    // FSM
    [SerializeField]
    public FSM[] enemyFSM = new FSM[System.Enum.GetValues(typeof(Enemy_State)).Length];
    [SerializeField]
    public HeadMachine enemyMachine;        // HeadMachin 
    public Enemy_State currState;                   // ���� ����
    public Enemy_State preSate;                     // ���� ����

    // ������Ʈ
    [SerializeField]
     protected EnemyDB _myDB;                   // EnemyPooling���� ������ �� DB�� �Ҵ����ش�.
    [SerializeField]
     Animator _animator;
    [SerializeField]
     Collider _attackCollider;
    [SerializeField]
    protected TextMeshProUGUI _damageText;         // �������� ���� text

    //����
    [SerializeField]
    bool _endAttack;
    [SerializeField]
    protected float _myEnemyHp;
    // �� enemy �� ����� DB�� ������ �ִµ�
    // ��� ���ؾ� �ϴ� hp�� ���� ������ ������ �ִ°� ���ҵ�

    // ������Ƽ
    public EnemyDB myEnemyDB { get => _myDB; }
    public Animator animator { get => _animator; }
    public bool EndAttack { get => _endAttack; }
    public float myEnemyHP { get => _myEnemyHp; }

    //FSM init
    public void FSM_Init()
    {
        enemyMachine = new HeadMachine();

        enemyFSM[(int)Enemy_State.Idle]     = new Enemy_Idle(this);         // Enemy_Idle ������
        enemyFSM[(int)Enemy_State.Tracking] = new Enemy_Tracking(this);     // Enemy_Walk ������
        enemyFSM[(int)Enemy_State.Attack]   = new Enemy_Attack(this);       // Enemey_Attack ������
        enemyFSM[(int)Enemy_State.Die]      = new Enemy_Die(this);          // Enemy_Die ������   

        enemyMachine.SetState(enemyFSM[(int)Enemy_State.Idle]);

        // ������Ʈ ��������
        _animator = gameObject.GetComponent<Animator>();
        _attackCollider.enabled = false;                        // �ݶ��̴� ����
    }

    public void changeEnemyState(Enemy_State state)
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(Enemy_State)).Length; i++)
        {
            if ((int)state == i)                        // for�� ���鼭 ���� ���¸� ã����
                enemyMachine.ChangeState(enemyFSM[i]);  // �� ���·� �ٲ�
        }
    }

    //FSM ����
    protected IEnumerator FSM_Run()
    {
        while (true)
        {
            enemyMachine.H_Run();               // update�� ��� ���� ������ run�� �������� ����
            yield return null;
        }
    }


    //----------------------------------------------------------------

    // hp üũ
    public bool checkHp()
    {
        if (_myEnemyHp <= 0)
            return true;

        return false;
    }


    // �þ� �� Player�� Ž���ϴ�
    // tracking -> attack
    public bool searchRangePlayer()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        Vector3 center = new Vector3(x, y, z);     // ������ �߽� (��, enemy)

        Collider[] colliders = Physics.OverlapSphere(center, myEnemyDB.Sight); //���� ��ġ , ����

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    // Attack ������ ��, ���� ����
    public void startAttackPlayer()
    {
        _endAttack = false;
        _attackCollider.enabled = true;             // �ݶ��̴� �ѱ�
        _animator.SetTrigger(myEnemyDB.AttackAni);
    }

    // Attak�� ���� ��, �ִϸ��̼� �̺�Ʈ�� ����
    public void endAttackplayer()
    {
        _endAttack = true;
        _attackCollider.enabled = false;            // �ݶ��̴� ����
    }

    // Enemy�� �ǰ� ������ ��
    public void getDamagePlayer()
    {
        _animator.SetTrigger(myEnemyDB.GetDamageAni);
    }

    // Enemy ���� Text �߰� �ϴ�
    protected IEnumerator damageText()
    {
        if (_damageText.gameObject == null)
            yield return null;

        _damageText.gameObject.SetActive(true);     // �ؽ�Ʈ �ѱ�
        _damageText.text = GameManager.instance.playerManager.GetEnemyBulletDamage().ToString();        // ������ ǥ��


        yield return new WaitForSeconds(0.5f);

        _damageText.gameObject.SetActive(false);
    }

}
