using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField]
    Transform[] _spawner;
    [SerializeField]
    Transform _playerDungeonStart;

    // UI Manager에서 Button Event로 실행됨 
    public void startDungeon() 
    {
        GameManager.instance.player.transform.position = _playerDungeonStart.position;
        GameManager.instance.playerManager.PlayerRot = 180f;
        GameManager.instance.playerManager.MoveDir = 1f;

        StartCoroutine(IE_startDungeon());
    }

    IEnumerator IE_startDungeon()
    {
        Debug.Log("던전에 입장 합니다.");

        // 총 웨이브 5개
        // 1웨이브에 몬스터 25마리 pool에서 get , 위치는 스포터 근처의 랜덤위치로
        // 몬스터 몇머리인지 가지고 있다가 일정 마리수 이하가 되면 다름 웨이브에서 몬스터 생성 반복
        // 다 처지하면 마지막 위치에 마을로 돌아가는 포탈 생성



        while (true) 
        {
            int idx = returnRandNum();                      // 랜덤 인덱스
            // pool 에서 idx에 해당하는 Enemy 가져오기
            GameObject obj = EnemyPooling.instance.getEnemy(idx);
            obj.transform.parent = _spawner[1];                 // spawner의 위치 아래로 오브젝트 생성
            obj.transform.position = _spawner[1].position;      // spqnwer의 위치로 위치 지정

            yield return new WaitForSeconds(GameManager.instance.generationTime);
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
