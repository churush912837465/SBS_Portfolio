using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;                   // 닷트윈 using
using UnityEngine.UI;     
using UnityEngine.SceneManagement;   // 씬 이동

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
    CanvasGroup _canvasGroup;   // Lobby Ui의 투명도 조절 위한
    [SerializeField]
    Button _enterButtom;        // 게임 enter 버튼

    [Header("first UI")]
    [SerializeField]
    Canvas _firstCanvas;               // 맨 처음 검정 -> 투명 이미지
    [SerializeField]
    CanvasGroup _firstcanvvasGroup;   // Lobby Ui의 투명도 조절 위한

    bool flag = true;

    void Start()
    {
        _firstcanvvasGroup = _firstCanvas.GetComponent<CanvasGroup>();
        _firstcanvvasGroup.DOFade(0f, 2f);

        _enterButtom.onClick.AddListener(EnterGameScene);

        _canvasGroup = _canvas.GetComponent<CanvasGroup>();
        _animator = gameObject.GetComponent<Animator>();
        _canvasGroup.alpha = 0f;        // 초기에는 투명하게

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

        _canvasGroup.DOFade(1f,3f);                 // 캔버스그룹의 alpha값을 1f로 , 2f초 동안 실행
        _animator.SetBool(_playerWaitAni , true);
    }

    void EnterGameScene() 
    {
        SceneManager.LoadScene("02_GameScene");
    }

}
