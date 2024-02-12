using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_HandGun : FSM
{
    [Header("�÷��̾� ")]
    private PlayerManager player;

    [Header("ȸ�� ����")]
    float playerY;
    float rotSpeed = 800f;
    int maxTrun = 3;

    public Skill_HandGun(PlayerManager playerManager)
    {
        this.player = playerManager;
    }

    public override void Begin()
    {
        //Debug.Log("Player�� HandGun ����");
        player.currSkill = PlayerSkill_State.HandGun;   // ���� ���� ����

        player.setBulletDB(player.handgunIdx);          // �ڵ�� DB �Ҵ�

        playerY = player.transform.rotation.y;          // �÷��̾��� ���� y ȸ����
        player.startShoot(player.getsCoruHandGun);      // �� ��� �ڷ�ƾ ����
    }

    public override void Run()
    {
        // ȸ���� ���ϱ�
        // ���Ϸ� ��� : 0 ~ 360
        // ���ʹϾ� ��� : 0 ~ 180 / -180 ~ 0
        playerY += rotSpeed * Time.deltaTime;
        player.transform.rotation = Quaternion.Euler(0, playerY, 0);

        // ���Ϸ� ������� ���
        if (playerY >= 360 * maxTrun)
        {
            // �ִ�� ȸ�� �ϸ� -> idle�� ���� ��ȯ
            player.ChangeSkill(PlayerSkill_State.Idle);
        }

    }

    public override void End()
    {
        //Debug.Log("�ڵ�� ����");
        player.stopShoot(player.getsCoruHandGun);     // �ѽ�� �ڷ�ƾ ����
        player.preSkilll = PlayerSkill_State.HandGun;
    }


}
