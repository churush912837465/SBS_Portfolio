using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlot : MonoBehaviour
{
    [SerializeField]
    private Image _slotImage;
    [SerializeField]
    private bool _hasItem;

    // 프로퍼티
    public bool HasItem { get => _hasItem; }    // 아이템이 있으면 true

    // 이미지 세팅
    public void SetSlotImage(Sprite v_sp)
    {
        _slotImage.sprite = v_sp;
        _hasItem = false;
    }

    // Item 세팅
    public void ChangeIcon(Sprite v_ItemSprite) 
    {
        this._slotImage.sprite = v_ItemSprite;
        _hasItem = true;
    }

}
