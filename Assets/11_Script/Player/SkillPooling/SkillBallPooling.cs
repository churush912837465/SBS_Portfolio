using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SkillBallPooling : MonoBehaviour
{
    public static SkillBallPooling instance;

    [Header("Skill Ball Pool")]
    [SerializeField]
    private Transform[] _skillBallPool;

    [Header("Skill Ball")]
    [SerializeField]
    private GameObject[] _skillBall;
    // [0] Thunder
    // [1] Fire
    // [2] Earth
    // [3] Water

    [SerializeField]
    private List<List<SkillBall>> _skillBalls;
    // List[0] : List<SkillBall> _thunderBall
    // List[1] : List<SkillBall> _fireBall
    // List[2] : List<SkillBall> _earthBall
    // List[3] : List<SkillBall> _waterBall

    private List<SkillBall> _thunderBall;
    private List<SkillBall> _fireBall;
    private List<SkillBall> _earthBall;
    private List<SkillBall> _waterBall;

    [Header("Skill의 Particle obj")]
    [SerializeField]
    private List<GameObject[]> _objList;

    [Space]
    #region particle 초기 오브젝트 저장
    [Header("ParticleObj")]
    [SerializeField]
    private GameObject[] _po0;
    [SerializeField]
    private GameObject[] _po1;
    [SerializeField]
    private GameObject[] _po2;
    [SerializeField]
    private GameObject[] _po3;
    #endregion

    [SerializeField]
    int _initCnt = 3;

    private void Awake()
    {
        instance = this;    // 싱글톤

        // particle 오브젝트 List 초기화
        _objList = new List<GameObject[]>();
        _objList.Add(_po0);
        _objList.Add(_po1);
        _objList.Add(_po2);
        _objList.Add(_po3);

        // Skill Ball List 초기화
        _skillBalls     = new List<List<SkillBall>>();
        _thunderBall    = new List<SkillBall>();
        _fireBall       = new List<SkillBall>();
        _earthBall      = new List<SkillBall>();
        _waterBall      = new List<SkillBall>();

        _skillBalls.Add(_thunderBall);
        _skillBalls.Add(_fireBall);
        _skillBalls.Add(_earthBall);
        _skillBalls.Add(_waterBall);

        InitSkillBallPool();
    }

    private void Start()
    {

    }

    public void InitSkillBallPool()
    {
        for(int i = 0; i < _skillBall.Length; i++)      // skill ball 갯수
        {
            for (int j = 0; j < _initCnt; j++)          // 초기 cnt
            {
                SkillBall _sk = CreateSkillBall(i);     // Skill Ball 생성
                _skillBalls[i].Add(_sk);                // List에 넣기
            }
        }
    }

    // 초기 initSkill Ball
    public SkillBall CreateSkillBall(int v_b) 
    {
        GameObject obj          = Instantiate(_skillBall[v_b]);     // skill ball 생성
        obj.transform.parent    = _skillBallPool[v_b];              // skill ball 부모 생성
        obj.SetActive(false);                                       // 오브젝트 끄기

        Rigidbody _rb           = obj.GetComponent<Rigidbody>();
        _rb.mass = 0f;                                              // 생성 후 안 떨어지게 mass을 0으로

        SkillBall _objSkillBall = obj.GetComponent<SkillBall>();    // SKill Ball 스크립트가 있어야 함
        if(_objSkillBall == null)
            _objSkillBall.AddComponent<SkillBall>();

        _objSkillBall.InitParticle(_objList[v_b]);                  // parti 오브젝트가 담겨져 있는 배열 넘기기
        _objSkillBall.SkillBallIdx = v_b;                           // 인덱스 설정

        return _objSkillBall;
    }

    // get 할 때
    public SkillBall GetSkillBall(int v_i) 
    {
        // Skill의 SkillUse에서 get 사용
        SkillBall _reBall = null;

        if (_skillBalls[v_i].Count > 0)         // _skillBalls에 있는 List 안에 오브젝트 있으면 
        {
            // queue의 dequeue에 해당
            _reBall = _skillBalls[v_i][0];      // _skillBalls의 첫번 째 오브젝트
            _skillBalls[v_i].RemoveAt(0);       // 첫번 째 오브젝트 지움
        }
        else if (_skillBalls[v_i].Count <= 0)   // 오브젝트가 없으면
        {
            _reBall = CreateSkillBall(v_i);     // list에는 return 할 때 넣기 (켜져있음)
        }

        _reBall.gameObject.SetActive(true);     // 켜기
        return _reBall;
    }

    // return 할 때
    public void ReturnSkillBall(SkillBall v_reObj , int v_idx) 
    {
        // SklillBall 에서 return
        Debug.Log("return to Pool");

        // 부모 바꾸기
        v_reObj.transform.parent = _skillBallPool[v_idx];

        // 안보이게
        v_reObj.gameObject.SetActive(false);

        // List에 넣기
        _skillBalls[v_idx].Add(v_reObj);

    }


}
