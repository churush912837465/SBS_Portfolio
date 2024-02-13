using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField]
    Transform spawner;

    // UI Manager���� Button Event�� ����� 
    public void startDungeon() 
    {
        StartCoroutine(IE_startDungeon());
    }

    IEnumerator IE_startDungeon()
    {
        Debug.Log("������ ���� �մϴ�.");

        while (true) 
        {
            int idx = returnRandNum();                      // ���� �ε���
            // pool ���� idx�� �ش��ϴ� Enemy ��������
            GameObject obj = EnemyPooling.instance.getEnemy(idx);
            obj.transform.parent = spawner;                 // spawner�� ��ġ �Ʒ��� ������Ʈ ����
            obj.transform.position = spawner.position;      // spqnwer�� ��ġ�� ��ġ ����

            yield return new WaitForSeconds(1f);
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
