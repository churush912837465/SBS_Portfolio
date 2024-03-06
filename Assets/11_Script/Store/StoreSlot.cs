using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreSlot : MonoBehaviour
{
    [SerializeField]
    private Sprite _sp;
    [SerializeField]
    private TextMeshProUGUI _tooltip;
    [SerializeField]
    private TextMeshProUGUI _price;

    public void SetStoreSlot(Sprite v_sp, string v_tool, int v_pri) 
    {
        this._sp = v_sp;
        _tooltip.text = v_tool;
        _price.text = v_pri.ToString();

    }
}
