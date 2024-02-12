using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum nowState 
{
    Village,
    Dungeon
}

public class GameManager : MonoBehaviour
{
    [Header("싱글톤")]
    public static GameManager instance;

    [Header("현재상태")]
    public nowState state;                   // 현재 상태 

    [Header("컴포넌트")]
    public GameObject player;
    public PlayerManager playerManager;
    public GameObject dungeon;
    public DungeonManager dungeonManager;

    private void Awake()
    {
        if(instance == null) 
        { 
            // instance 가 없으면 넣어주기
            instance = this;
        }
        else 
        { 
            Destroy(instance);
        }
    }

    void Start()
    {
        playerManager   = player.GetComponent<PlayerManager>();
        dungeonManager  = dungeon.GetComponent<DungeonManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DungeonEnter() 
    {
        Debug.Log("던전에 입장 합니다");
        dungeonManager.startDungeon();
    }
}
