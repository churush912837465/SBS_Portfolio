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
        Debug.Log("던전에 입장 합니다.");

        while (true) 
        {
            Debug.Log("enemy를 생성합니다");
            yield return new WaitForSeconds(1f);
        }

    }

}
