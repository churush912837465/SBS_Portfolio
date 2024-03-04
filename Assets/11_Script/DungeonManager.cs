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

    [Header("���� ������Ʈ")]
    [SerializeField]
    private Transform[] _spawner;
    [SerializeField]
    private Transform _playerDungeonStart;
    [SerializeField]
    private GameObject _returnPotal;

    [Header("����")]
    private int _createInit = 3;        // �ѹ��� ��� �����Ұ���
    private int _nowSpanwer = 0;        // ���� ������ ���� index
    private int _nowCnt;                // ���� �ʵ忡 �ִ� ���� ����
    private int _minEnemyCnt = 1;       // �ٸ� spawn���� �Ѿ �ּ� ����

    // ������Ƽ
    public int nowCnt { get => _nowCnt; set { _nowCnt = value; } }

    bool flag = true;

    private void Start()
    {
        instance = this;        // �̱���
        // enemy�� die ���� �� �� createInit�� --

        _nowCnt = _createInit;  // �ʱ�ȭ
    }

    // UI Manager���� Button Event�� ����� 
    public void startDungeon() 
    {
        Debug.Log("������ ���� �մϴ�.");
        _returnPotal.SetActive(false);

        GameManager.instance.player.transform.position = _playerDungeonStart.position;
        StartCoroutine(DungeinFlow());
    }

    IEnumerator DungeinFlow()
    {
        for (_nowSpanwer = 0; _nowSpanwer < _spawner.Length;) 
        {
            // 1. �� ����
            CreateEnemy(_nowSpanwer);

            // 2. ���� �� _nowCnt �˻� (�ʵ忡 �����ִ� ���� ���� ��)
            while (!Checkcount()) 
            {
                yield return new WaitForSeconds(0.1f);
            }
            // 0.1�� ���� while���� �ݺ�, CheckCount�� ������ �˻���
            // ������ �¾Ƽ� true�� �Ǹ�? 

            // 3. nowSpanwer ����
            _nowSpanwer++;
        }

        Debug.Log("������ ���ư��� ��Ż�� ���Ƚ��ϴ�");
        _returnPotal.SetActive(true);

    }

    bool Checkcount()
    {
        // _nowCnt�� 0 �� �Ǹ� true ��ȯ
        if (_nowCnt < _minEnemyCnt)
            return true;
        return false;
    }

    private void CreateEnemy(int v_cnt)
    {
        _nowCnt = _createInit;              // ���� �ʵ忡 �ִ� ���� �� �ʱ�ȭ

        Vector3 _geneVec = Vector3.zero;
        float _rx;
        float _rz;

        for (int i = 0; i < _createInit; i++) 
        {
            int idx = returnRandNum();                                  // ���� �ε���
            GameObject obj = EnemyPooling.instance.getEnemy(idx);       // ���� pool���� get
            
            obj.transform.parent        = _spawner[v_cnt];              // spawner�� ��ġ �Ʒ��� ������Ʈ ����
            obj.transform.position      = _spawner[v_cnt].position;      // spqnwer�� ��ġ�� ��ġ ����

            _rx = Random.Range(0 , 1f);
            _rz = Random.Range(0 , 1f);

            _geneVec = new Vector3( _rx, _rz);
            obj.transform.localPosition = _geneVec;
        }

    }

    int returnRandNum() 
    {
        int _min = 0;
        int _max = EnemyManager.instance.enemyCnt;      // Enemy ������ŭ
        int range = Random.Range(_min, _max);           // ���� index

        return range;
    }

}
