using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseToMove : MonoBehaviour
{
    // ī�޶� Controller�� ������
    // ȸ���� Player�� ��

    [SerializeField]
    Camera camera;
    [SerializeField]
    GameObject player;
    
    private float _speed;

    private Vector3 _destPos;       // ���콺 Ŭ���� ��ġ
    private Vector3 _destDir;       // �����̴� ��ġ
    private Quaternion _destRot;    // ȸ����

    private bool _canMove;

    void Start()
    {
        _canMove = false;
        _speed = GameManager.instance.playerManager.ReturnMoveSpeed();
    }

    void Update()
    {

        // player���� ��ų �� �� �������̰�
        if (GameManager.instance.playerManager.CanMove)
        { 
            PointToMouse();
            MoveToPoint();
        }
    }

    void PointToMouse() 
    {
        // ��Ŭ�� �ϸ�
        if (Input.GetMouseButtonDown(1)) 
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            // ray�� hit(collider�� hit ��) �Ȱ� ������ true ��ȯ
            if (Physics.Raycast(ray, out rayHit))
            {
                float _dx = rayHit.point.x;                     // ray�� ���� x
                float _dy = gameObject.transform.position.y;    // �����̴� �� �ٲ�
                float _dz = rayHit.point.z;                     // ray�� ���� y
                
                _destPos = new Vector3( _dx , _dy , _dz);       // ��ǥ ��ġ
                _destDir = _destPos - transform.position;       // ���� ����
                _destRot = Quaternion.LookRotation(_destDir);   // ���� ������ ȸ��
                                                                // LookRotation : ������ �������� ȸ��
                _canMove = true;
            }
        }
    }

    void MoveToPoint() 
    {
        // �÷��̾� run Ani ����
        GameManager.instance.playerManager.PlayerIsMoveAndPlayerAni(_canMove);

        if (!_canMove)
            return;

        // playerController ������ (����ȭ �ؼ� �����ϰ�)
        gameObject.transform.position += _destDir.normalized * Time.deltaTime * _speed;
        // player ȸ��
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation , _destRot , 0.5f);

        // Vector Magnitude
        // : �� ���Ͱ� ���� ��, �� ������ ���� ���� �� �ش� ������ ���̸� ���ϸ� �� ���Ͱ��� �Ÿ��� ���� �� ����
        // ���� ��ǥ�����̶� �Ÿ��� 0.05f �̻��̸� ������ true
        // `` �����̸� false
        _canMove = (player.transform.position - _destPos).magnitude > 1f;

    }
}
