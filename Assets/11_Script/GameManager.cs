using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("싱글톤")]
    public static GameManager instance;

    [Header("컴포넌트")]
    public GameObject player;
    public GameObject dungeon;

    [Header("class")]
    public PlayerManager playerManager;
    public DungeonManager dungeonManager;
    public Inventory inventory;
    public ItemManager itemManager;

    [Header("던전")]
    public float generationTime;

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

        generationTime = 20f; 
    }

    #region UIManager 사용
    
    public void DungeonEnter() 
    {
        Debug.Log("던전에 입장 합니다");
        dungeonManager.startDungeon();

    }

    public void PlayerGetPortion() 
    {
        itemManager.PlayerGetPortion();
    }

    public void PlayerGetBomb() 
    {
        itemManager.PlayerGetBomb();
    }

    public void PlayerGetClothes() 
    {
        itemManager.PlayerGetEquip();
    }

    internal void PlayerGetAccessory()
    {
        itemManager.PlayerGetAccessory();
    }
    #endregion
}
