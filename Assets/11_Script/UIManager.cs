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

    [Header("Player Info Ui")]
    [SerializeField]
    Canvas _playerInfoCanvas;
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

    public void Awake()
    {
        instance = this;    // �̱���

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

        UpdatePlayerStateUI();          // Ui Manager�� player ui infoâ ������Ʈ
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
