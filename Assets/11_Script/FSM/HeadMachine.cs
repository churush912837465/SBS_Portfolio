using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeadMachine
{
    public FSM currState;   // 현재 스킬
    public FSM preState;    // 이전 스킬

    // 상태를 초기 세팅
    public void SetState(FSM sk)
    {
        currState = sk;

        if (currState != sk && currState != null)
            preState = currState;
    }

    public void H_Begin()
    {
        if (currState == null)  // 상태가 없으면 실행못함 (예외처리)
            return;

        currState.Begin();      // 현재 상태가 무엇이든지, skill 스크립트의 begin() 실행
    }

    public void H_Run() 
    {
        if(currState == null) 
            return;

        currState.Run();        // 현재 상태가 무엇이든지, skill 스크립트의 Run() 실행
    }

    public void H_End() 
    {
        if (currState == null)
            return;

        currState.End();        // 현재 상태가 무엇이든지, skill 스크립트의 End() 실행    
    }

    internal void ChangeState(FSM chageSk)
    {
        // 상태를 바꿀 때,
        // 1. 핸드건 -> 핸드건 으로 상태 변화 x
        if (currState == preState)
            return;

        // 2. 현재 스킬이 있다면? -> 종료 후 스킬 변화를 함
        if (currState != null)
            currState.End();    // 현재 스킬의 End() 메서드 실행

        // 3. 이전 스킬은 현재 스킬이 된다.
        preState = currState;

        // 4. 현재 스킬은 들어온 새로운 스킬이 된다.
        currState = chageSk;

        // 5. 현재 스킬의 Begin을 실행
        if(currState != null)
            currState.Begin();

    }
}
