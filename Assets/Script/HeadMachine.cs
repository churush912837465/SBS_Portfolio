using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMachine
{
    public FSM currSkill;   // ���� ��ų
    public FSM preSkill;    // ���� ��ų

    // Player�� ���� �� �� Start���� ����� , Idle�� �ʱ� ����
    public void SetState(FSM sk)
    {
        currSkill = sk;
    }

    public void H_Begin()
    {
        if (currSkill == null)  // ���°� ������ ������� (����ó��)
            return;

        currSkill.Begin();      // ���� ���°� �����̵���, skill ��ũ��Ʈ�� begin() ����
    }

    public void H_Run() 
    {
        if(currSkill == null) 
            return;

        currSkill.Run();        // ���� ���°� �����̵���, skill ��ũ��Ʈ�� Run() ����
    }

    public void H_End() 
    {
        if (currSkill == null)
            return;

        currSkill.End();        // ���� ���°� �����̵���, skill ��ũ��Ʈ�� End() ����    
    }

    internal void ChangeState(FSM chageSk)
    {
        // ���¸� �ٲ� ��,
        // 1. �ڵ�� -> �ڵ�� ���� ���� ��ȭ x
        if (currSkill == preSkill)
            return;

        // 2. ���� ��ų�� �ִٸ�? -> ���� �� ��ų ��ȭ�� ��
        if (currSkill != null)
            currSkill.End();    // ���� ��ų�� End() �޼��� ����

        // 3. ���� ��ų�� ���� ��ų�� �ȴ�.
        preSkill = currSkill;

        // 4. ���� ��ų�� ���� ���ο� ��ų�� �ȴ�.
        currSkill = chageSk;

        // 5. ���� ��ų�� Begin�� ����
        if(currSkill != null)
            currSkill.Begin();

    }
}
