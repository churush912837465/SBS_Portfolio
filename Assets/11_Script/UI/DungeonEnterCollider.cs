using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonEnterCollider : MonoBehaviour
{
    [SerializeField]
    GameObject _popUp;
    [SerializeField]
    Button _enterButton;
    [SerializeField]
    Button _backButton;

    private void OnTriggerEnter(Collider other)
    {
        _popUp.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        _popUp.SetActive(false);
    }

    private void Start()
    {
        _enterButton.onClick.AddListener(enterDungeon);
        _backButton.onClick.AddListener(backDungeon);
    }

    private void enterDungeon() 
    {
        GameManager.instance.DungeonEnter();
        _popUp.SetActive(false);
    }
    private void backDungeon() 
    {
        _popUp.SetActive(false);
    }

}
