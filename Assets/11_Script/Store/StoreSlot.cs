using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class StoreSlot : MonoBehaviour , IPointerClickHandler
{
    // 상점 slot 프리팹 스크립트
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

    // Slot을 클릭 했을 때
    public void OnPointerClick(PointerEventData eventData)
    {
        // storeManger에 idx 넘겨주기
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
