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

    // Item �� ������ true, ������ flase ��ȯ
    public bool HasItem() { return _playerItem != null;  }

    // �̹��� ����
    public void SetSlotImage(Sprite v_sp)
    {
        _slotImage.sprite = v_sp;
    }

    // Item ����
    public void SetItem(Item v_slotItem) 
    {
        this._playerItem = v_slotItem;
    }

}
