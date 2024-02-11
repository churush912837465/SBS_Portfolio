using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Lifle : FSM
{
    private PlayerManager player;

    public Skill_Lifle(PlayerManager playerManager)
    {
        this.player = playerManager;
    }

    public override void Begin()
    {
        //Debug.Log("Player의 Lifle 실행");
        player.currSkill = PlayerSkill_State.Lifle;   // 현재 상태 설정

        player.setBulletDB(player.lifleIdx);        // 라이플 DB 할당

        player.startShoot(player.getsCoruLifle);      // 총 쏘는 코루틴 실행
    }

    public override void Run()
    {
        if (player.getisChange == true)
            player.ChangeSkill(PlayerSkill_State.Idle); //  Idle 상태로 변환
    }

    public override void End()
    {
        player.preSkilll = PlayerSkill_State.Lifle;   // 현재 상태 설정
    }


}
