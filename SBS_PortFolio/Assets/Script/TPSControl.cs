using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCameraControl : MonoBehaviour
{
    [SerializeField]
    Transform trsPlayer;

    [SerializeField]
    Transform trsCameraArm;   
    
    // 카메라가 담긴 상위 Transform
    // 마우스 따라 회전 할 때 cameraArm이 회전함 (플레이어가 회전 x)

    void Start()
    {
    }

    void Update()
    {
        limitMove();
    }

    void limitMove() 
    {
        // playermanager에서 움직일 수 있는 상태가 아니면 (canMove 가 false이면)
        if (!GameManager.instance.playerManager.getCanMove)
            return;
        if (GameManager.instance == null)
        {
            Debug.Log("게임메니져없음");
            return;
        }
        if (GameManager.instance.playerManager == null) 
        {
            Debug.Log("플레이어 메니져 없음");
            return;
        }

        // canMove가 true이면
        mouseToCamera();
        movePlayer();
    }

    private void movePlayer() 
    {
        float inputx = Input.GetAxis("Horizontal");
        float inputy = Input.GetAxis("Vertical");
        Vector2 moveInput = new Vector2 (inputx, inputy);

        bool isMove = (moveInput.magnitude != 0 );              // vector의 길이 반환, 움직이고 있을 때
        // 걷는 애니메이션 실행
        // ani.SetBool("애니메이션이름", isMove);

        if (isMove) 
        {
            Vector3 lookForwardVec = new Vector3(trsCameraArm.forward.x, 0, trsCameraArm.forward.z);  // 카메라가 바라보고 있는 정면
            Vector3 lookRightVec = new Vector3(trsCameraArm.right.x, 0, trsCameraArm.right.z);        // 카메라가 바라보고 있는 좌우
            Vector3 moveDir = lookRightVec * moveInput.x + lookForwardVec * moveInput.y;              // 

            trsPlayer.forward = moveDir;
            transform.position += moveDir * Time.deltaTime * GameManager.instance.playerManager.getfMoveSpeed;    // 플레이어 posi 이동 (움직임) (x 이동속도)
            
        }

        Debug.DrawRay(trsCameraArm.position , new Vector3(trsCameraArm.forward.x , 0 , trsCameraArm.forward.z).normalized , Color.red);
        // camera 의 ray의 y 좌표를 0으로 해서 땅이랑 수평으로 되게
        // normalized : ray의 길이를 1로 (?)
    }

    private void mouseToCamera()    // 마우스 움직임에 따라 카메라 움직임
    {
        float mouseX = Input.GetAxis("Mouse X");                // 마우스가 x축으로 얼마나 움직였는지
        float mouseY = Input.GetAxis("Mouse Y");                // 마우스가 y축으로 얼마나 움직였는지

        Vector2 mouseDelta = new Vector2(mouseX, mouseY);       // 마우스가 움직인 정도
        Vector3 cameraAngle = trsCameraArm.rotation.eulerAngles;   // 카메라 Arm의 회전

        // 카메라 회전 제한
        float limitX = cameraAngle.x - mouseY;                  // arm 회전 - 마우스 회전 -> 얼마나 회전했는지
        if (limitX < 180f)                                      // 만약 180밑으로 회전했으면
            limitX = Mathf.Clamp(limitX, -1f, 45f);             // 0도 ~ 70도 로 제한
        else                                                    // 만약 180보다 크게 회전했으면
            limitX = Mathf.Clamp(limitX, 320f, 361f);           // 290도 ~ 360도로 제한

        trsCameraArm.rotation = Quaternion.Euler(limitX, cameraAngle.y + mouseX, cameraAngle.z);
        // cameraArm의 x축 회전 -> 카메라가 위아래로 움직임 (y)
        // cameraArm의 y축 회전 -> 카메라가 좌우로 움직임 (x)
        

    }

}
