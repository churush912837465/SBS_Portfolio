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
    [Header("�̱���")]
    public static GameManager instance;

    [Header("�������")]
    public nowState state;                   // ���� ���� 

    [Header("������Ʈ")]
    public GameObject player;
    public PlayerManager playerManager;
    public GameObject dungeon;
    public DungeonManager dungeonManager;

    private void Awake()
    {
        if(instance == null) 
        { 
            // instance �� ������ �־��ֱ�
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
        Debug.Log("������ ���� �մϴ�");
        dungeonManager.startDungeon();
    }
}
