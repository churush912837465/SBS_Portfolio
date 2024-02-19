using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Idle : FSM
{
    private PlayerManager player;

    public Skill_Idle(PlayerManager playerManager)
    {
        this.player = playerManager;
    }

    public override void Begin()
    {
        //Debug.Log("Player의 Idle 실행");
        player.currSkill = Player_State.Idle;   // 현재 상태 설정
        
        player.CanMove = true;                    // idle 상태에서는 움직일 수 0

    }

    public override void Run()
    {
        if(player.PlayerHpUnderZero())                         // hp가 0 이하면 
            player.ChangeSkill(Player_State.Die);              // Die로 상태변화

        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 상태 변화 -> 핸드건 
            player.setBulletDB(player.handgunIdx);             // 핸드건 DB 할당              
            player.ChangeSkill(Player_State.HandGun);

        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // 상태 변화 -> 샷건
            player.setBulletDB(player.shootgunIdx);             // 샷건 DB 할당
            player.ChangeSkill(Player_State.ShotGun);

        }
        else if (Input.GetKeyDown(KeyCode.R)) 
        {
            // 상태 변화 -> 라이플 
            player.setBulletDB(player.lifleIdx);                // 라이플 DB 할당
            player.ChangeSkill(Player_State.Lifle);
        }
    }

    public override void End()
    {
        player.CanMove = false;                  // idle 상태가 아니면 움직일 수 x

        player.preSkilll = Player_State.Idle;   // 이전 상태 설정
    }



}
