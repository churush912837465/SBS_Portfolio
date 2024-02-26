using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro.EditorUtilities;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public InventoryUI _inventoryUI;

    [SerializeField]
    public Item[] _itemList;

    [SerializeField]
    private int _capacity;

    private void Start()
    {
        _capacity = _inventoryUI.HoriSlotCnt * _inventoryUI.VertiSlotCnt;
        _itemList = new Item[_capacity];
    }

    // 1. 아이템을 먹었을 때
    public void GetAddItem(Item v_getItem , int v_amt = 1)
    {
        // 예외처리
        if (v_getItem == null)
            return;

        // 셀 수 있는 아이템
        if (v_getItem is Countable cl)
        {
            int _findIdx = SearchSameItem(v_getItem);

            // 아이템이 인벤 안에 없으면? 
            if (_findIdx == -1)
            {
                int _nextIdx = SearchEmptyItemIdx();
                _itemList[_nextIdx] = v_getItem;        // 아이템 넣기

                UpdateSlot(_nextIdx);
            }

            // 인벤이 있으면
            else
            {
                Item _slotItem = _itemList[_findIdx];
                Countable _slotCnt = _slotItem as Countable;

                int _max = _slotCnt.countableData.MaxAmount;
                int _sum = cl.countableData.Amount + _slotCnt.countableData.Amount;

                // sum이 max 보다 작으면
                if (_sum < _max)
                {
                    _slotCnt.AccessAndAddAmount(_sum);
                }
                else
                {
                    _slotCnt.AccessAndAddAmount(_max);

                    // 새로 생성
                    int _nextIdx = SearchEmptyItemIdx();

                    Item _newItem = v_getItem.CreateItem();
                    _itemList[_nextIdx] = _newItem;

                    UpdateSlot(_nextIdx);
                }

                UpdateSlot(_findIdx);
            }
        }
        // 못 세는 장비 (equipment 아이템) 이면?
        else
        {
            // 새로 생성
            int _nextIdx = SearchEmptyItemIdx();
            _itemList[_nextIdx] = v_getItem;

            UpdateSlot(_nextIdx);
        }

    }

    public int SearchSameItem(Item v_sitem) 
    {
        for(int i = _capacity - 1 ; i  >= 0  ; i--) 
        {
            if (_itemList[i] == null)
                continue;

            if (v_sitem.itemData.Name == _itemList[i].itemData.Name)
                return i;
        }

        return -1;
    }
    public int SearchEmptyItemIdx() 
    {
        for (int i = 0; i < _capacity; i++)
        {
            if (_itemList[i] == null)
                return i;
        }
        return -1;
    }

    private void UpdateSlot(int v_idx) 
    {
        // index에 해당하는 item 을
        Item _updateItem = _itemList[v_idx];

        if (_updateItem != null) 
        {
            // 스프라이트 바꾸기
            _inventoryUI.SetItemIcon(v_idx, _updateItem.itemData.Icon);

            // 셀 수 있으면
            if (_updateItem is Countable cl)
            {
                // 수량 update
                _inventoryUI.SetAmount(v_idx , cl.countableData.Amount);
            }
            // 셀 수 없으면
            else 
            {
                // 수량 text 지우기
                _inventoryUI.RemoveText(v_idx);
            }
        }
    }

}
