using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _storeSlot;          // slot ������     
    [SerializeField]
    private Transform _scrollViewContent;   // scroll view ���� content 

    [SerializeField]
    List<Item> _storeItem;

    public void Start()
    {
        InitStore();
    }

    private void InitStore()    // store �ʱ⼼��
    {
        _storeItem = new List<Item>
        {
            // ������ List�� �߰�
            GameManager.instance.itemManager.ReturnPortion(PortionType.potion),
            GameManager.instance.itemManager.ReturnPortion(PortionType.highPortion),
            GameManager.instance.itemManager.ReturnPortion(PortionType.spiritsPortin),
            GameManager.instance.itemManager.ReturnBomb(BombType.tornadoBomb),
            GameManager.instance.itemManager.ReturnBomb(BombType.destroyBomb)
        };

        for (int i = 0; i < _storeItem.Count; i++) 
        { 
            // ������Ʈ ����, ��ġ ����
            GameObject _obj = Instantiate(_storeSlot);
            _obj.transform.parent = _scrollViewContent;
            _obj.transform.localPosition = Vector3.zero;

            StoreSlot _objSlot = _obj.GetComponent<StoreSlot>();

            Countable cl = _storeItem[i] as Countable;
            // ��������Ʈ  , ���� , ����
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

        if (GoodsManager.instance.CheckGoodsIsValid(GoodsType.Silling, cl.countableData.Price))     // ���� ����ϸ�
        {
            PlayerBuyItem(v_idx); 
            GoodsManager.instance.PlayerLostGoods(GoodsType.Silling, cl.countableData.Price);     // �� ����
        }  
    } 

    public void PlayerBuyItem( int v_idx)   // �÷��̾ ���� , �÷��̾� �κ��丮�� �ֱ�
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
