using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SorceressSkill 
{
    thunderSkill,
    fireSkill,
    windSKill,
    iceSkill
}

public class Sorceress : PlayerManager
{
    [Space]
    [Header("Sorceress")]
    [SerializeField]
    Transform _masicFarStart;
    [SerializeField]
    Transform _masicCloseStart;

    // 함수 구현
    public override void InitSkill()
    {
        //_playerSkill = new Skill[System.Enum.GetValues(typeof(SorceressSkill)).Length];
        // 인스펙터 창에서 각 Skill 스크립트를 드래그 해서 넣어줌
        /*
        _playerSkill[0] = new ThunderSkill();   // 천벌
        _playerSkill[1] = new FireSkill();      // 종말의 날
        _playerSkill[2] = new WindSkill();      // 혹한의 부름
        _playerSkill[3] = new IceSkill();       // 아이스 애로우
        */
    }

    public override void PlayerUseSkill()
    {
        // 일정 키를 눌렀을 때 스킬이 사용되게
        // Skill배열의 SkillUser를 사용

        if (Input.GetKeyDown(KeyCode.Q))            // thunder
            PlayerPlaySkill(0 , _masicFarStart);

        else if (Input.GetKeyDown(KeyCode.W))       // fire
            PlayerPlaySkill(1 , _masicFarStart);

        else if (Input.GetKeyDown(KeyCode.E))       // wind
            PlayerPlaySkill(2 , _masicCloseStart);   

        else if (Input.GetKeyDown(KeyCode.R))       // ice
            PlayerPlaySkill(3 , _masicFarStart);

    }

    
    // 실행
    private void Start()
    {
        InitPlayerData();
        //InitSkill();
    }

    private void Update()
    {
        PlayerUseSkill();
    }


}
