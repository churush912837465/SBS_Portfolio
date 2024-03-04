using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonManager : MonoBehaviour
{
    public static DungeonManager instance;

    [Header("던전 오브젝트")]
    [SerializeField]
    private Transform[] _spawner;
    [SerializeField]
    private Transform _playerDungeonStart;
    [SerializeField]
    private GameObject _returnPotal;

    [Header("변수")]
    private int _createInit = 3;        // 한번에 몇마리 생성할건지
    private int _nowSpanwer = 0;        // 현재 생성할 스폰 index
    private int _nowCnt;                // 현재 필드에 있는 몬스터 수량
    private int _minEnemyCnt = 1;       // 다름 spawn으로 넘어갈 최소 조건

    // 프로퍼티
    public int nowCnt { get => _nowCnt; set { _nowCnt = value; } }

    bool flag = true;

    private void Start()
    {
        instance = this;        // 싱글톤
        // enemy가 die 상태 일 때 createInit을 --

        _nowCnt = _createInit;  // 초기화
    }

    // UI Manager에서 Button Event로 실행됨 
    public void startDungeon() 
    {
        Debug.Log("던전에 입장 합니다.");
        _returnPotal.SetActive(false);

        GameManager.instance.player.transform.position = _playerDungeonStart.position;
        StartCoroutine(DungeinFlow());
    }

    IEnumerator DungeinFlow()
    {
        for (_nowSpanwer = 0; _nowSpanwer < _spawner.Length;) 
        {
            // 1. 적 생성
            CreateEnemy(_nowSpanwer);

            // 2. 생성 후 _nowCnt 검사 (필드에 남아있는 일정 몬스터 수)
            while (!Checkcount()) 
            {
                yield return new WaitForSeconds(0.1f);
            }
            // 0.1초 마다 while문을 반복, CheckCount로 조건을 검사함
            // 조건이 맞아서 true가 되면? 

            // 3. nowSpanwer 증가
            _nowSpanwer++;
        }

        Debug.Log("마을로 돌아가는 포탈이 열렸습니다");
        _returnPotal.SetActive(true);

    }

    bool Checkcount()
    {
        // _nowCnt가 0 이 되면 true 반환
        if (_nowCnt < _minEnemyCnt)
            return true;
        return false;
    }

    private void CreateEnemy(int v_cnt)
    {
        _nowCnt = _createInit;              // 현재 필드에 있는 몬스터 수 초기화

        Vector3 _geneVec = Vector3.zero;
        float _rx;
        float _rz;

        for (int i = 0; i < _createInit; i++) 
        {
            int idx = returnRandNum();                                  // 랜덤 인덱스
            GameObject obj = EnemyPooling.instance.getEnemy(idx);       // 몬스터 pool에서 get
            
            obj.transform.parent        = _spawner[v_cnt];              // spawner의 위치 아래로 오브젝트 생성
            obj.transform.position      = _spawner[v_cnt].position;      // spqnwer의 위치로 위치 지정

            _rx = Random.Range(0 , 1f);
            _rz = Random.Range(0 , 1f);

            _geneVec = new Vector3( _rx, _rz);
            obj.transform.localPosition = _geneVec;
        }

    }

    int returnRandNum() 
    {
        int _min = 0;
        int _max = EnemyManager.instance.enemyCnt;      // Enemy 종류만큼
        int range = Random.Range(_min, _max);           // 랜덤 index

        return range;
    }

}
