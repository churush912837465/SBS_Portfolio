using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeadMachine
{
    public FSM currState;   // ���� ��ų
    public FSM preState;    // ���� ��ų

    // ���¸� �ʱ� ����
    public void SetState(FSM sk)
    {
        currState = sk;

        if (currState != sk && currState != null)
            preState = currState;
    }

    public void H_Begin()
    {
        if (currState == null)  // ���°� ������ ������� (����ó��)
            return;

        currState.Begin();      // ���� ���°� �����̵���, skill ��ũ��Ʈ�� begin() ����
    }

    public void H_Run() 
    {
        if(currState == null) 
            return;

        currState.Run();        // ���� ���°� �����̵���, skill ��ũ��Ʈ�� Run() ����
    }

    public void H_End() 
    {
        if (currState == null)
            return;

        currState.End();        // ���� ���°� �����̵���, skill ��ũ��Ʈ�� End() ����    
    }

    internal void ChangeState(FSM chageSk)
    {
        // ���¸� �ٲ� ��,
        // 1. �ڵ�� -> �ڵ�� ���� ���� ��ȭ x
        if (currState == preState)
            return;

        // 2. ���� ��ų�� �ִٸ�? -> ���� �� ��ų ��ȭ�� ��
        if (currState != null)
            currState.End();    // ���� ��ų�� End() �޼��� ����

        // 3. ���� ��ų�� ���� ��ų�� �ȴ�.
        preState = currState;

        // 4. ���� ��ų�� ���� ���ο� ��ų�� �ȴ�.
        currState = chageSk;

        // 5. ���� ��ų�� Begin�� ����
        if(currState != null)
            currState.Begin();

    }
}
