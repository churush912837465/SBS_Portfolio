using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum PlayerSkill_State 
{
    Idle,
    HandGun,
    ShotGun,
    Lifle
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [Header("�⺻���� ����")]
    int iHp;
    float fMoveSpeed;

    [Header("Move")]
    [SerializeField] bool canMove;              // ������ ���� (��ų ���� �� ������)

    [Header("�� ���")]
    [SerializeField] Transform trsShootPosi;
    private int _handgunIdx;
    private int _shootgunIdx;
    private int _lifleIdx;

    [Header("�� ��� �ڷ�ƾ �̸�")]
    [SerializeField] string sCoruHandGun;
    [SerializeField] string sCoruShootGun;
    [SerializeField] string sCoruLifle;

    [SerializeField] bool isChange;                 // �⺻�� false, ��ų �� ���� true (����, �����ÿ��� ���)

    [SerializeField]
    BulletDB myBulletDB;                                // ���� �߻� �� �ѿ� ���� Bullet ����

    // �÷��̾� ��ų
    // �÷��̾ ������ �ִ� ��ų ��ŭ �迭 ũ�� 
    private FSM[] arraySkill = new FSM[System.Enum.GetValues(typeof(PlayerSkill_State)).Length];
    private HeadMachine skillmachine;

    public PlayerSkill_State currSkill;   // ���� ��ų
    public PlayerSkill_State preSkilll;    // �� ��ų


    // ������, �ڷ�ƾ String ������Ƽ
    public bool getCanMove          { get => canMove; set { canMove = value; } }
    public string getsCoruHandGun   { get => sCoruHandGun;  }
    public string getCoruShootGun   { get => sCoruShootGun; }
    public string getsCoruLifle     { get => sCoruLifle; }
    public bool getisChange { get => isChange; }

    // ���� ������Ƽ
    public float getfMoveSpeed { get => fMoveSpeed; }
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
        arraySkill[(int)PlayerSkill_State.Idle]       = new Skill_Idle(this);   // ������
        arraySkill[(int)PlayerSkill_State.HandGun]    = new Skill_HandGun(this);
        arraySkill[(int)PlayerSkill_State.ShotGun]    = new Skill_ShotGun(this);
        arraySkill[(int)PlayerSkill_State.Lifle]      = new Skill_Lifle(this);

        // �ʱ� ���´� Idle 
        // Idle �� Run() �޼��忡�� ���� ��ȭ�� ��.
        skillmachine.SetState(arraySkill[(int)PlayerSkill_State.Idle]);
    }

    public void ChangeSkill(PlayerSkill_State chageSk)
    {
        // Enum�� int �����ε� ��ȯ �����ؼ� int�� ���ϰ� ����!
        for (int i = 0; i < System.Enum.GetValues(typeof(PlayerSkill_State)).Length; i++) 
        {
            if ((int)chageSk == i)
                skillmachine.ChangeState(arraySkill[i]);  // HeadMachine�� chage �Լ� ��� , FSM���� �Ű������� �ѱ�
        }
    }

    public void VarInit() 
    {
        // �� idx
        _handgunIdx = 0;
        _shootgunIdx = 1;
        _lifleIdx = 2;

        // �� �� �߻翡 �ش��ϴ� �ڷ�ƾ �̸�
        sCoruHandGun    = "IE_handGun";
        sCoruShootGun   = "IE_shootGun";
        sCoruLifle      = "IE_lifle";
        isChange        = false;

        // �⺻ ����
        iHp             = 10;
        canMove         = true;     // �⺻������ -> true
        fMoveSpeed      = 5f;       // �ӵ�        

        // BulletDB
        myBulletDB = null;
    }

    private void Awake()
    {
        Instance = this;            // �̱��� 

        // FSM �ʱ� ����
        PlayerInit();
        skillmachine.H_Begin();     // Machine�� begin �޼��� ���� 

        // ���� �ʱ�ȭ
        VarInit();
    }

    void Start()
    {

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
        isChange = false;

        for (int i = 0; i < 3; i++)
        {
            yield return StartCoroutine(FireShots());       // �Ѿ��� 3���� ������ �ڷ�ƾ 
            yield return new WaitForSeconds(0.5f);          // �ڷ�ƾ ������ ������
        }

        trsShootPosi.rotation = Quaternion.Euler(0,0,0);    // shootposi ȸ�� �ʱ�ȭ
        isChange = true;
    }

    IEnumerator FireShots() 
    {
        // �Ѿ� 3�� �߻��ϴ� �ڷ�ƾ
        for (int i = 0; i < 3; i++)
        {
            trsShootPosi.rotation = Quaternion.Euler(0, -30 + (i * 30), 0);
            GunFire(_shootgunIdx);                              // �Ѿ� �߻�
            yield return new WaitForSeconds(0.05f);             // �Ѿ� <->�Ѿ� ���� ������
        }
    }

    // Lifle �� ���
    IEnumerator IE_lifle() 
    {
        isChange = false;
        for (int i = 0; i < 4; i++) 
        {
            GunFire(_lifleIdx);                              // �Ѿ� �߻�

            yield return new WaitForSeconds(0.8f);             // �Ѿ� <->�Ѿ� ���� ������
        }
        isChange = true;
    }

    // �Ѿ� �߻��ϴ� �ڵ�
    void GunFire(int gunIdx)
    {
        if (myBulletDB == null)       // ���� �ִ� Bullet�� null �̸� (����ó��)
        {
            return;
        }

        GameObject obj          = GunSlinerBullet.Instance.getBullet();     // pool���� get
        obj.transform.position  = trsShootPosi.position;          // �߻� ��ġ�� �̵�
        //obj.transform.rotation  = Quaternion.Euler(90, 0,0);                // ȸ��
        //obj.transform.LookAt(trsShootPosi.transform.forward);               // �Ѿ˵� �Ѿ� ��� ��ġ ���� �������� ����

        obj.GetComponent<Rigidbody>().velocity = trsShootPosi.forward * myBulletDB.BulletSpeed;     // ���� Bullet�� �ӵ� ��������
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
        return myDamage;
    }

}
