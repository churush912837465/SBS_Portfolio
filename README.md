# SBS_Portfolio

1. PlayerManager 플레이어
  + 기본 playerManager 생성 (싱글톤)
  + 1-1. BulletDB(SciptablaObject) 변수 존재
    + : Player에서 총알의 DB를 관리함 (총알에서 관리하는거 x)
  + 1-2. Player가 bullet을 get해서 사용
        + Skill_idle 스크립트에서 (idle 상태 일 때)
        + q를 누르면 : DB에 handGunDB  담김
        + e를 누르면 : DB에 shootGunDB 담김
        + r를 누르면 : DB에 lifleGunDb 담김

2. 총알
  + 2-1. GunSlinerBullet 스크립트 (Bullet Pooling 구현)
    + ScriptableObject형의 BulletDB 3종류를 List로 가지고 있음
    + bulletBase 오브젝트를 Pool에 생성
    + Bullet get(); 구현완
    + Bullet return(); 구현 완
         
  + 2-2. Bullet
    + OnEnable될 때
      + 일정 시간 후(PlayerManager의 BulletDB의 lifeTime에 접근) Bullet pool로 return
    + enemy랑 충돌
      + Bullet pool로 return 

3.스킬
3-1. 구현
  + FSM으로 구현
  + 스킬 3종
  + 1. 핸드건
    + 오브젝트를 회전, 회전이 360 * 3 이상이 되면 ChangeSkill() 
    + 총발사 코루틴 사용
  + 2. 샷건
    + 샷건 corutine이 끝나면 bool true , true가 되면 ChangeSkill()
  + 3.라이플
    + 라이플 corutine이 끝나면 bool true, true가 되면 ChangeSkill()

4.적
  + Enemy DB 생성 (scriptable object)
  + 4-0. FSM 으로 구현
    + idle / tracking / attack / get damage / die 상태가 존재
    + idle : pool 안에 들어가 있을 때의 상태
    + tracking : 범위 안에 들어오면 플레이어를 tracking
    + attack : sight안에 들어오면 공격시작
    + getDamage : 피격받는 애니메이션 실행 , hp 깎기
    + die : 어느 상태에서든지 hp가 0이하이면 die 상태
      
  + 4-1. Enemy Manager 스크립트
    + Enemy DB List
    + Enemy Prefab List 
      + 각 몬스터 프리팹은 Enemy 스크립트를 보유하고 있음

  + 4-2. EnemyPooling
    + EnemyManager에 접근해서 index에 해당하는 Prefab을 가져옴
      + Enemy의 DB에 해당하는 EnemyDB를 넣어줌 
    + Enemy get(); 구현완
    + Enemy return(); 아직 미구현

  + 4-3. EnemyFSM 
    + 하위 Enemy의 부모 클래스
    + FSM을 초기화하는 메서드

  + 4-4. Enemy 스크립트 : EnemyFSM
    + EnemyDB 변수를 가지고있음, pooling에서 생성할 때 EnemyDB할당
    + start에서 FSM 초기화
    + onEnable 초기에 시작될 때 작동 안되도록 bool 조건 걸어줌
      + pooling에서 나올 떄 ( onEnable 될 때)
        + 1. 상태를 Tracking으로 변화
        + 2. FSM스크립트의 Run()을 매프레임 실행하는 코루틴 start 
    + disEnable
        + 아직 미완     

  + 4-5. 총알과 충돌
    + PlayerManager(싱글톤)에서 가지고 있는 Bullet스크립트의 BulletDB의 getDamage 를 return 하는 함수 작성
    + Enemy 스크립트에서 위의 함수에 접근함.

5. DungeonManager ( spawn 담당 )
  + startDungeon() 메서드
    + GameManager에 있는 일정시간마다 enemy 생성,  
    + EnemyPooling에 접근 / 랜덤으로 pool에서 get()
      
999. GameNamager
  + 1. PlayerManager

999. Uimanager 
  + 1. 던전 입장 버튼
    + 온클릭 AddListener 사용
      + 클릭시 GameManager의 startDungeon 메서드 실행
   
