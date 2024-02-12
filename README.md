# SBS_Portfolio

1. 플레이어
  + 기본 playerManager 생성 (싱글톤)

1-2. BulletDB(SciptablaObject) 변수
  + 각 상태 (handGun , ShootGun , lifle)가 시작이 될 때(Begin() 함수 )
  + GunSlinerBullet에 접근하여 BulletDB를 가져온 후, Player 스크립트의 Bullet 에 할당

2. 총알 구현
  +2-1. GunSlinerBullet 스크립트
  + ScriptableObject형의 BulletDB 3종류를 List로 가지고 있음
2-1. pooling 구현 (Queue 사용)
  + init에서 BulletBase를 생성
  + Player는 BulletBase를 get 해서 사용

3.스킬
3-1. 구현
  + FSM으로 구현
  + 스킬 3종
  + 1. 핸드건
    오브젝트를 회전, 쿼터니언 값으로 360 * 3바퀴 돌면 Idle로 상태 변화
    총발사 코루틴 사용
  + 2. 샷건
    총발사 코루틴 사용
  + 3.라이플
    총발사 코루틴 사용

  +  HandGun 상태
    + 회전이 360 * 3이상이 되면 ChangeSkill()
  + Shoot 상태
    + 샷건 corutine이 끝나면 bool true , true가 되면 ChangeSkill()
  + lifle 상태
    +라이플 corutine이 끝나면 bool true, true가 되면 ChangeSkill()

4.적
  + Enemy DB 생성 (scriptable object)
  + 0. FSM 으로 구현
    + idle / walk / attack / get damage / die 상태가 존재
    + idle : 플레이어가 던전에 들어오면 상태 시작
    + walk : 플레이어를 tracking
    + attack : sight안에 들어오면 공격시작 (enemy의 enemyDB에 접근헤서 원거리 / 근거리 bool 변수 가져오기)
    + getDamage : 피격받는 애니메이션 실행 , hp 깎기
    + die : 어느 상태에서든지 hp가 0이하이면 die 상태
      
  + 1.총알과 충돌
    + PlayerManager(싱글톤)에서 가지고 있는 Bullet스크립트의 BulletDB의 getDamage 를 return 하는 함수 작성
    + Enemy 스크립트에서 위의 함수에 접근함.
  + 2. enemy 생성
    + 던전에 들어가야 몬스터가 생성 ( 그냥 필드에는 생성 x ) / 로아 카오스 던전
    + enemy Spawner 에서 몬스터 생성 ( pooling 에서 get)
    
5. GameNamager
  + 1. PlayerManager
   
