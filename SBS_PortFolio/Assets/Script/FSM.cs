using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSM
{
    // ���� ���� �� 1ȸ
    public abstract void Begin();

    // ���� �� ������ ����
    public abstract void Run();

    // ���� ���� �� 1ȸ
    public abstract void End();

}
