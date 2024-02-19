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
        //Debug.Log("Player�� Lifle ����");
        player.currSkill = Player_State.Lifle;   // ���� ���� ����

        player.setBulletDB(player.lifleIdx);        // ������ DB �Ҵ�

        player.startShoot(player.getsCoruLifle);      // �� ��� �ڷ�ƾ ����
    }

    public override void Run()
    {
        if (player.getisChange == true)
            player.ChangeSkill(Player_State.Idle); //  Idle ���·� ��ȯ
    }

    public override void End()
    {
        player.preSkilll = Player_State.Lifle;   // ���� ���� ����
    }


}
