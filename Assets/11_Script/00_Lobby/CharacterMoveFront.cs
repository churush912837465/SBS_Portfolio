using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;                   // ��Ʈ�� using
using UnityEngine.UI;     
using UnityEngine.SceneManagement;   // �� �̵�

public class CharacterMoveFront : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    Animator _animator;
    [SerializeField]
    string _playerWaitAni = "playerWait";

    [Header("UI")]
    [SerializeField]
    Canvas _canvas;             // Lobby Ui
    [SerializeField]
    CanvasGroup _canvasGroup;   // Lobby Ui�� ���� ���� ����
    [SerializeField]
    Button _enterButtom;        // ���� enter ��ư

    [Header("first UI")]
    [SerializeField]
    Canvas _firstCanvas;               // �� ó�� ���� -> ���� �̹���
    [SerializeField]
    CanvasGroup _firstcanvvasGroup;   // Lobby Ui�� ���� ���� ����

    bool flag = true;

    void Start()
    {
        _firstcanvvasGroup = _firstCanvas.GetComponent<CanvasGroup>();
        _firstcanvvasGroup.DOFade(0f, 2f);

        _enterButtom.onClick.AddListener(EnterGameScene);

        _canvasGroup = _canvas.GetComponent<CanvasGroup>();
        _animator = gameObject.GetComponent<Animator>();
        _canvasGroup.alpha = 0f;        // �ʱ⿡�� �����ϰ�

        Invoke("StopCoru" , 4.7f);
    }

    private void FixedUpdate()
    {
        if(flag)
            this.transform.position += new Vector3(0.15f, 0, 0);
    }


    void StopCoru() 
    {
        flag = false;

        _canvasGroup.DOFade(1f,3f);                 // ĵ�����׷��� alpha���� 1f�� , 2f�� ���� ����
        _animator.SetBool(_playerWaitAni , true);
    }

    void EnterGameScene() 
    {
        SceneManager.LoadScene("02_GameScene");
    }

}
