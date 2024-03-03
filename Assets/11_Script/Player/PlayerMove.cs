using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [SerializeField]
    Vector3 _moveVec;
    [SerializeField]
    float _moveSpeed;

    private void Start()
    {
        _moveSpeed = GameManager.instance.playerManager.playerData.MoveSpeed;
    }

    private void Update()
    {
        playerMove();
    }

    void playerMove() 
    {
        float _hAxis = Input.GetAxis("Horizontal");
        float _vAxis = Input.GetAxis("Vertical");

        _moveVec = new Vector3(_vAxis , 0 , _hAxis).normalized;

        transform.position += _moveVec * _moveSpeed * Time.deltaTime;

    }

}
