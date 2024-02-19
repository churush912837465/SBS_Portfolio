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
        //Debug.Log("Player�� ShotGun ����");
        player.currSkill = Player_State.ShotGun;   // ���� ���� ����

        player.setBulletDB(player.shootgunIdx);         // ���� DB �Ҵ�

        player.startShoot(player.getCoruShootGun);      // �� ��� �ڷ�ƾ ����
    }
    public override void Run()
    {
        if (player.getisChange == true)
            player.ChangeSkill(Player_State.Idle); //  Idle ���·� ��ȯ
    }

    public override void End()
    {
        //Debug.Log("���� ����");
        player.stopShoot(player.getCoruShootGun);       // �� ��� �ڷ�ƾ ����

        player.preSkilll = Player_State.ShotGun;   // ���� ���� ����
    }


}
