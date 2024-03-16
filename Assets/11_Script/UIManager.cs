using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;                   // ��Ʈ�� using

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

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

    [Header("Player Die Panel")]
    [SerializeField]
    GameObject _playerDiePanel;
    [SerializeField]
    Button _returnToVillageButton;
    [SerializeField]
    Button _returnToMainmanuButton;

    //[Header("Player start int village")]
    //[SerializeField]
    //Image _fadePanel;

    public void Awake()
    {
        instance = this;    // �̱���

        // �̺�Ʈ �߰�
        getClothesButton.onClick.AddListener(getClothes);
        getAccessoryButton.onClick.AddListener(getAccessory);
        _returnToVillageButton.onClick.AddListener(PlayerRetunToVillage);
        _returnToMainmanuButton.onClick.AddListener(PlayerReturnToMain);
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
        _textNickName.text = "����ä";
        _textLevel.text = " 1 Level";
        _textAddHp.text = GameManager.instance.playerManager.playerData.AdditionalHp.ToString();
        _textPhyDefen.text = GameManager.instance.playerManager.playerData.PhyDefencity.ToString();
        _textMasicDefen.text = GameManager.instance.playerManager.playerData.MasicDefencity.ToString();
        _textCounter.text = GameManager.instance.playerManager.playerData.Counter.ToString();

    }

    public void OnOffPlayerDiePanel(bool v_f)
    {
        _playerDiePanel.SetActive(v_f);
    }

    public void PlayerRetunToVillage()
    {
        // ������ ���ư���
        OnOffPlayerDiePanel(false);
        GameManager.instance.player.transform.position = new Vector3(105f, 45f, -152f);
        GameManager.instance.playerManager.CanMove = true;

    }

    public void PlayerReturnToMain()
    {
        // ���� ȭ������ ���ư���
        OnOffPlayerDiePanel(false);
        SceneManager.LoadScene("01_Lobby");
    }

    /*
    public void PlayerStartInVillage()
    {
        _fadePanel.DOFade(0f, 2f);                  // fade �ϱ�
    }
    */
}