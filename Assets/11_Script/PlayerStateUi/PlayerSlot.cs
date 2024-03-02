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

    // ������Ƽ
    public bool HasItem { get => _hasItem; }    // �������� ������ true

    // �̹��� ����
    public void SetSlotImage(Sprite v_sp)
    {
        _slotImage.sprite = v_sp;
        _hasItem = false;
    }

    // Item ����
    public void ChangeIcon(Sprite v_ItemSprite) 
    {
        this._slotImage.sprite = v_ItemSprite;
        _hasItem = true;
    }

}
