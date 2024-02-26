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

    public void Awake()
    {
        // �̺�Ʈ �߰�
        dungeonEnterButton.onClick.AddListener(dungeonEnter);
        getPortionButton.onClick.AddListener(getPortion);
        getBombButton.onClick.AddListener(getBomb);
        getClothesButton.onClick.AddListener(getClothes);
        getAccessoryButton.onClick.AddListener(getAccessory);
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
