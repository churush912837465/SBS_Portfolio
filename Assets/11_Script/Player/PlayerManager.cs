using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public abstract class PlayerManager : MonoBehaviour
{
    /// <summary>
    ///  1. player 상위 스크립트
    ///  2. playerMove는 "PlayerMouseToMove"스크립트에서 관리
    ///  
    /// </summary>

    [Header("Init")]
    [SerializeField]
    protected PlayerData _playerData;
    [SerializeField]
    protected Skill[] _playerSkill;
    [SerializeField]
    Skill _currSkill;

    [Space]
    [Header("공통 필드")]
    [SerializeField]
    protected Animator _playerAnimator;
    [SerializeField]
    protected bool _canMove = true;
    private string _moveAni = "run";
    private string _dieAni = "die";
    private string _hitAni = "isHit";

    // 프로퍼티
    public bool CanMove { get => _canMove; }
    public string MoveAni { get => _moveAni; }

    // 함수
    public virtual void InitPlayerData()  // 본인 player data를 init (공통)
    {
        _playerData = new PlayerData();
        _playerData.HP = 100f;
        _playerData.MoveSpeed = 10f;
        _playerData.ATK = 10f;
        _playerData.EXP = 0;

        _playerAnimator = GetComponent<Animator>();
    }

    public virtual void PlayerGetDamage(float v_damage) // player 피격 (공통)
    {
        _playerData.HP -= v_damage;
        _playerAnimator.SetTrigger(_hitAni);

        if (PlayerHPIsUnderZero()) 
        {
            PlayerIsDie();
        }
    }

    public virtual void PlayerIsDie()   // player가 죽었을 때 행동
    {
        _playerAnimator.SetBool(_dieAni , true);
    }

    public virtual bool PlayerHPIsUnderZero()   // hp 검사
    {
        if (_playerData.HP <= 0)
            return true;
        else
            return false;
    }

    public virtual void PlayerPlaySkill(int v_idx , Transform posi)   // 해당 idx에 대한 스킬 실행 
    {
        _canMove = false;

        _currSkill  = _playerSkill[v_idx];              // 현재 스킬 저장
        _playerSkill[v_idx].SkillUse(_playerAnimator , posi);

        Invoke("AgainCanMove", 1f);     // 스킬 사용 후 Nf 후에 움직일 수 있도록
    }

    public virtual void AgainCanMove() 
    {
        _canMove = true;
    }

    public virtual float PlayerReturnSKillDamage()      // 현재 스킬에 대한 damage를 return
    {
        /*
        if (_currSkill == null)
            return 0;
        */

        float _ranDamage = Random.Range(_currSkill.MinDamage , _currSkill.MaxDamage);
        return _ranDamage;
    }

    public abstract void InitSkill();           // 본인 player 스킬을 init 
    public abstract void PlayerUseSkill();      // 본인 skill을 사용

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

}
