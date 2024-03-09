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

    [Header("Script")]
    public PlayerManager playerManager;
    public DungeonManager dungeonManager;
    public Inventory inventory;
    public ItemManager itemManager;
    public GoodsManager goodsManager;
    public StoreManager storeManager;
    public GoodsUI goodsUi;

    [Header("던전")]
    public float generationTime;

    private void Awake()
    {
        if (instance == null)
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
        playerManager = player.GetComponent<PlayerManager>();
        dungeonManager = dungeon.GetComponent<DungeonManager>();

        generationTime = 20f;

    }

    #region 임시 아이템 획득 사용
    
    public void DungeonEnter() 
    {
        dungeonManager.startDungeon();

    }

    public void PlayerGetPortion() 
    {
        itemManager.PlayerGetPortion(PortionType.potion);
    }

    public void PlayerGetBomb() 
    {
        itemManager.PlayerGetBomb(BombType.destroyBomb);
    }

    public void PlayerGetClothes() 
    {
        itemManager.PlayerGetEquip(0);
    }

    internal void PlayerGetAccessory()
    {
        itemManager.PlayerGetAccessory(0);
    }
    #endregion
}
