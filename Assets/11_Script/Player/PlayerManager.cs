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

    [Header("기본적인 스탯")]
    int iHp;
    float fMoveSpeed;

    [Header("Move")]
    [SerializeField] bool canMove;              // 움직임 조건 (스킬 쓰면 못 움직임)

    [Header("총 쏘는")]
    [SerializeField] Transform trsShootPosi;
    private int _handgunIdx;
    private int _shootgunIdx;
    private int _lifleIdx;

    [Header("총 쏘는 코루틴 이름")]
    [SerializeField] string sCoruHandGun;
    [SerializeField] string sCoruShootGun;
    [SerializeField] string sCoruLifle;

    [SerializeField] bool isChange;                 // 기본이 false, 스킬 쓸 때는 true (샷건, 라이플에서 사용)

    [SerializeField]
    BulletDB myBulletDB;                                // 현재 발사 한 총에 대한 Bullet 정보

    // 플레이어 스킬
    // 플레이어가 가지고 있는 스킬 만큼 배열 크기 
    private FSM[] arraySkill = new FSM[System.Enum.GetValues(typeof(PlayerSkill_State)).Length];
    private HeadMachine skillmachine;

    public PlayerSkill_State currSkill;   // 현재 스킬
    public PlayerSkill_State preSkilll;    // 전 스킬


    // 움직임, 코루틴 String 프로퍼티
    public bool getCanMove          { get => canMove; set { canMove = value; } }
    public string getsCoruHandGun   { get => sCoruHandGun;  }
    public string getCoruShootGun   { get => sCoruShootGun; }
    public string getsCoruLifle     { get => sCoruLifle; }
    public bool getisChange { get => isChange; }

    // 스탯 프로퍼티
    public float getfMoveSpeed { get => fMoveSpeed; }
    public int handgunIdx { get => _handgunIdx; }
    public int shootgunIdx { get => _shootgunIdx; }
    public int lifleIdx { get => _lifleIdx; }

    // DB 프로퍼티
    public BulletDB MyBulletDb { get => myBulletDB; }

    private void PlayerInit() 
    {
        skillmachine = new HeadMachine();   
        // headmachine 생성 , PlayerManaher가 HeadMachine 을 가지게됨

        // arr[0] : idle 스크립트
        // arr[1] : handGun 스크립트
        // arr[2] : ShotGun 스크립트
        // arr[3] : Lifle 스크립트
        arraySkill[(int)PlayerSkill_State.Idle]       = new Skill_Idle(this);   // 생성자
        arraySkill[(int)PlayerSkill_State.HandGun]    = new Skill_HandGun(this);
        arraySkill[(int)PlayerSkill_State.ShotGun]    = new Skill_ShotGun(this);
        arraySkill[(int)PlayerSkill_State.Lifle]      = new Skill_Lifle(this);

        // 초기 상태는 Idle 
        // Idle 의 Run() 메서드에서 상태 변화를 함.
        skillmachine.SetState(arraySkill[(int)PlayerSkill_State.Idle]);
    }

    public void ChangeSkill(PlayerSkill_State chageSk)
    {
        // Enum은 int 형으로도 변환 가능해서 int로 비교하고 있음!
        for (int i = 0; i < System.Enum.GetValues(typeof(PlayerSkill_State)).Length; i++) 
        {
            if ((int)chageSk == i)
                skillmachine.ChangeState(arraySkill[i]);  // HeadMachine의 chage 함수 사용 , FSM형을 매개변수로 넘김
        }
    }

    public void VarInit() 
    {
        // 총 idx
        _handgunIdx = 0;
        _shootgunIdx = 1;
        _lifleIdx = 2;

        // 각 총 발사에 해당하는 코루틴 이름
        sCoruHandGun    = "IE_handGun";
        sCoruShootGun   = "IE_shootGun";
        sCoruLifle      = "IE_lifle";
        isChange        = false;

        // 기본 스탯
        iHp             = 10;
        canMove         = true;     // 기본움직임 -> true
        fMoveSpeed      = 5f;       // 속도        

        // BulletDB
        myBulletDB = null;
    }

    private void Awake()
    {
        Instance = this;            // 싱글톤 

        // FSM 초기 설정
        PlayerInit();
        skillmachine.H_Begin();     // Machine의 begin 메서드 실행 

        // 변수 초기화
        VarInit();
    }

    void Start()
    {

    }

    void Update()
    {
        skillmachine.H_Run();       // Machine의 Run 메서드 실행
    }

    // 총쏘는 코루틴 시작 ( 각 총은 Corutine으로 발사가 구현되어 있음)
    public void startShoot(string str)
    {
        StartCoroutine(str);
    }

    // 총 쏘는 코루틴 멈추기
    public void stopShoot(string str)
    {
        //StopAllCoroutines();
        StopCoroutine(str);
    }

    // HandGun 총 쏘기
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

    // ShootGun 총 쏘기
    IEnumerator IE_shootGun() 
    {
        isChange = false;

        for (int i = 0; i < 3; i++)
        {
            yield return StartCoroutine(FireShots());       // 총알이 3개씩 나가는 코루틴 
            yield return new WaitForSeconds(0.5f);          // 코루틴 사이의 딜레이
        }

        trsShootPosi.rotation = Quaternion.Euler(0,0,0);    // shootposi 회전 초기화
        isChange = true;
    }

    IEnumerator FireShots() 
    {
        // 총알 3개 발사하는 코루틴
        for (int i = 0; i < 3; i++)
        {
            trsShootPosi.rotation = Quaternion.Euler(0, -30 + (i * 30), 0);
            GunFire(_shootgunIdx);                              // 총알 발사
            yield return new WaitForSeconds(0.05f);             // 총알 <->총알 마다 딜레이
        }
    }

    // Lifle 총 쏘기
    IEnumerator IE_lifle() 
    {
        isChange = false;
        for (int i = 0; i < 4; i++) 
        {
            GunFire(_lifleIdx);                              // 총알 발사

            yield return new WaitForSeconds(0.8f);             // 총알 <->총알 마다 딜레이
        }
        isChange = true;
    }

    // 총알 발사하는 코드
    void GunFire(int gunIdx)
    {
        if (myBulletDB == null)       // 지금 있는 Bullet이 null 이면 (예외처리)
        {
            return;
        }

        GameObject obj          = GunSlinerBullet.Instance.getBullet();     // pool에서 get
        obj.transform.position  = trsShootPosi.position;          // 발사 위치로 이동
        //obj.transform.rotation  = Quaternion.Euler(90, 0,0);                // 회전
        //obj.transform.LookAt(trsShootPosi.transform.forward);               // 총알도 총알 쏘는 위치 보는 방향으로 보게

        obj.GetComponent<Rigidbody>().velocity = trsShootPosi.forward * myBulletDB.BulletSpeed;     // 현재 Bullet의 속도 가져오기
    }

    public void setBulletDB(int idx) 
    {
        myBulletDB = GunSlinerBullet.Instance.retunBulletDB(idx);
    }
    public void returnBulletToPool(GameObject obj) 
    {
        GunSlinerBullet.Instance.returnBullet(obj);
    }

    // Enemy에서 총을 맞을 때
    // Player Manager에 있는 Bullet은 플레이어가 마지막으로 쏜 총임 -> 그럼 이 총을 Enemy가 맞겠지?
    // Enemy에서는 playerManager에 있는 Bullet 에 접근해서 데미지를 가져가기만 하면 될듯
    public float GetEnemyBulletDamage() 
    {
        if (myBulletDB == null)
            return 0;

        // bullet의 최대~최소 중 랜덤 데미지를 return
        float myDamage = Random.Range(myBulletDB.MinDamage, myBulletDB.MaxDamage);
        return myDamage;
    }

}
