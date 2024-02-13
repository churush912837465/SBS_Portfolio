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
        player.currSkill = PlayerSkill_State.Idle;   // 현재 상태 설정
        
        player.getCanMove = true;                    // idle 상태에서는 움직일 수 0

    }

    public override void Run()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 상태 변화 -> 핸드건 
            player.setBulletDB(player.handgunIdx);             // 핸드건 DB 할당              
            player.ChangeSkill(PlayerSkill_State.HandGun);

        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // 상태 변화 -> 샷건
            player.setBulletDB(player.shootgunIdx);             // 샷건 DB 할당
            player.ChangeSkill(PlayerSkill_State.ShotGun);

        }
        else if (Input.GetKeyDown(KeyCode.R)) 
        {
            // 상태 변화 -> 라이플 
            player.setBulletDB(player.lifleIdx);                // 라이플 DB 할당
            player.ChangeSkill(PlayerSkill_State.Lifle);
        }
    }

    public override void End()
    {
        player.getCanMove = false;                  // idle 상태가 아니면 움직일 수 x

        player.preSkilll = PlayerSkill_State.Idle;   // 이전 상태 설정
    }



}
