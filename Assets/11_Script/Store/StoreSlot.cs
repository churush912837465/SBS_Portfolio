using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class StoreSlot : MonoBehaviour , IPointerClickHandler
{
    // ���� slot ������ ��ũ��Ʈ
    [SerializeField]
    private int _slotIdx;
    [SerializeField]
    private Image _sp;
    [SerializeField]
    private TextMeshProUGUI _itemName;
    [SerializeField]
    private TextMeshProUGUI _tooltip;
    [SerializeField]
    private TextMeshProUGUI _price;

    // Slot�� Ŭ�� ���� ��
    public void OnPointerClick(PointerEventData eventData)
    {
        // storeManger�� idx �Ѱ��ֱ�
        GameManager.instance.storeManager.CheckPlayerGoods(_slotIdx);
    }

    public void SetStoreSlot(int v_idx, Sprite v_sp, string v_name, string v_tool, int v_pri) 
    {
        _slotIdx = v_idx;
        _sp.sprite = v_sp;
        _itemName.text = v_name;
        _tooltip.text = v_tool;
        _price.text = v_pri.ToString();

    }
}
