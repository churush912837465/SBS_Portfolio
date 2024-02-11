using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_HandGun : FSM
{
    [Header("플레이어 ")]
    private PlayerManager player;

    [Header("회전 변수")]
    float playerY;
    float rotSpeed = 800f;
    int maxTrun = 3;

    public Skill_HandGun(PlayerManager playerManager)
    {
        this.player = playerManager;
    }

    public override void Begin()
    {
        //Debug.Log("Player의 HandGun 실행");
        player.currSkill = PlayerSkill_State.HandGun;   // 현재 상태 설정

        player.setBulletDB(player.handgunIdx);          // 핸드건 DB 할당

        playerY = player.transform.rotation.y;          // 플레이어의 현재 y 회전값
        player.startShoot(player.getsCoruHandGun);      // 총 쏘는 코루틴 실행
    }

    public override void Run()
    {
        // 회전값 더하기
        // 오일러 방식 : 0 ~ 360
        // 쿼터니언 방식 : 0 ~ 180 / -180 ~ 0
        playerY += rotSpeed * Time.deltaTime;
        player.transform.rotation = Quaternion.Euler(0, playerY, 0);

        // 오일러 방식으로 출력
        if (playerY >= 360 * maxTrun)
        {
            // 최대로 회전 하면 -> idle로 상태 변환
            player.ChangeSkill(PlayerSkill_State.Idle);
        }

    }

    public override void End()
    {
        //Debug.Log("핸드건 멈춤");
        player.stopShoot(player.getsCoruHandGun);     // 총쏘는 코루틴 멈춤
        player.preSkilll = PlayerSkill_State.HandGun;
    }


}
