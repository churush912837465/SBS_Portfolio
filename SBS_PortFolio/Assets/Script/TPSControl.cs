using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCameraControl : MonoBehaviour
{
    [SerializeField]
    Transform trsPlayer;

    [SerializeField]
    Transform trsCameraArm;   
    
    // ī�޶� ��� ���� Transform
    // ���콺 ���� ȸ�� �� �� cameraArm�� ȸ���� (�÷��̾ ȸ�� x)

    void Start()
    {
    }

    void Update()
    {
        limitMove();
    }

    void limitMove() 
    {
        // playermanager���� ������ �� �ִ� ���°� �ƴϸ� (canMove �� false�̸�)
        if (!GameManager.instance.playerManager.getCanMove)
            return;
        if (GameManager.instance == null)
        {
            Debug.Log("���Ӹ޴�������");
            return;
        }
        if (GameManager.instance.playerManager == null) 
        {
            Debug.Log("�÷��̾� �޴��� ����");
            return;
        }

        // canMove�� true�̸�
        mouseToCamera();
        movePlayer();
    }

    private void movePlayer() 
    {
        float inputx = Input.GetAxis("Horizontal");
        float inputy = Input.GetAxis("Vertical");
        Vector2 moveInput = new Vector2 (inputx, inputy);

        bool isMove = (moveInput.magnitude != 0 );              // vector�� ���� ��ȯ, �����̰� ���� ��
        // �ȴ� �ִϸ��̼� ����
        // ani.SetBool("�ִϸ��̼��̸�", isMove);

        if (isMove) 
        {
            Vector3 lookForwardVec = new Vector3(trsCameraArm.forward.x, 0, trsCameraArm.forward.z);  // ī�޶� �ٶ󺸰� �ִ� ����
            Vector3 lookRightVec = new Vector3(trsCameraArm.right.x, 0, trsCameraArm.right.z);        // ī�޶� �ٶ󺸰� �ִ� �¿�
            Vector3 moveDir = lookRightVec * moveInput.x + lookForwardVec * moveInput.y;              // 

            trsPlayer.forward = moveDir;
            transform.position += moveDir * Time.deltaTime * GameManager.instance.playerManager.getfMoveSpeed;    // �÷��̾� posi �̵� (������) (x �̵��ӵ�)
            
        }

        Debug.DrawRay(trsCameraArm.position , new Vector3(trsCameraArm.forward.x , 0 , trsCameraArm.forward.z).normalized , Color.red);
        // camera �� ray�� y ��ǥ�� 0���� �ؼ� ���̶� �������� �ǰ�
        // normalized : ray�� ���̸� 1�� (?)
    }

    private void mouseToCamera()    // ���콺 �����ӿ� ���� ī�޶� ������
    {
        float mouseX = Input.GetAxis("Mouse X");                // ���콺�� x������ �󸶳� ����������
        float mouseY = Input.GetAxis("Mouse Y");                // ���콺�� y������ �󸶳� ����������

        Vector2 mouseDelta = new Vector2(mouseX, mouseY);       // ���콺�� ������ ����
        Vector3 cameraAngle = trsCameraArm.rotation.eulerAngles;   // ī�޶� Arm�� ȸ��

        // ī�޶� ȸ�� ����
        float limitX = cameraAngle.x - mouseY;                  // arm ȸ�� - ���콺 ȸ�� -> �󸶳� ȸ���ߴ���
        if (limitX < 180f)                                      // ���� 180������ ȸ��������
            limitX = Mathf.Clamp(limitX, -1f, 45f);             // 0�� ~ 70�� �� ����
        else                                                    // ���� 180���� ũ�� ȸ��������
            limitX = Mathf.Clamp(limitX, 320f, 361f);           // 290�� ~ 360���� ����

        trsCameraArm.rotation = Quaternion.Euler(limitX, cameraAngle.y + mouseX, cameraAngle.z);
        // cameraArm�� x�� ȸ�� -> ī�޶� ���Ʒ��� ������ (y)
        // cameraArm�� y�� ȸ�� -> ī�޶� �¿�� ������ (x)
        

    }

}
