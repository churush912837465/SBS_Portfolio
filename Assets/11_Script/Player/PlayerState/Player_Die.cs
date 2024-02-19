using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Die : FSM
{
    private PlayerManager player;

    public Player_Die(PlayerManager playerManager)
    {
        this.player = playerManager;
    }

    public override void Begin()
    {
        player.currSkill = Player_State.Die;    // ���� ���� ����
        player.PlayerDieAction();               // �÷��̾� ��� �׼� ����
    }

    public override void End()
    {

    }

    public override void Run()
    {
        player.preSkilll = Player_State.Die;   // ���� ���� ����
    }
}
