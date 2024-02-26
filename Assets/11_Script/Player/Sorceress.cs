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
    // �Լ� ����
    public override void InitSkill()
    {
        _playerSkill = new Skill[System.Enum.GetValues(typeof(SorceressSkill)).Length];
        _playerSkill[0] = new ThunderSkill();
        _playerSkill[1] = new FireSkill();
        _playerSkill[2] = new WindSkill();
        _playerSkill[3] = new IceSkill();
    }

    public override void PlayerUseSkill()
    {
        // ���� Ű�� ������ �� ��ų�� ���ǰ�
        // Skill�迭�� SkillUser�� ���

        if (Input.GetKeyDown(KeyCode.Q))
            PlayerPlaySkillAni(0);

        else if (Input.GetKeyDown(KeyCode.W))
            PlayerPlaySkillAni(1);

        else if (Input.GetKeyDown(KeyCode.E))
            PlayerPlaySkillAni(2);

        else if (Input.GetKeyDown(KeyCode.R))
            PlayerPlaySkillAni(3);

    }

    
    // ����
    private void Start()
    {
        InitPlayerData();
        InitSkill();
    }

    private void Update()
    {
        PlayerUseSkill();
    }


}
