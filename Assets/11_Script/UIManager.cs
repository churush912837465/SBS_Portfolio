using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    private Button dungeonEnterButton;
    [SerializeField]
    private Button getClothesButton;
    [SerializeField]
    private Button getAccessoryButton;

    [Space]
    [Header("Canvas")]
    [SerializeField]
    GameObject _inventoryUI;

    [Header("Player Info Ui")]
    [SerializeField]
    GameObject _playerInfoUI;
    [SerializeField]
    TextMeshProUGUI _textNickName;
    [SerializeField]
    TextMeshProUGUI _textLevel;
    [SerializeField]
    TextMeshProUGUI _textAddHp;
    [SerializeField]
    TextMeshProUGUI _textPhyDefen;
    [SerializeField]
    TextMeshProUGUI _textMasicDefen;
    [SerializeField]
    TextMeshProUGUI _textCounter;

    [Header("Store")]
    [SerializeField]
    GameObject _storeUI;

    public void Awake()
    {
        instance = this;    // �̱���

        // �̺�Ʈ �߰�
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
        _inventoryUI.SetActive(!_inventoryUI.activeSelf);
    }

    // �÷��̾� info â �ѱ�
    private void OnOffPlayerInfoUi() 
    {
        _playerInfoUI.SetActive(!_playerInfoUI.activeSelf);

        UpdatePlayerStateUI();          // Ui Manager�� player ui infoâ ������Ʈ
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

    public void UpdatePlayerStateUI() 
    {
        _textNickName.text      = "����ä";
        _textLevel.text         = " 1 Level";
        _textAddHp.text         = GameManager.instance.playerManager.playerData.AdditionalHp.ToString();
        _textPhyDefen.text      = GameManager.instance.playerManager.playerData.PhyDefencity.ToString();
        _textMasicDefen.text    = GameManager.instance.playerManager.playerData.MasicDefencity.ToString();
        _textCounter.text       = GameManager.instance.playerManager.playerData.Counter.ToString();

    }

}
