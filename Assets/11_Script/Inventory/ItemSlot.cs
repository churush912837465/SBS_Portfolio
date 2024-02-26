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
    private int _slotIndex;     // �ε���
    [SerializeField]
    private Image _iconImage;   // ������ �̹���
    [SerializeField]
    private TextMeshProUGUI _amountText;    // ���� text
    [SerializeField]
    private Image _hilightImage;            // ���̶���Ʈ �̹���

    [SerializeField]
    GameObject _itemIconSlot;               // slot ������Ʈ (Slot �� itemIconSlot)

    [SerializeField]
    RectTransform _slotRect;                // �� slot rect
    [SerializeField]
    RectTransform _iconRect;                // slot���� �̹��� rec

    // ������Ƽ
    public Image IconImage { get => _iconImage; set { _iconImage = value; } }
    public int SlotIndex { get => _slotIndex; set { _slotIndex = value; } }
    public RectTransform SlotRect { get => _slotRect; }
    public RectTransform IconRect { get => _iconRect; }

    // �̹����� ������ true, ������ false
    public bool HasItem() { return _iconImage.sprite != null; }

    private void Start()
    {
        _slotRect = GetComponent<RectTransform>();
        _iconRect = _itemIconSlot.GetComponent<RectTransform>();
    }

    public void SwapItem(ItemSlot v_item)
    {
        // ���� item���� ������ ��ȯ�ؾ���
        Sprite _mySprite = _iconImage.sprite;

        // ��� slot�� �������� �ִٸ�?
        if (v_item.HasItem())
        {
            // �̹��� set
            SetIcon(v_item.IconImage.sprite);
        }
        // ��� slot�� �������� ���ٸ�?
        else if (!v_item.HasItem())
        {
            // �̹��� ���ֱ�
            RemoveIcon();
        }

        // ��� slot���� �� �������� set �������
        v_item.SetIcon(_mySprite);
    }

    public void SetIcon(Sprite v_sp)
    {
        _iconImage.sprite = v_sp;
        _iconImage.gameObject.transform.position = _slotRect.position;
    }

    public void RemoveIcon()
    {
        _iconImage.sprite = null;               // ������ ���ֱ�
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
