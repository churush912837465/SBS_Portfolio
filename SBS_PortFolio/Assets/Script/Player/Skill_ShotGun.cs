using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ShotGun : FSM
{
    private PlayerManager player;

    public Skill_ShotGun(PlayerManager playerManager)
    {
        this.player = playerManager;
    }

    public override void Begin()
    {
        //Debug.Log("Player의 ShotGun 실행");
        player.currSkill = PlayerSkill_State.ShotGun;   // 현재 상태 설정

        player.setBulletDB(player.shootgunIdx);         // 샷건 DB 할당

        player.startShoot(player.getCoruShootGun);      // 총 쏘는 코루틴 실행
    }
    public override void Run()
    {
        if (player.getisChange == true)
            player.ChangeSkill(PlayerSkill_State.Idle); //  Idle 상태로 변환
    }

    public override void End()
    {
        //Debug.Log("샷건 멈춤");
        player.stopShoot(player.getCoruShootGun);       // 총 쏘는 코루틴 종료

        player.preSkilll = PlayerSkill_State.ShotGun;   // 현재 상태 설정
    }


}
