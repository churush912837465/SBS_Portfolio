using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUi : MonoBehaviour
{
    // 캐릭터 장비 칸 4개
    [Header("장비")]
    [SerializeField]
    private GameObject[] _equipObj;
    [SerializeField]
    private Sprite[] _equipImage;
    [SerializeField]
    private PlayerSlot[] _equipSlots;

    // 캐릭터 장신구 칸 4개
    [Header("장신구")]
    [SerializeField]
    private GameObject[] _accessoryObj;
    [SerializeField]
    private Sprite _acceImage;
    [SerializeField]
    private PlayerSlot[] _accSlots;

    private void Start()
    {
        _equipSlots = new PlayerSlot[4];
        _accSlots = new PlayerSlot[4];


        for (int i = 0; i < _equipObj.Length; i++) 
        {
            _equipSlots[i] = _equipObj[i].GetComponent<PlayerSlot>();
            _equipSlots[i].SetSlotImage(_equipImage[i]);
        }

        for (int i = 0; i < _accessoryObj.Length; i++) 
        {
            _accSlots[i] = _accessoryObj[i].GetComponent<PlayerSlot>();
            _accSlots[i].SetSlotImage(_acceImage);
        }
    }

    public void SetPlayerEquipOfAcc(Item v_item) 
    {
        // 장비이면?
        if (v_item is Equipment ep)
        {
            // 장비 slot에 끼우기
            // Slot에 끼울 떄 해당 equip에 저장 되어 있는 index에 접근해서,
            // 그 index에 해당하는 slot에 set해야함



        }
        else if (v_item is Accessory acs)
        {
            // 악세사리slot에 끼우기
            
        }

    }

}
