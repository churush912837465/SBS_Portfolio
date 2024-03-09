using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _storeSlot;          // slot 프리팹     
    [SerializeField]
    private Transform _scrollViewContent;   // scroll view 밑의 content 

    [SerializeField]
    List<Item> _storeItem;

    public void Start()
    {
        InitStore();
    }

    private void InitStore()    // store 초기세팅
    {
        _storeItem = new List<Item>
        {
            // 아이템 List에 추가
            GameManager.instance.itemManager.ReturnPortion(PortionType.potion),
            GameManager.instance.itemManager.ReturnPortion(PortionType.highPortion),
            GameManager.instance.itemManager.ReturnPortion(PortionType.spiritsPortin),
            GameManager.instance.itemManager.ReturnBomb(BombType.tornadoBomb),
            GameManager.instance.itemManager.ReturnBomb(BombType.destroyBomb)
        };

        for (int i = 0; i < _storeItem.Count; i++) 
        { 
            // 오브젝트 생성, 위치 설정
            GameObject _obj = Instantiate(_storeSlot);
            _obj.transform.parent = _scrollViewContent;
            _obj.transform.localPosition = Vector3.zero;

            StoreSlot _objSlot = _obj.GetComponent<StoreSlot>();

            Countable cl = _storeItem[i] as Countable;
            // 스프라이트  , 툴팁 , 가격
            _objSlot.SetStoreSlot
            (
                i,
                _storeItem[i].itemData.Icon,
                _storeItem[i].itemData.Name,
                _storeItem[i].itemData.ToolTip,
                cl.countableData.Price
            );
        }
    }

    public void CheckPlayerGoods(int v_idx) 
    {
        Countable cl = _storeItem[v_idx] as Countable;

        if (GoodsManager.instance.CheckGoodsIsValid(GoodsType.Silling, cl.countableData.Price))     // 돈이 충분하면
        {
            PlayerBuyItem(v_idx); 
            GoodsManager.instance.PlayerLostGoods(GoodsType.Silling, cl.countableData.Price);     // 돈 빼기
        }  
    } 

    public void PlayerBuyItem( int v_idx)   // 플레이어가 구매 , 플레이어 인벤토리에 넣기
    {
        switch (v_idx)
        {
            case 0:
                GameManager.instance.itemManager.PlayerGetPortion(PortionType.potion);
                break;
            case 1:
                GameManager.instance.itemManager.PlayerGetPortion(PortionType.highPortion);
                break;
            case 2:
                GameManager.instance.itemManager.PlayerGetPortion(PortionType.spiritsPortin);
                break;
            case 3:
                GameManager.instance.itemManager.PlayerGetBomb(BombType.tornadoBomb);
                break;
            case 4:
                GameManager.instance.itemManager.PlayerGetBomb(BombType.destroyBomb);
                break;
        }
    }
}
