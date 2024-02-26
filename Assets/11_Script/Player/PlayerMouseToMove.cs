using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseToMove : MonoBehaviour
{
    // 카메라 Controller가 움직임
    // 회전은 Player가 함

    [SerializeField]
    Camera camera;
    [SerializeField]
    GameObject player;
    
    private float _speed;

    private Vector3 _destPos;       // 마우스 클릭한 위치
    private Vector3 _destDir;       // 움직이는 위치
    private Quaternion _destRot;    // 회전값

    private bool _canMove;

    void Start()
    {
        _canMove = false;
        _speed = GameManager.instance.playerManager.ReturnMoveSpeed();
    }

    void Update()
    {

        // player에서 스킬 쓸 때 못움직이게
        if (GameManager.instance.playerManager.CanMove)
        { 
            PointToMouse();
            MoveToPoint();
        }
    }

    void PointToMouse() 
    {
        // 우클릭 하면
        if (Input.GetMouseButtonDown(1)) 
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            // ray에 hit(collider를 hit 함) 된게 있으면 true 반환
            if (Physics.Raycast(ray, out rayHit))
            {
                float _dx = rayHit.point.x;                     // ray가 찍은 x
                float _dy = gameObject.transform.position.y;    // 높낮이는 안 바뀜
                float _dz = rayHit.point.z;                     // ray가 찍은 y
                
                _destPos = new Vector3( _dx , _dy , _dz);       // 목표 위치
                _destDir = _destPos - transform.position;       // 방향 벡터
                _destRot = Quaternion.LookRotation(_destDir);   // 방향 벡터의 회전
                                                                // LookRotation : 지정된 방향으로 회전
                _canMove = true;
            }
        }
    }

    void MoveToPoint() 
    {
        // 플레이어 run Ani 실행
        GameManager.instance.playerManager.PlayerIsMoveAndPlayerAni(_canMove);

        if (!_canMove)
            return;

        // playerController 움직임 (정규화 해서 일정하게)
        gameObject.transform.position += _destDir.normalized * Time.deltaTime * _speed;
        // player 회전
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation , _destRot , 0.5f);

        // Vector Magnitude
        // : 두 벡터가 있을 때, 두 벡터의 차를 구한 후 해당 벡터의 길이를 구하면 두 벡터간의 거리를 구할 수 있음
        // 나랑 목표지점이랑 거리가 0.05f 이상이면 움직임 true
        // `` 이하이면 false
        _canMove = (player.transform.position - _destPos).magnitude > 1f;

    }
}
