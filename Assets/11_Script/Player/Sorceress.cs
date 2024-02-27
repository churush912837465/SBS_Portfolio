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
    Transform _masicStartPosi;

    // �Լ� ����
    public override void InitSkill()
    {
        //_playerSkill = new Skill[System.Enum.GetValues(typeof(SorceressSkill)).Length];
        // �ν����� â���� �� Skill ��ũ��Ʈ�� �巡�� �ؼ� �־���
        /*
        _playerSkill[0] = new ThunderSkill();   // õ��
        _playerSkill[1] = new FireSkill();      // ������ ��
        _playerSkill[2] = new WindSkill();      // Ȥ���� �θ�
        _playerSkill[3] = new IceSkill();       // ���̽� �ַο�
        */
    }

    public override void PlayerUseSkill()
    {
        // ���� Ű�� ������ �� ��ų�� ���ǰ�
        // Skill�迭�� SkillUser�� ���

        if (Input.GetKeyDown(KeyCode.Q))            // thunder
            PlayerPlaySkill(0 , _masicStartPosi);

        else if (Input.GetKeyDown(KeyCode.W))       // fire
            PlayerPlaySkill(1 , _masicStartPosi);

        else if (Input.GetKeyDown(KeyCode.E))       // wind
            PlayerPlaySkill(2 , this.transform );    // �� �� �ֺ����� ��ų ����

        else if (Input.GetKeyDown(KeyCode.R))       // ice
            PlayerPlaySkill(3 , _masicStartPosi);

    }

    
    // ����
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
