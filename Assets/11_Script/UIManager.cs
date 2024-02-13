using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button dungeonEnterButton;

    public void Awake()
    {
        dungeonEnterButton.onClick.AddListener(dungeonEnter);
    }
    
    public void dungeonEnter()
    {
        GameManager.instance.DungeonEnter();
    }
}
