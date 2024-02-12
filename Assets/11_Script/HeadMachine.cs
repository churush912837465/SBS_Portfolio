using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMachine
{
    public FSM currSkill;   // 현재 스킬
    public FSM preSkill;    // 이전 스킬

    // Player를 생성 할 때 Start에서 실행됨 , Idle로 초기 설정
    public void SetState(FSM sk)
    {
        currSkill = sk;
    }

    public void H_Begin()
    {
        if (currSkill == null)  // 상태가 없으면 실행못함 (예외처리)
            return;

        currSkill.Begin();      // 현재 상태가 무엇이든지, skill 스크립트의 begin() 실행
    }

    public void H_Run() 
    {
        if(currSkill == null) 
            return;

        currSkill.Run();        // 현재 상태가 무엇이든지, skill 스크립트의 Run() 실행
    }

    public void H_End() 
    {
        if (currSkill == null)
            return;

        currSkill.End();        // 현재 상태가 무엇이든지, skill 스크립트의 End() 실행    
    }

    internal void ChangeState(FSM chageSk)
    {
        // 상태를 바꿀 때,
        // 1. 핸드건 -> 핸드건 으로 상태 변화 x
        if (currSkill == preSkill)
            return;

        // 2. 현재 스킬이 있다면? -> 종료 후 스킬 변화를 함
        if (currSkill != null)
            currSkill.End();    // 현재 스킬의 End() 메서드 실행

        // 3. 이전 스킬은 현재 스킬이 된다.
        preSkill = currSkill;

        // 4. 현재 스킬은 들어온 새로운 스킬이 된다.
        currSkill = chageSk;

        // 5. 현재 스킬의 Begin을 실행
        if(currSkill != null)
            currSkill.Begin();

    }
}
