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
        //Debug.Log("Player�� Idle ����");
        player.currSkill = PlayerSkill_State.Idle;   // ���� ���� ����
        
        player.getCanMove = true;                    // idle ���¿����� ������ �� 0

    }

    public override void Run()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // ���� ��ȭ -> �ڵ�� 
            player.setBulletDB(player.handgunIdx);             // �ڵ�� DB �Ҵ�              
            player.ChangeSkill(PlayerSkill_State.HandGun);

        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // ���� ��ȭ -> ����
            player.setBulletDB(player.shootgunIdx);             // ���� DB �Ҵ�
            player.ChangeSkill(PlayerSkill_State.ShotGun);

        }
        else if (Input.GetKeyDown(KeyCode.R)) 
        {
            // ���� ��ȭ -> ������ 
            player.setBulletDB(player.lifleIdx);                // ������ DB �Ҵ�
            player.ChangeSkill(PlayerSkill_State.Lifle);
        }
    }

    public override void End()
    {
        player.getCanMove = false;                  // idle ���°� �ƴϸ� ������ �� x

        player.preSkilll = PlayerSkill_State.Idle;   // ���� ���� ����
    }



}
