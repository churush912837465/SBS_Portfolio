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
    private List<ItemSlot> _slotUiList;         // slot에 담겨있는 slot 스크립트

    #region 인벤토리 동적 생성
    [Header("inventory basic")]
    private int _horiSlotCnt = 6;               // 슬롯 가로 갯수
    private int _vertSlotCnt = 8;               // 슬롯 세로 갯수
    private float _slotSize = 70f;              // 각 슬롯 크기
    private float _slotMargin = 8.5f;           // slot 사이끼리 상하좌우 여백

    [Header("inventory Component")]
    [SerializeField]
    private RectTransform _slotRect;            // 슬롯의 rect
    [SerializeField]
    private GameObject _slotPrefab;             // 슬롯 프리팹
    [SerializeField]
    private Transform _inventoryBody;           // slot이 위치할 상위 오브젝트

    public int HoriSlotCnt { get => _horiSlotCnt; }
    public int VertiSlotCnt { get => _vertSlotCnt; }

    #endregion

    [SerializeField]
    GraphicRaycaster _graphicRay;               // 그래픽에서 ray를 쏠 수 있는
    [SerializeField]
    PointerEventData _pointEvent;               // 이벤트 발생 시 담아놓을 수 있는 (컨테이너 같은)
    [SerializeField]
    EventSystem _eventSystem;                   // 유니티에서 제공하는 기본 이벤트 시스템
    [SerializeField]
    List<RaycastResult> _rayResult = new List<RaycastResult>();             // 이벤트 발생 시 이벤트를 담아놓는

    #region 인벤토리 drag & drop
    private ItemSlot _beginItemSlot;            // 드래그 하는 slot
    private Transform _beginItemTransform;      // 드래그 하는 slot의 위치
    private Vector3 _beginDragPosi;             // 드래그 하는 위치
    private Vector3 _beginCursorPosi;           // 드래그 하는 마우스커서의 위치

    private int _beginSlotIndex;                // 드래그 하는 slot의 index
    #endregion

    #region
    private ItemSlot _useSlot;      // 사용할 아이템 슬롯
    #endregion


    private void Start()
    {
        // 부모 (cavas 가 가진 컴포넌트 가져오기)
        _graphicRay = transform.GetComponentInParent<GraphicRaycaster>();

        InitSlot();
    }

    private void Update()
    {
        
        _pointEvent = new PointerEventData(_eventSystem);
        _pointEvent.position = Input.mousePosition;             // mouse 위치를 이벤트로 발생시킴 

        UserInventoryItem();      // 아이템 사용

        // 아이템 drag & drop
        /*
        OndragBegin();
        Ondrag();
        OnDragEnd();
        */
    }

    private T RayCastAndReturnValue<T>() where T : Component    // T가 Component 일때만 , return null을 할수잇음
    {
        // graphic ray를 쏘는건 이 함수에서
        _rayResult.Clear();     // List 초기화

        _graphicRay.Raycast(_pointEvent, _rayResult);
        // 그래픽raycast에서 ray를 쏜 결과 (마우스 point)가 _PointEvent , 이 이벤트를 _rayResult 배열에 담음
        // GraphicRaycaster.Raycase( PointerEventData , RaycastResult 타입 List )

        if (_rayResult.Count == 0)
            return null;

        for (int i = 0; i < _rayResult.Count; i++)
        {
            if (_rayResult[i].gameObject.GetComponent<T>() != null)
                return _rayResult[i].gameObject.GetComponent<T>();
        }

        return null;
    }

    #region 인벤토리 drag & drop
    /* 아이템 drag & drop
    private void OndragBegin() 
    {
        // 마우스(0)를 누르면
        if (Input.GetMouseButtonDown(0)) 
        {
            // ray로 ItemSlot 검사 , ItemSlot을 return
            _beginItemSlot = RayCastAndReturnValue<ItemSlot>();

            if (_beginItemSlot == null)
                return;

            // ray로 찾은 slot이 있고, 그 slot에 아이템이 있으면
            if (_beginItemSlot != null && _beginItemSlot.HasItem()) 
            {
                // 시작 slot 위치 -> 캔버스 전체 에서의 position을 저장
                _beginItemTransform = _beginItemSlot.IconRect.transform;

                // 시작 위치
                _beginDragPosi = _beginItemTransform.position;
                // 시작 커서 위치 -> 현재 클릭 위치
                _beginCursorPosi = Input.mousePosition;

                // 드래그 할 땐 그 Slot이 가장 위에 있어야 함
                _beginSlotIndex = _beginItemSlot.transform.GetSiblingIndex();   // 현재 몇번째 index에 있는지 가져오기
                _beginItemSlot.transform.SetAsLastSibling();                    // 가장 마지막 순서로 (가장 위에 보이게)
            }

        }
    }
    private void Ondrag()
    {
        // slot이 없을 때 (예외처리)
        if (_beginItemSlot == null)
            return;

        // 마우스(0)를 누른 상태면
        if (Input.GetMouseButton(0))
        {
            // slot의 위치 
            // = 시작 위치 + (지금 마우스 위치 - 시작 마우스 위치)
            _beginItemTransform.position
                = _beginDragPosi + (Input.mousePosition - _beginCursorPosi);
        }
    }

    private void OnDragEnd()
    {
        // slot이 없을 때 (예외처리)
        if (_beginItemSlot == null)
            return;

        // 마우스(0)를 떼면
        if (Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }
    }

    private void EndDrag()
    {
        _beginItemSlot.transform.SetSiblingIndex(_beginSlotIndex);
        // 드래그 끝에 있는 _slot 가져오기
        ItemSlot _endDragSlot = RayCastAndReturnValue<ItemSlot>();

        Debug.Log(_endDragSlot);
        // + 인벤토리 밖으로 드래그 하면? -> 버리는거 
        // 0. 슬롯이 없다면?
        if (_endDragSlot == null)
        {
            _beginItemTransform.position = _beginItemSlot.SlotRect.transform.position;
        }
        // 슬롯이 있다면?
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


    #region 인벤토리 동적 생성

    private void InitSlot() 
    {
        // 슬롯 사이즈 결정
        _slotRect = _slotPrefab.GetComponent<RectTransform>();
        _slotRect.sizeDelta = new Vector2(_slotSize, _slotSize);    // sizeDelta로 사이즈 설정

        // slot에 inventorySlot 컴포넌트 검사
        ItemSlot _itemSlot = _slotPrefab.GetComponent<ItemSlot>();
        if (_itemSlot == null) 
        {
            _slotPrefab.AddComponent<ItemSlot>();
        }

        // 슬롯 생성
        Vector2 _beginPos = new Vector2(_slotMargin, -_slotMargin);
        Vector2 _currPos = _beginPos;

        _slotUiList = new List<ItemSlot>();

        for (int i = 0; i < _vertSlotCnt; i++)
        {
            for (int j = 0; j < _horiSlotCnt; j++)
            {
                int _slotIdx = (_horiSlotCnt * i) + j;
                RectTransform _slotCI = CloneSlot();

                _slotCI.pivot = new Vector2(0, 1f);     // pivot을 왼쪽 위로 설정
                _slotCI.anchoredPosition = _currPos;    // 위치 설정 (로컬 위치랑 머가다른거지)
                _slotCI.gameObject.name = $"Item Slot [{_slotIdx}]";

                ItemSlot _slot = _slotCI.GetComponent<ItemSlot>();
                _slotUiList.Add(_slot);                 // slot 스크립트를 List에 담아놓고 관리
                _slot.SlotIndex = _slotIdx;             // 인덱스 설정
                _slot.RemoveAmountText();               // 처음엔 텍스트 없음

                // 다음 x 위치
                // 내 현재 위치 + 내 사이즈 + slot 사이의 여백
                _currPos.x += (_slotSize + _slotMargin);
            }

            // 다음 y 위치
            _currPos.x = _beginPos.x;                   // x 위치는 원래대로   
            _currPos.y -= (_slotSize + _slotMargin);    // y는 작아짐 (인스펙터 창에서 직접 해보셈)
        }
        
        
    }

    private RectTransform CloneSlot()
    {
        GameObject _slot = Instantiate(_slotPrefab);
        RectTransform _slotRT = _slot.GetComponent<RectTransform>();
        _slotRT.SetParent(_inventoryBody);      // 상위 오브젝트로 위치 설정

        return _slotRT;
    }



    #endregion


    #region 아이템 사용

    public void UserInventoryItem() 
    {
        // 우클릭을 하면?
        if (Input.GetMouseButtonDown(1)) 
        {
            // ray로 아이템 검사
            _useSlot = RayCastAndReturnValue<ItemSlot>();

            // null 이 아니면?
            if (_useSlot == null) 
                return;

            if (_useSlot.HasItem())     // 그 슬롯이 아이템이 있으면?
            {
                // slot index를 Inventory에 넘기기
                GameManager.instance.inventory.GetUseItem(_useSlot.SlotIndex);
            }
        }

    }

    #endregion


    #region Inventory 스크립트에서 사용

    public void SetItemIcon(int v_idx, Sprite v_icon)
    {
        if (CheckIdx(v_idx)) 
        {
            // slot의 sprite를 update
            _slotUiList[v_idx].SetIcon(v_icon);
        }
    }
    public void SetAmount(int v_idx , int v_amount) 
    {
        if (CheckIdx(v_idx)) 
        {
            // slot의 text를 update
            _slotUiList[v_idx].ChangeAmountText(v_amount);
        }
    }
    public void RemoveText(int v_idx)
    {
        // slot의 text를 remove
        _slotUiList[v_idx].RemoveAmountText();
    }

    public void RemoveIcon(int v_idx) 
    {
        // slot의 Icont을 remove
        _slotUiList[v_idx].RemoveIcon();
    }

    public bool CheckIdx(int v_idx)
    {
        // 인덱스가 인벤토리 내에 존재 할 때만
        if (v_idx >= 0 && v_idx < _horiSlotCnt * _vertSlotCnt)
        {
            return true;
        }
        else
            return false;
    }



    #endregion
}

