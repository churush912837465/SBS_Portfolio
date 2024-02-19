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
        player.currSkill = Player_State.Idle;   // ���� ���� ����
        
        player.CanMove = true;                    // idle ���¿����� ������ �� 0

    }

    public override void Run()
    {
        if(player.PlayerHpUnderZero())                         // hp�� 0 ���ϸ� 
            player.ChangeSkill(Player_State.Die);              // Die�� ���º�ȭ

        if (Input.GetKeyDown(KeyCode.Q))
        {
            // ���� ��ȭ -> �ڵ�� 
            player.setBulletDB(player.handgunIdx);             // �ڵ�� DB �Ҵ�              
            player.ChangeSkill(Player_State.HandGun);

        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // ���� ��ȭ -> ����
            player.setBulletDB(player.shootgunIdx);             // ���� DB �Ҵ�
            player.ChangeSkill(Player_State.ShotGun);

        }
        else if (Input.GetKeyDown(KeyCode.R)) 
        {
            // ���� ��ȭ -> ������ 
            player.setBulletDB(player.lifleIdx);                // ������ DB �Ҵ�
            player.ChangeSkill(Player_State.Lifle);
        }
    }

    public override void End()
    {
        player.CanMove = false;                  // idle ���°� �ƴϸ� ������ �� x

        player.preSkilll = Player_State.Idle;   // ���� ���� ����
    }



}
