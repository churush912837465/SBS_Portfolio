using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoodsUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _timeText;
    
    [SerializeField]
    TextMeshProUGUI _goldText;
    [SerializeField]
    TextMeshProUGUI _shillingText;
    [SerializeField]
    TextMeshProUGUI _honorText;

    public void Start()
    {
        UPdateGoodsUI(GoodsType.Gold);
        UPdateGoodsUI(GoodsType.Silling);
        UPdateGoodsUI(GoodsType.Honor);
    }

    public void Update()
    {
        _timeText.text = DateTime.Now.ToString(("HH:mm:ss"));
    }

    public void UPdateGoodsUI( GoodsType v_type) 
    {
        string _updateCount = GameManager.instance.goodsManager._goodsList[(int)v_type].GoodsCount.ToString();

        switch (v_type)
        {
            case GoodsType.Gold:
                _goldText.text = _updateCount;
                break;
            case GoodsType.Silling:
                _shillingText.text = _updateCount;
                break;
            case GoodsType.Honor:
                _honorText.text = _updateCount;
                break;
        }
    }   

}
