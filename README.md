# SBS_Portfolio

## 1. PlayerManager 플레이어 
  + 1-1. Sorceress 의 상위 클래스
  + 1-2. 변수
    + Skill 변수로 현재 사용한 스킬에 대한 정보 저장 
    + Skill[] 배열로 인스펙터 창에서 드래그해서 저장
      + Skill[0] : thunderSkill
      + Skill[1] : fireSkill
      + Skill[2] : EarthSkill
      + Skill[3] : WaterSKill
  + 1-3. 함수
    + 1. PlayerMove
    + 2. abstract PlayerUseSkill()
    + 3. PlayerPlaySkill( idx , Transform)
      + 1. Skill 변수에 Skill[idx] 를 저장해놓음
      + 2. Skill[idx]의 UserSkill( idx, trasform )을 사용
      + 3. canMove 변수를 false로 바꾸고, N 초 후에 true로 변환
    + 4. PlayerReturnSKillDamage
      + 1. 현재 SKill의 min, max 데미지사이의 random값을 반환함   

## 2. Sorceress c
  + 2-1. PlayerUseSkill 사용
    + z를 누르면 PlayerPlaySkill(스킬 인덱스, 스킬 시작 위치);
    + x를 누르면 ``
    + c를 누르면 ``
    + v를 누르면 ``   

## 3. abstract Skill
  + 3-1. 상위 Skill 스크립트
    + Skill의 데이터 ( idx, 이름, aniName , 데미지 등등 )
    + abstract Init()
    + virtual SkillUse ( 애니메이터, 스킬 시작 trsf )
      + 1. animator의 aniName 애니메이션 실행
      + 2. SkillBall을 pool 에서 get 함 / GetSkillBall(idx)
      + 3. get 한 SkillBall의 위치를 trsf 로
  + 3-2. 하위 ThunderSkill / fireSkill / EarthSkill / WaterSkill
    + override Init()
      + 해당 스킬에 대한 필드를 초기화
    + override SkillUse( 애니메이터, 스킬 시작 trsf )
      + base의 SkillUse를 사용 

## 4. Skill Ball
  + 1. ParicleSystem[] 배열 가지고있음
      + [0] : skillBall 시작 시 
      + [1] : skillBall play 시 
      + [2] : skillBall 끝날 시 
  + 2. SkillBallPooling에서 초기생성 할 때 InitParticle(GameObj[] _obj) 받음
       + ParicleSystem[] 배열에 저장
  + 3. OnEnable()
    + Skill 스크립트에서 해당 Ball을 get 할때 OnEnable 실행됨
    + rb의 mass를 10f로 지정 , OnEnable 된 후 바닥으로 떨어지게
    + start particle / play particle 실행
  + 4. OnCollisionEnter()
    + 땅이랑 충돌 시
    + 몬스터랑 충돌 시
      + end particle 실행
      + N초 후 모든 파티클 끔, mass를 0 , pool로 return

## 5. Skill Ball Pooling
  + 1. 해당 Skill Ball의 ParticleSystem GameObject 저장
  + 2. Skill Ball Create
      + Skill Ball을 instantiate한 후 SkillBall의 InitParticle( GameObj[] _obj ) 을 매개변수로 넘김
  + 3. SkillBall get(idx)
      + 해당 idx에 해당하는 SkillBall을 return
  + 4. SkillBall return( SkillBall, idx )
      + SkillBall을 active false     

## 5. Enemy
  + Enemy DB 생성 (scriptable object)
  + 4-0. FSM 으로 구현
    + idle / tracking / attack / get damage / die 상태가 존재
    + idle : pool 안에 들어가 있을 때의 상태
    + tracking : 범위 안에 들어오면 플레이어를 tracking
    + attack : sight안에 들어오면 공격시작
    + die : 어느 상태에서든지 hp가 0이하이면 die 상태
      + enemyPoolManager의 return을 실행함
      
  + 4-1. Enemy Manager 스크립트
    + Enemy DB List
    + Enemy Prefab List 
      + 각 몬스터 프리팹은 Enemy 스크립트를 보유하고 있음

  + 4-2. EnemyPooling
    + EnemyManager에 접근해서 index에 해당하는 Prefab을 가져옴
      + Enemy의 DB에 해당하는 EnemyDB를 넣어줌 
    + Enemy get(); 
    + Enemy return();
      + 부모 지정 후 
      + poolManager의 return함수에서 오브젝트의 active를 끔

  + 4-3. EnemyParent
    + 하위 Enemy의 부모 클래스
    + FSM을 초기화하는 메서드

  + 4-4. Enemy 스크립트 : EnemyParent
    + EnemyDB 변수를 가지고있음, pooling에서 생성할 때 EnemyDB할당
    + start에서 FSM 초기화
    + onEnable 초기에 시작될 때 작동 안되도록 bool 조건 걸어줌
      + pooling에서 나올 떄 ( onEnable 될 때)
        + 1. 상태를 Tracking으로 변화
        + 2. FSM스크립트의 Run()을 매프레임 실행하는 코루틴 start 
    + disEnable
        + 1. (Run을 매프레임 실행하는) 코루틴 stop

  + 4-5. 충돌감지
        + 1. Oncollision
            + 1. 플레이어랑 충돌 시
               + playerManager의 GetDamgae 실행
            + 2. Skill Ball (tag가 Skill) 충돌 시
              + PlayerManager의 SkillDamage를 return 하는 함수 실행 후 hp에서 (-)     
        + 2. OnParicleCollision
            + PlayerManager의 SkillDamage를 return 하는 함수 실행 후 hp에서 (-)     

## 6. DungeonManager
  + startDungeon() 메서드
    + DungeinFlow() 코루틴 사용
  + IE_DungeinFlow()
    + 0. _nowSpanwer = 0 , spanwer.Length 만큼 for문 실행
      + 1. CreateEnemy(_nowSpanwer)
      + 2. 생성 _nowCnt 검사 : _nowCnt가 
        + while을 yield return new WaitForSeconds(0.1f); 마다 실행 (조건검사)
      + 3. while문을 빠져나오면 _nowSpanwer를 (++)
    + 4. for문을 빠져나오면 _returnPotal를 
  + CreateEnemy( spanwer idx ) 
    + 랜덤 idx를 구한 후 pool 에서 getEnemy(idx)
    + spanwer idx에 해당하는 위치로

## 999. GameNamager
  + 1. PlayerManager

## 999. Uimanager 
  + 1. 던전 입장 버튼
    + 온클릭 AddListener 사용
      + 클릭시 GameManager의 startDungeon 메서드 실행
   
