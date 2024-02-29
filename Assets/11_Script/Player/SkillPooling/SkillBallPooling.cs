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

    [Header("Skill�� Particle obj")]
    [SerializeField]
    private List<GameObject[]> _objList;

    [Space]
    #region particle �ʱ� ������Ʈ ����
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
        instance = this;    // �̱���

        // particle ������Ʈ List �ʱ�ȭ
        _objList = new List<GameObject[]>();
        _objList.Add(_po0);
        _objList.Add(_po1);
        _objList.Add(_po2);
        _objList.Add(_po3);

        // Skill Ball List �ʱ�ȭ
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
        for(int i = 0; i < _skillBall.Length; i++)      // skill ball ����
        {
            for (int j = 0; j < _initCnt; j++)          // �ʱ� cnt
            {
                SkillBall _sk = CreateSkillBall(i);     // Skill Ball ����
                _skillBalls[i].Add(_sk);                // List�� �ֱ�
            }
        }
    }

    // �ʱ� initSkill Ball
    public SkillBall CreateSkillBall(int v_b) 
    {
        GameObject obj          = Instantiate(_skillBall[v_b]);     // skill ball ����
        obj.transform.parent    = _skillBallPool[v_b];              // skill ball �θ� ����
        obj.SetActive(false);                                       // ������Ʈ ����

        Rigidbody _rb           = obj.GetComponent<Rigidbody>();
        _rb.mass = 0f;                                              // ���� �� �� �������� mass�� 0����

        SkillBall _objSkillBall = obj.GetComponent<SkillBall>();    // SKill Ball ��ũ��Ʈ�� �־�� ��
        if(_objSkillBall == null)
            _objSkillBall.AddComponent<SkillBall>();

        _objSkillBall.InitParticle(_objList[v_b]);                  // parti ������Ʈ�� ����� �ִ� �迭 �ѱ��
        _objSkillBall.SkillBallIdx = v_b;                           // �ε��� ����

        return _objSkillBall;
    }

    // get �� ��
    public SkillBall GetSkillBall(int v_i) 
    {
        // Skill�� SkillUse���� get ���
        SkillBall _reBall = null;

        if (_skillBalls[v_i].Count > 0)         // _skillBalls�� �ִ� List �ȿ� ������Ʈ ������ 
        {
            // queue�� dequeue�� �ش�
            _reBall = _skillBalls[v_i][0];      // _skillBalls�� ù�� ° ������Ʈ
            _skillBalls[v_i].RemoveAt(0);       // ù�� ° ������Ʈ ����
        }
        else if (_skillBalls[v_i].Count <= 0)   // ������Ʈ�� ������
        {
            _reBall = CreateSkillBall(v_i);     // list���� return �� �� �ֱ� (��������)
        }

        _reBall.gameObject.SetActive(true);     // �ѱ�
        return _reBall;
    }

    // return �� ��
    public void ReturnSkillBall(SkillBall v_reObj , int v_idx) 
    {
        // SklillBall ���� return
        Debug.Log("return to Pool");

        // �θ� �ٲٱ�
        v_reObj.transform.parent = _skillBallPool[v_idx];

        // �Ⱥ��̰�
        v_reObj.gameObject.SetActive(false);

        // List�� �ֱ�
        _skillBalls[v_idx].Add(v_reObj);

    }


}
