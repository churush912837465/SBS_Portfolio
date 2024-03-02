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
    public PlayerInfoUi _playerInfoUi;

    [SerializeField]
    public Item[] _itemList;

    [SerializeField]
    private int _capacity;

    private void Start()
    {
        _capacity = _inventoryUI.HoriSlotCnt * _inventoryUI.VertiSlotCnt;
        _itemList = new Item[_capacity];
    }

    // 1. �������� �Ծ��� ��
    public void GetAddItem(Item v_getItem , int v_amt = 1)
    {
        // ����ó��
        if (v_getItem == null)
            return;

        // �� �� �ִ� ������
        if (v_getItem is Countable cl)
        {
            int _findIdx = SearchSameItem(v_getItem);

            // �������� �κ� �ȿ� ������? 
            if (_findIdx == -1)
            {
                int _nextIdx = SearchEmptyItemIdx();
                _itemList[_nextIdx] = v_getItem;        // ������ �ֱ�

                UpdateSlot(_nextIdx);
            }

            // �κ��� ������
            else
            {
                Item _slotItem = _itemList[_findIdx];
                Countable _slotCnt = _slotItem as Countable;

                int _max = _slotCnt.countableData.MaxAmount;
                int _sum = cl.countableData.Amount + _slotCnt.countableData.Amount;

                // sum�� max ���� ������
                if (_sum < _max)
                {
                    _slotCnt.AccessAndSetAmount(_sum);
                }
                else
                {
                    _slotCnt.AccessAndSetAmount(_max);

                    // ���� ����
                    int _nextIdx = SearchEmptyItemIdx();

                    Item _newItem = v_getItem.CreateItem();
                    _itemList[_nextIdx] = _newItem;

                    UpdateSlot(_nextIdx);
                }

                UpdateSlot(_findIdx);
            }
        }
        // �� ���� ��� (equipment ������) �̸�?
        else
        {
            // ���� ����
            int _nextIdx = SearchEmptyItemIdx();
            _itemList[_nextIdx] = v_getItem;

            UpdateSlot(_nextIdx);
        }

    }

    // 2. ������ ���
    public void GetUseItem(int v_idx) 
    {
        // _itemList�� v_idx�� �˻�
        if (_itemList[v_idx] == null)
            return;

        // �������� Countable �������̸�? 
        if (_itemList[v_idx] is Countable cl)
        {
            // ������ ���� -1
            // Countable�� ItemUse() ���
            int _currAmt = cl.countableData.Amount - 1;

            cl.AccessAndSetAmount(_currAmt);
            cl.ItemUse();

            // �κ��丮 Ui�� amount Text �ٱ���
            _inventoryUI.SetAmount(v_idx, cl.countableData.Amount);

            // ������ ���� -1 �ߴµ� 0 �����̸�?
            if (cl.countableData.Amount < 1)
            {
                // �� ������ ��������
                _inventoryUI.RemoveIcon(v_idx);
                _inventoryUI.RemoveText(v_idx);
            }
        }

        // �������� ��� �������̸�?
        else 
        {
            // inventory ���� remove ������, text �� 
            _inventoryUI.RemoveIcon(v_idx);
            _inventoryUI.RemoveText(v_idx);

            // playerStateâ���� �� �������� ȹ�� �� �� �ֵ���
            // playerInfoUi ��ũ��Ʈ�� ����
            _playerInfoUi.SetPlayerEquipOfAcc(_itemList[v_idx]);

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
        // index�� �ش��ϴ� item ��
        Item _updateItem = _itemList[v_idx];

        if (_updateItem != null) 
        {
            // ��������Ʈ �ٲٱ�
            _inventoryUI.SetItemIcon(v_idx, _updateItem.itemData.Icon);

            // �� �� ������
            if (_updateItem is Countable cl)
            {
                // ���� update
                _inventoryUI.SetAmount(v_idx , cl.countableData.Amount);
            }
            // �� �� ������
            else 
            {
                // ���� text �����
                _inventoryUI.RemoveText(v_idx);
            }
        }
    }

}
