using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum Player_State 
{
    Idle,
    HandGun,
    ShotGun,
    Lifle,
    Die
}

public class PlayerManager : MonoBehaviour
{

    [Header("�⺻���� ����")]
    private float _hp;
    private float _moveSpeed;

    [Header("Move")]
    [SerializeField] bool _canMove;              // ������ ���� (��ų ���� �� ������)

    [Header("�� ���")]
    [SerializeField] Transform _trsShootPosi;
    private int _handgunIdx;
    private int _shootgunIdx;
    private int _lifleIdx;

    [Header("�� ��� �ڷ�ƾ �̸�")]
    private string _CoruHandGun    = "IE_handGun";
    private string _CoruShootGun   = "IE_shootGun";
    private string _CoruLifle      = "IE_lifle";

    [SerializeField] bool _isChange;                 // �⺻�� false, ��ų �� ���� true (����, �����ÿ��� ���)

    [Header("������Ʈ")]
    [SerializeField]
    BulletDB myBulletDB;                                // ���� �߻� �� �ѿ� ���� Bullet ����
    [SerializeField]
    Animator myAnimator;

    [Header("�ִϸ��̼�")]
    private string _runAni      = "run";
    private string _handGunAni  = "handGun";
    private string _shootGunAni = "shootGun";
    private string _lifleGunAni = "lifleGun";
    private string _dieAni      = "die";

    // �÷��̾� ��ų
    // �÷��̾ ������ �ִ� ��ų ��ŭ �迭 ũ�� 
    private FSM[] arraySkill = new FSM[System.Enum.GetValues(typeof(Player_State)).Length];
    private HeadMachine skillmachine;

    public Player_State currSkill;   // ���� ��ų
    public Player_State preSkilll;    // �� ��ų


    // ������, �ڷ�ƾ String ������Ƽ
    public bool CanMove { get => _canMove; set { _canMove = value; } }
    public string getsCoruHandGun   { get => _CoruHandGun;  }
    public string getCoruShootGun   { get => _CoruShootGun; }
    public string getsCoruLifle     { get => _CoruLifle; }
    public bool getisChange { get => _isChange; }

    // ���� ������Ƽ
    public float MoveSpeed { get => _moveSpeed; }
    public int handgunIdx { get => _handgunIdx; }
    public int shootgunIdx { get => _shootgunIdx; }
    public int lifleIdx { get => _lifleIdx; }

    // DB ������Ƽ
    public BulletDB MyBulletDb { get => myBulletDB; }

    private void PlayerInit() 
    {
        skillmachine = new HeadMachine();   
        // headmachine ���� , PlayerManaher�� HeadMachine �� �����Ե�

        // arr[0] : idle ��ũ��Ʈ
        // arr[1] : handGun ��ũ��Ʈ
        // arr[2] : ShotGun ��ũ��Ʈ
        // arr[3] : Lifle ��ũ��Ʈ
        arraySkill[(int)Player_State.Idle]       = new Skill_Idle(this);   // ������
        arraySkill[(int)Player_State.HandGun]    = new Skill_HandGun(this);
        arraySkill[(int)Player_State.ShotGun]    = new Skill_ShotGun(this);
        arraySkill[(int)Player_State.Lifle]      = new Skill_Lifle(this);
        arraySkill[(int)Player_State.Die]        = new Player_Die(this);

        // �ʱ� ���´� Idle 
        // Idle �� Run() �޼��忡�� ���� ��ȭ�� ��.
        skillmachine.SetState(arraySkill[(int)Player_State.Idle]);

        // GetComponent
        myAnimator = gameObject.GetComponent<Animator>();
    }

    public void ChangeSkill(Player_State chageSk)
    {
        // Enum�� int �����ε� ��ȯ �����ؼ� int�� ���ϰ� ����!
        for (int i = 0; i < System.Enum.GetValues(typeof(Player_State)).Length; i++) 
        {
            if ((int)chageSk == i)
                skillmachine.ChangeState(arraySkill[i]);  // HeadMachine�� chage �Լ� ��� , FSM���� �Ű������� �ѱ�
        }
    }

    public void VarInit() 
    {
        // �� idx
        _handgunIdx     = 0;
        _shootgunIdx    = 1;
        _lifleIdx       = 2;


        // �⺻ ����
        _hp             = 100f;
        _moveSpeed      = 7f;       // �ӵ�        
        _canMove         = true;    // �⺻������ -> true
        _isChange       = false;

        // BulletDB
        myBulletDB = null;
    }

    private void Awake()
    {
        // FSM �ʱ� ����
        PlayerInit();
        skillmachine.H_Begin();     // Machine�� begin �޼��� ���� 

        // ���� �ʱ�ȭ
        VarInit();
    }

    void Update()
    {
        skillmachine.H_Run();       // Machine�� Run �޼��� ����
    }

    // �ѽ�� �ڷ�ƾ ���� ( �� ���� Corutine���� �߻簡 �����Ǿ� ����)
    public void startShoot(string str)
    {
        StartCoroutine(str);
    }

    // �� ��� �ڷ�ƾ ���߱�
    public void stopShoot(string str)
    {
        //StopAllCoroutines();
        StopCoroutine(str);
    }

    // HandGun �� ���
    IEnumerator IE_handGun()
    {
        while (true)
        {
            bool isbullet = true;
            if (isbullet)
            {
                GunFire(_handgunIdx);
                isbullet = false;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    // ShootGun �� ���
    IEnumerator IE_shootGun() 
    {
        _isChange = false;

        for (int i = 0; i < 3; i++)
        {
            yield return StartCoroutine(FireShots());       // �Ѿ��� 3���� ������ �ڷ�ƾ 
            yield return new WaitForSeconds(0.5f);          // �ڷ�ƾ ������ ������
        }

        _trsShootPosi.rotation = Quaternion.Euler(0,0,0);    // shootposi ȸ�� �ʱ�ȭ
        _isChange = true;
    }

    IEnumerator FireShots() 
    {
        // �Ѿ� 3�� �߻��ϴ� �ڷ�ƾ
        for (int i = 0; i < 3; i++)
        {
            _trsShootPosi.rotation = Quaternion.Euler(0, -30 + (i * 30), 0);
            GunFire(_shootgunIdx);                              // �Ѿ� �߻�
            yield return new WaitForSeconds(0.05f);             // �Ѿ� <->�Ѿ� ���� ������
        }
    }

    // Lifle �� ���
    IEnumerator IE_lifle() 
    {
        _isChange = false;
        for (int i = 0; i < 4; i++) 
        {
            GunFire(_lifleIdx);                              // �Ѿ� �߻�

            yield return new WaitForSeconds(0.8f);             // �Ѿ� <->�Ѿ� ���� ������
        }
        _isChange = true;
    }

    // �Ѿ� �߻��ϴ� �ڵ�
    void GunFire(int gunIdx)
    {
        if (myBulletDB == null)       // ���� �ִ� Bullet�� null �̸� (����ó��)
        {
            return;
        }

        GameObject obj          = GunSlinerBullet.Instance.getBullet();     // pool���� get
        obj.transform.position  = _trsShootPosi.position;                    // �߻� ��ġ�� �̵�
        obj.transform.rotation  = Quaternion.Euler(90, 0,0);                // ȸ��
        obj.transform.LookAt(_trsShootPosi.transform.forward);               // �Ѿ˵� �Ѿ� ��� ��ġ ���� �������� ����

        obj.GetComponent<Rigidbody>().velocity = _trsShootPosi.forward * myBulletDB.BulletSpeed;     // ���� Bullet�� �ӵ� ��������
    }

    public void setBulletDB(int idx) 
    {
        myBulletDB = GunSlinerBullet.Instance.retunBulletDB(idx);
    }
    public void returnBulletToPool(GameObject obj) 
    {
        GunSlinerBullet.Instance.returnBullet(obj);
    }

    // Enemy���� ���� ���� ��
    // Player Manager�� �ִ� Bullet�� �÷��̾ ���������� �� ���� -> �׷� �� ���� Enemy�� �°���?
    // Enemy������ playerManager�� �ִ� Bullet �� �����ؼ� �������� �������⸸ �ϸ� �ɵ�
    public float GetEnemyBulletDamage() 
    {
        if (myBulletDB == null)
            return 0;

        // bullet�� �ִ�~�ּ� �� ���� �������� return
        float myDamage = Random.Range(myBulletDB.MinDamage, myBulletDB.MaxDamage);
        return Mathf.Ceil(myDamage);

    }

    // �÷��̾� Hp �˻�
    public bool PlayerHpUnderZero() 
    {
        if (_hp <= 0)
            return true;
        else 
            return false;
    }

    // �÷��̾ �׾��� �� action
    public void PlayerDieAction() 
    {
        Debug.Log("�÷��̾ �׾����ϴ�.");
    }

    public void PlayerGetDamage(float damage) 
    {
        Debug.Log("�÷��̾ �������� ����");
        _hp -= damage;
    }

    // �ִϸ��̼� ���� (�����ؾ���)
    public void playerRunAni(bool v)
    {
        myAnimator.SetBool(_runAni, v);
    }
    public void playerHandGunAni(bool v)
    {
        myAnimator.SetBool(_handGunAni, v);
    }
    public void playerShootGunAni(bool v)
    {
        myAnimator.SetBool(_shootGunAni, v);
    }
    public void playerLifle(bool v)
    {
        myAnimator.SetBool(_lifleGunAni, v);
    }
    public void playerDie(bool v)
    {
        myAnimator.SetBool(_dieAni, v);
    }

}
