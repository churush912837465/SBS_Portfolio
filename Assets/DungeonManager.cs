using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public void startDungeon() 
    {
        StartCoroutine(IE_startDungeon());
    }

    IEnumerator IE_startDungeon()
    {
        Debug.Log("������ ���� �մϴ�.");

        while (true) 
        {
            Debug.Log("enemy�� �����մϴ�");
            yield return new WaitForSeconds(1f);
        }

    }

}
