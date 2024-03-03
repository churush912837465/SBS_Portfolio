using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class PlayerManager : MonoBehaviour
{
    /// <summary>
    ///  1. player ���� ��ũ��Ʈ
    ///  2. playerMove�� "PlayerMouseToMove"��ũ��Ʈ���� ����
    ///  
    /// </summary>

    [Header("���� �ʵ�")]
    [Header("Init")]
    [SerializeField]
    protected PlayerData _playerData;
    [SerializeField]
    protected Skill[] _playerSkill;
    [SerializeField]
    protected Skill _currSkill;

    [Space]
    [SerializeField]
    protected Animator _playerAnimator;
    [SerializeField]
    protected bool _canMove     = true;
    private string _moveAni     = "run";
    private string _dieAni      = "die";
    private string _hitAni      = "isHit";

    // ������Ƽ
    public bool CanMove     { get => _canMove; }
    public string MoveAni   { get => _moveAni; }
    public Skill CurrSkill  { get => _currSkill;  }
    public PlayerData playerData { get => _playerData; }    

    // �Լ�
    public virtual void InitPlayerData()  
    {
        // ���� player data�� init (����)
        _playerData = new PlayerData();
        _playerData.HP              = 100f;
        _playerData.MoveSpeed       = 10f;
        _playerData.ATK             = 10f;
        _playerData.EXP             = 0;
        _playerData.AdditionalHp    = 0;  
        _playerData.PhyDefencity    = 0;
        _playerData.MasicDefencity  = 0;
        _playerData.Counter         = 0;

        // GetComponent
        _playerAnimator = GetComponent<Animator>();
    }


    public virtual void PlayerGetDamage(float v_damage) // player �ǰ� (����)
    {
        _playerData.HP -= v_damage;
        _playerAnimator.SetTrigger(_hitAni);

        if (PlayerHPIsUnderZero()) 
        {
            PlayerIsDie();
        }
    }

    public virtual void PlayerIsDie()   // player�� �׾��� �� �ൿ
    {
        Debug.Log("Player ����");
        _playerAnimator.SetBool(_dieAni , true);
    }

    public virtual bool PlayerHPIsUnderZero()   // hp �˻�
    {
        if (_playerData.HP <= 0)
            return true;
        else
            return false;
    }

    public virtual void PlayerPlaySkill(int v_idx , Transform posi)   // �ش� idx�� ���� ��ų ���� 
    {
        _canMove = false;

        _currSkill  = _playerSkill[v_idx];              // ���� ��ų ����
        _playerSkill[v_idx].SkillUse(_playerAnimator , posi);

        Invoke("AgainCanMove", 3f);     // ��ų ��� �� Nf �Ŀ� ������ �� �ֵ���
    }

    public virtual void AgainCanMove() 
    {
        _canMove = true;
    }

    public virtual float PlayerReturnSKillDamage()      // ���� ��ų�� ���� damage�� return
    {
        /*
        if (_currSkill == null)
            return 0;
        */

        float _ranDamage = Random.Range(_currSkill.MinDamage , _currSkill.MaxDamage);
        return _ranDamage;
    }

    public abstract void InitSkill();           // ���� player ��ų�� init 
    public abstract void PlayerUseSkill();      // ���� skill�� ���

    #region Use In PlayerMouseToMove

    public float ReturnMoveSpeed() 
    {
        return _playerData.MoveSpeed;
    }

    public void PlayerIsMoveAndPlayerAni(bool v_move) 
    {
        _playerAnimator.SetBool(MoveAni, v_move);
    }

    #endregion

    #region Use Item ������ ����

    // Portion ��� -> Hp ����
    internal void UserPortion(float healingAmont)
    {
        float _nowHp = _playerData.HP;
        _nowHp += healingAmont;
        _playerData.HP = _nowHp;

        Debug.Log(_playerData.HP);
    }

    // playerInfoUi ���� equip ���� �� �߰�ü��
    public virtual void AddPlayerHP(float v_addHP)
    {
        _playerData.HP += v_addHP;                  // �� hp += �߰� hp
        _playerData.AdditionalHp += v_addHP;

        //Debug.Log("���� ü���� " + _playerData.HP);
    }
    // playerInfoUi ���� equip ���� �� �߰� ��������
    public virtual void AddPhyDefen(float v_addPD)
    {
        _playerData.PhyDefencity += v_addPD;
        //Debug.Log("���� ���� ���ݷ��� " + _playerData.PhyDefencity);
    }
    // playerInfoUi ���� equip ���� �� �߰� ���� ����
    public virtual void AddMasicDefen(float v_addMD)
    {
        _playerData.MasicDefencity += v_addMD;
        //Debug.Log("���� ���� ���ݷ��� " + _playerData.MasicDefencity);
    }

    public virtual void AddCounter(float v_cnter) 
    {
        _playerData.Counter += v_cnter;
        //Debug.Log("���� ġ��Ÿ���� " + _playerData.Counter);
    }
    #endregion

}
