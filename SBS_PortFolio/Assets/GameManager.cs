using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public PlayerManager playerManager;

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
        playerManager = player.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
