using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    private int _slotIndex;     // 인덱스
    [SerializeField]
    private Image _iconImage;   // 아이콘 이미지
    [SerializeField]
    private TextMeshProUGUI _amountText;    // 수량 text
    [SerializeField]
    private Image _hilightImage;            // 하이라이트 이미지

    [SerializeField]
    GameObject _itemIconSlot;               // slot 오브젝트 (Slot 밑 itemIconSlot)

    [SerializeField]
    RectTransform _slotRect;                // 내 slot rect
    [SerializeField]
    RectTransform _iconRect;                // slot안의 이미지 rec

    // 프로퍼티
    public Image IconImage { get => _iconImage; set { _iconImage = value; } }
    public int SlotIndex { get => _slotIndex; set { _slotIndex = value; } }
    public RectTransform SlotRect { get => _slotRect; }
    public RectTransform IconRect { get => _iconRect; }

    // 이미지가 있으면 true, 없으면 false
    public bool HasItem() { return _iconImage.sprite != null; }

    private void Start()
    {
        _slotRect = GetComponent<RectTransform>();
        _iconRect = _itemIconSlot.GetComponent<RectTransform>();
    }

    public void SwapItem(ItemSlot v_item)
    {
        // 들어온 item과의 정보를 교환해야함
        Sprite _mySprite = _iconImage.sprite;

        // 상대 slot에 아이템이 있다면?
        if (v_item.HasItem())
        {
            // 이미지 set
            SetIcon(v_item.IconImage.sprite);
        }
        // 상대 slot에 아이템이 없다면?
        else if (!v_item.HasItem())
        {
            // 이미지 없애기
            RemoveIcon();
        }

        // 상대 slot에는 내 아이콘을 set 해줘야함
        v_item.SetIcon(_mySprite);
    }

    public void SetIcon(Sprite v_sp)
    {
        _iconImage.sprite = v_sp;
        _iconImage.gameObject.transform.position = _slotRect.position;
    }

    public void RemoveIcon()
    {
        _iconImage.sprite = null;               // 아이콘 없애기
    }

    public void ChangeAmountText(int v_amount) 
    {
        _amountText.text = v_amount.ToString();
    }
    public void RemoveAmountText() 
    {
        _amountText.text = "";
    }
}
