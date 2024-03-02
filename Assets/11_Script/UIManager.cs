using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button dungeonEnterButton;
    [SerializeField]
    private Button getPortionButton;
    [SerializeField]
    private Button getBombButton;
    [SerializeField]
    private Button getClothesButton;
    [SerializeField]
    private Button getAccessoryButton;

    [Space]
    [Header("Canvas")]
    [SerializeField]
    Canvas _inventoryCanvas;
    [SerializeField]
    Canvas _playerInfoCanvas;

    public void Awake()
    {
        // �̺�Ʈ �߰�
        dungeonEnterButton.onClick.AddListener(dungeonEnter);
        getPortionButton.onClick.AddListener(getPortion);
        getBombButton.onClick.AddListener(getBomb);
        getClothesButton.onClick.AddListener(getClothes);
        getAccessoryButton.onClick.AddListener(getAccessory);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            OnOffInventoryUi();
        if (Input.GetKeyDown(KeyCode.P))
            OnOffPlayerInfoUi();
    }

    // inventory �ѱ�
    private void OnOffInventoryUi() 
    {
        _inventoryCanvas.gameObject.SetActive(!_inventoryCanvas.gameObject.activeSelf);
    }

    private void OnOffPlayerInfoUi() 
    {
        _playerInfoCanvas.gameObject.SetActive(!_playerInfoCanvas.gameObject.activeSelf);
    }

    // ���� Enter
    public void dungeonEnter()
    {
        GameManager.instance.DungeonEnter();
    }

    // Portion ȹ��
    public void getPortion() 
    {
        GameManager.instance.PlayerGetPortion();
    }

    // bomb ȹ��
    public void getBomb() 
    {
        GameManager.instance.PlayerGetBomb();
    }

    // ��� ȹ��
    public void getClothes() 
    {
        GameManager.instance.PlayerGetClothes();
    }

    // �Ǽ��縮 ȹ��
    public void getAccessory() 
    {
        GameManager.instance.PlayerGetAccessory();
    }
}
