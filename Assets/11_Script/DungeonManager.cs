using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField]
    Transform[] _spawner;
    [SerializeField]
    Transform _playerDungeonStart;

    // UI Manager���� Button Event�� ����� 
    public void startDungeon() 
    {
        GameManager.instance.player.transform.position = _playerDungeonStart.position;
        GameManager.instance.playerManager.PlayerRot = 180f;
        GameManager.instance.playerManager.MoveDir = 1f;

        StartCoroutine(IE_startDungeon());
    }

    IEnumerator IE_startDungeon()
    {
        Debug.Log("������ ���� �մϴ�.");

        // �� ���̺� 5��
        // 1���̺꿡 ���� 25���� pool���� get , ��ġ�� ������ ��ó�� ������ġ��
        // ���� ��Ӹ����� ������ �ִٰ� ���� ������ ���ϰ� �Ǹ� �ٸ� ���̺꿡�� ���� ���� �ݺ�
        // �� ó���ϸ� ������ ��ġ�� ������ ���ư��� ��Ż ����



        while (true) 
        {
            int idx = returnRandNum();                      // ���� �ε���
            // pool ���� idx�� �ش��ϴ� Enemy ��������
            GameObject obj = EnemyPooling.instance.getEnemy(idx);
            obj.transform.parent = _spawner[1];                 // spawner�� ��ġ �Ʒ��� ������Ʈ ����
            obj.transform.position = _spawner[1].position;      // spqnwer�� ��ġ�� ��ġ ����

            yield return new WaitForSeconds(GameManager.instance.generationTime);
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
