using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _storeSlot;
    [SerializeField]
    private Transform _scrollViewContent;
    [SerializeField]
    List<StoreSlot> _storeList;

    public void Start()
    {
        InitStore();
    }

    private void InitStore()
    {
        for (int i = 0; i < 5; i++) 
        { 
            // 오브젝트 생성, 위치 설정
            GameObject _obj = Instantiate(_storeSlot);
            _obj.transform.parent = _scrollViewContent;
            _obj.transform.localPosition = Vector3.zero;

            if(_obj.GetComponent<StoreSlot>() == null)
                _obj.AddComponent<StoreSlot>();

            StoreSlot _objSlot = _obj.GetComponent<StoreSlot>();
            _storeList.Add(_objSlot);
        }

       


    }

}
