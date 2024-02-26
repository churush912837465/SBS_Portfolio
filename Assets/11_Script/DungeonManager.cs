using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField]
    Transform _spawner;
    [SerializeField]
    Transform _playerDungeonStart;

    // UI Manager에서 Button Event로 실행됨 
    public void startDungeon() 
    {
        GameManager.instance.TPSPlayer.transform.position = _playerDungeonStart.position;
        StartCoroutine(IE_startDungeon());
    }

    IEnumerator IE_startDungeon()
    {
        Debug.Log("던전에 입장 합니다.");

        while (true) 
        {
            int idx = returnRandNum();                      // 랜덤 인덱스
            // pool 에서 idx에 해당하는 Enemy 가져오기
            GameObject obj = EnemyPooling.instance.getEnemy(idx);
            obj.transform.parent = _spawner;                 // spawner의 위치 아래로 오브젝트 생성
            obj.transform.position = _spawner.position;      // spqnwer의 위치로 위치 지정

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
