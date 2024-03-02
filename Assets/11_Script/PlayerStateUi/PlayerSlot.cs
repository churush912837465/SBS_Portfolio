using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlot : MonoBehaviour
{
    [SerializeField]
    private Image _slotImage;
    [SerializeField]
    private Item _playerItem;

    // Item 이 있으면 true, 없으면 flase 반환
    public bool HasItem() { return _playerItem != null;  }

    // 이미지 세팅
    public void SetSlotImage(Sprite v_sp)
    {
        _slotImage.sprite = v_sp;
    }

    // Item 세팅
    public void SetItem(Item v_slotItem) 
    {
        this._playerItem = v_slotItem;
    }

}
