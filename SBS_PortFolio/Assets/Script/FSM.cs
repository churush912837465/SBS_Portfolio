using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSM
{
    // 상태 시작 시 1회
    public abstract void Begin();

    // 상태 매 프레임 실행
    public abstract void Run();

    // 상태 끝날 때 1회
    public abstract void End();

}
