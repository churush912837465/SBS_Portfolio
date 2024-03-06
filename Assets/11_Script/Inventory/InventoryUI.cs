using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryUI : MonoBehaviour
{
    private List<ItemSlot> _slotUiList;         // slot�� ����ִ� slot ��ũ��Ʈ

    #region �κ��丮 ���� ����
    [Header("inventory basic")]
    private int _horiSlotCnt = 6;               // ���� ���� ����
    private int _vertSlotCnt = 8;               // ���� ���� ����
    private float _slotSize = 70f;              // �� ���� ũ��
    private float _slotMargin = 8.5f;           // slot ���̳��� �����¿� ����

    [Header("inventory Component")]
    [SerializeField]
    private RectTransform _slotRect;            // ������ rect
    [SerializeField]
    private GameObject _slotPrefab;             // ���� ������
    [SerializeField]
    private Transform _inventoryBody;           // slot�� ��ġ�� ���� ������Ʈ

    public int HoriSlotCnt { get => _horiSlotCnt; }
    public int VertiSlotCnt { get => _vertSlotCnt; }

    #endregion

    [SerializeField]
    GraphicRaycaster _graphicRay;               // �׷��ȿ��� ray�� �� �� �ִ�
    [SerializeField]
    PointerEventData _pointEvent;               // �̺�Ʈ �߻� �� ��Ƴ��� �� �ִ� (�����̳� ����)
    [SerializeField]
    EventSystem _eventSystem;                   // ����Ƽ���� �����ϴ� �⺻ �̺�Ʈ �ý���
    [SerializeField]
    List<RaycastResult> _rayResult = new List<RaycastResult>();             // �̺�Ʈ �߻� �� �̺�Ʈ�� ��Ƴ���

    #region �κ��丮 drag & drop
    private ItemSlot _beginItemSlot;            // �巡�� �ϴ� slot
    private Transform _beginItemTransform;      // �巡�� �ϴ� slot�� ��ġ
    private Vector3 _beginDragPosi;             // �巡�� �ϴ� ��ġ
    private Vector3 _beginCursorPosi;           // �巡�� �ϴ� ���콺Ŀ���� ��ġ

    private int _beginSlotIndex;                // �巡�� �ϴ� slot�� index
    #endregion

    #region
    private ItemSlot _useSlot;      // ����� ������ ����
    #endregion


    private void Start()
    {
        // �θ� (cavas �� ���� ������Ʈ ��������)
        _graphicRay = transform.GetComponentInParent<GraphicRaycaster>();

        InitSlot();
    }

    private void Update()
    {
        
        _pointEvent = new PointerEventData(_eventSystem);
        _pointEvent.position = Input.mousePosition;             // mouse ��ġ�� �̺�Ʈ�� �߻���Ŵ 

        UserInventoryItem();      // ������ ���

        // ������ drag & drop
        /*
        OndragBegin();
        Ondrag();
        OnDragEnd();
        */
    }

    private T RayCastAndReturnValue<T>() where T : Component    // T�� Component �϶��� , return null�� �Ҽ�����
    {
        // graphic ray�� ��°� �� �Լ�����
        _rayResult.Clear();     // List �ʱ�ȭ

        _graphicRay.Raycast(_pointEvent, _rayResult);
        // �׷���raycast���� ray�� �� ��� (���콺 point)�� _PointEvent , �� �̺�Ʈ�� _rayResult �迭�� ����
        // GraphicRaycaster.Raycase( PointerEventData , RaycastResult Ÿ�� List )

        if (_rayResult.Count == 0)
            return null;

        for (int i = 0; i < _rayResult.Count; i++)
        {
            if (_rayResult[i].gameObject.GetComponent<T>() != null)
                return _rayResult[i].gameObject.GetComponent<T>();
        }

        return null;
    }

    #region �κ��丮 drag & drop
    /* ������ drag & drop
    private void OndragBegin() 
    {
        // ���콺(0)�� ������
        if (Input.GetMouseButtonDown(0)) 
        {
            // ray�� ItemSlot �˻� , ItemSlot�� return
            _beginItemSlot = RayCastAndReturnValue<ItemSlot>();

            if (_beginItemSlot == null)
                return;

            // ray�� ã�� slot�� �ְ�, �� slot�� �������� ������
            if (_beginItemSlot != null && _beginItemSlot.HasItem()) 
            {
                // ���� slot ��ġ -> ĵ���� ��ü ������ position�� ����
                _beginItemTransform = _beginItemSlot.IconRect.transform;

                // ���� ��ġ
                _beginDragPosi = _beginItemTransform.position;
                // ���� Ŀ�� ��ġ -> ���� Ŭ�� ��ġ
                _beginCursorPosi = Input.mousePosition;

                // �巡�� �� �� �� Slot�� ���� ���� �־�� ��
                _beginSlotIndex = _beginItemSlot.transform.GetSiblingIndex();   // ���� ���° index�� �ִ��� ��������
                _beginItemSlot.transform.SetAsLastSibling();                    // ���� ������ ������ (���� ���� ���̰�)
            }

        }
    }
    private void Ondrag()
    {
        // slot�� ���� �� (����ó��)
        if (_beginItemSlot == null)
            return;

        // ���콺(0)�� ���� ���¸�
        if (Input.GetMouseButton(0))
        {
            // slot�� ��ġ 
            // = ���� ��ġ + (���� ���콺 ��ġ - ���� ���콺 ��ġ)
            _beginItemTransform.position
                = _beginDragPosi + (Input.mousePosition - _beginCursorPosi);
        }
    }

    private void OnDragEnd()
    {
        // slot�� ���� �� (����ó��)
        if (_beginItemSlot == null)
            return;

        // ���콺(0)�� ����
        if (Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }
    }

    private void EndDrag()
    {
        _beginItemSlot.transform.SetSiblingIndex(_beginSlotIndex);
        // �巡�� ���� �ִ� _slot ��������
        ItemSlot _endDragSlot = RayCastAndReturnValue<ItemSlot>();

        Debug.Log(_endDragSlot);
        // + �κ��丮 ������ �巡�� �ϸ�? -> �����°� 
        // 0. ������ ���ٸ�?
        if (_endDragSlot == null)
        {
            _beginItemTransform.position = _beginItemSlot.SlotRect.transform.position;
        }
        // ������ �ִٸ�?
        else if (_endDragSlot != null)
        { 
            TrySwapItems(_beginItemSlot, _endDragSlot);
        }
        
    }

    private void TrySwapItems(ItemSlot _begin, ItemSlot _end) 
    {
        if (_begin == _end)
            return;

        _begin.SwapItem(_end);
    }
    */
    #endregion


    #region �κ��丮 ���� ����

    private void InitSlot() 
    {
        // ���� ������ ����
        _slotRect = _slotPrefab.GetComponent<RectTransform>();
        _slotRect.sizeDelta = new Vector2(_slotSize, _slotSize);    // sizeDelta�� ������ ����

        // slot�� inventorySlot ������Ʈ �˻�
        ItemSlot _itemSlot = _slotPrefab.GetComponent<ItemSlot>();
        if (_itemSlot == null) 
        {
            _slotPrefab.AddComponent<ItemSlot>();
        }

        // ���� ����
        Vector2 _beginPos = new Vector2(_slotMargin, -_slotMargin);
        Vector2 _currPos = _beginPos;

        _slotUiList = new List<ItemSlot>();

        for (int i = 0; i < _vertSlotCnt; i++)
        {
            for (int j = 0; j < _horiSlotCnt; j++)
            {
                int _slotIdx = (_horiSlotCnt * i) + j;
                RectTransform _slotCI = CloneSlot();

                _slotCI.pivot = new Vector2(0, 1f);     // pivot�� ���� ���� ����
                _slotCI.anchoredPosition = _currPos;    // ��ġ ���� (���� ��ġ�� �Ӱ��ٸ�����)
                _slotCI.gameObject.name = $"Item Slot [{_slotIdx}]";

                ItemSlot _slot = _slotCI.GetComponent<ItemSlot>();
                _slotUiList.Add(_slot);                 // slot ��ũ��Ʈ�� List�� ��Ƴ��� ����
                _slot.SlotIndex = _slotIdx;             // �ε��� ����
                _slot.RemoveAmountText();               // ó���� �ؽ�Ʈ ����

                // ���� x ��ġ
                // �� ���� ��ġ + �� ������ + slot ������ ����
                _currPos.x += (_slotSize + _slotMargin);
            }

            // ���� y ��ġ
            _currPos.x = _beginPos.x;                   // x ��ġ�� �������   
            _currPos.y -= (_slotSize + _slotMargin);    // y�� �۾��� (�ν����� â���� ���� �غ���)
        }
        
        
    }

    private RectTransform CloneSlot()
    {
        GameObject _slot = Instantiate(_slotPrefab);
        RectTransform _slotRT = _slot.GetComponent<RectTransform>();
        _slotRT.SetParent(_inventoryBody);      // ���� ������Ʈ�� ��ġ ����

        return _slotRT;
    }



    #endregion


    #region ������ ���

    public void UserInventoryItem() 
    {
        // ��Ŭ���� �ϸ�?
        if (Input.GetMouseButtonDown(1)) 
        {
            // ray�� ������ �˻�
            _useSlot = RayCastAndReturnValue<ItemSlot>();

            // null �� �ƴϸ�?
            if (_useSlot == null) 
                return;

            if (_useSlot.HasItem())     // �� ������ �������� ������?
            {
                // slot index�� Inventory�� �ѱ��
                GameManager.instance.inventory.GetUseItem(_useSlot.SlotIndex);
            }
        }

    }

    #endregion


    #region Inventory ��ũ��Ʈ���� ���

    public void SetItemIcon(int v_idx, Sprite v_icon)
    {
        if (CheckIdx(v_idx)) 
        {
            // slot�� sprite�� update
            _slotUiList[v_idx].SetIcon(v_icon);
        }
    }
    public void SetAmount(int v_idx , int v_amount) 
    {
        if (CheckIdx(v_idx)) 
        {
            // slot�� text�� update
            _slotUiList[v_idx].ChangeAmountText(v_amount);
        }
    }
    public void RemoveText(int v_idx)
    {
        // slot�� text�� remove
        _slotUiList[v_idx].RemoveAmountText();
    }

    public void RemoveIcon(int v_idx) 
    {
        // slot�� Icont�� remove
        _slotUiList[v_idx].RemoveIcon();
    }

    public bool CheckIdx(int v_idx)
    {
        // �ε����� �κ��丮 ���� ���� �� ����
        if (v_idx >= 0 && v_idx < _horiSlotCnt * _vertSlotCnt)
        {
            return true;
        }
        else
            return false;
    }



    #endregion
}

