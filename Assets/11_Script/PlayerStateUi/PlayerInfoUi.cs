using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

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

    [Header("Item List 저장")]
    [SerializeField]
    private Item[] _equiItem;
    [SerializeField]
    private Item[] _accItem;
    

    private void Start()
    {
        _equipSlots = new PlayerSlot[4];
        _accSlots = new PlayerSlot[4];

        _equiItem = new Item[4];
        _accItem = new Item[4];

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

    public void SetPlayerEquiporAcc(Item v_item)
    {

        int _slotIdx = v_item.itemData.Id;
        Equipment _eqp = v_item as Equipment;

        // 장비이면?
        // Slot에 끼울 떄 해당 equip에 저장 되어 있는 index에 접근해서,
        // 그 index에 해당하는 slot에 set해야함
        if (v_item is ClothesEquipment ep)
        {
            // 이미 슬롯에 아이템이 있으면
            if (_equipSlots[_slotIdx].HasItem)
            {
                // inventory에 현재 아이템을 add 하면 될듯?
                GameManager.instance.inventory.GetAddItem(v_item);
                // 끼고 있는 장비의 스탯을 (-) 해줘야함
                SetPlayerState(_eqp, -1);
            }

            // 장비 slot에 끼우기
            _equipSlots[_slotIdx].ChangeIcon(v_item.itemData.Icon);
            _equiItem[_slotIdx] = v_item;                           // 아이템 저장

        }
        // 악세사리 이면?
        else if (v_item is Accessory acs)
        {
            // 이미 슬롯에 아이템이 있으면
            if (_accSlots[_slotIdx].HasItem)
            {
                GameManager.instance.inventory.GetAddItem(v_item);
                SetPlayerState(_eqp, -1);
            }

            // 악세사리slot에 끼우기
            _accSlots[_slotIdx].ChangeIcon(v_item.itemData.Icon);
            _equiItem[_slotIdx] = acs;                              // 악세사리 저장
        }

        // 현재 아이템이 대한 스탯 (추가체력, 물리/마법방어력 or 치명타)을 추가해줌
        SetPlayerState(_eqp , 1);
    }

    void SetPlayerState(Equipment v_equip , int v_h) 
    {
        // state을 더하면 v_h는 1 , state을 빼야하면 -1

        // 장비 아이템 추가체력
        GameManager.instance.playerManager.AddPlayerHP(v_equip.EquipmentData.addHP * v_h);

        // clothes의 물리, 마법 방어력
        if (v_equip is ClothesEquipment ce)
        {
            GameManager.instance.playerManager.AddPhyDefen(ce.ClothesData.physicDefencity * v_h );
            GameManager.instance.playerManager.AddMasicDefen(ce.ClothesData.masicDefencity * v_h);
        }

        // 악세사리의 치명타
        else if (v_equip is Accessory asc) 
        {
            GameManager.instance.playerManager.AddCounter(asc.accessoryData.counter * v_h);
        }
    }

}
