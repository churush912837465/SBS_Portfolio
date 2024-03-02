using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PlayerInfoUi : MonoBehaviour
{
    // ĳ���� ��� ĭ 4��
    [Header("���")]
    [SerializeField]
    private GameObject[] _equipObj;
    [SerializeField]
    private Sprite[] _equipImage;
    [SerializeField]
    private PlayerSlot[] _equipSlots;

    // ĳ���� ��ű� ĭ 4��
    [Header("��ű�")]
    [SerializeField]
    private GameObject[] _accessoryObj;
    [SerializeField]
    private Sprite _acceImage;
    [SerializeField]
    private PlayerSlot[] _accSlots;

    [Header("Item List ����")]
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

        // ����̸�?
        // Slot�� ���� �� �ش� equip�� ���� �Ǿ� �ִ� index�� �����ؼ�,
        // �� index�� �ش��ϴ� slot�� set�ؾ���
        if (v_item is ClothesEquipment ep)
        {
            // �̹� ���Կ� �������� ������
            if (_equipSlots[_slotIdx].HasItem)
            {
                // inventory�� ���� �������� add �ϸ� �ɵ�?
                GameManager.instance.inventory.GetAddItem(v_item);
                // ���� �ִ� ����� ������ (-) �������
                SetPlayerState(_eqp, -1);
            }

            // ��� slot�� �����
            _equipSlots[_slotIdx].ChangeIcon(v_item.itemData.Icon);
            _equiItem[_slotIdx] = v_item;                           // ������ ����

        }
        // �Ǽ��縮 �̸�?
        else if (v_item is Accessory acs)
        {
            // �̹� ���Կ� �������� ������
            if (_accSlots[_slotIdx].HasItem)
            {
                GameManager.instance.inventory.GetAddItem(v_item);
                SetPlayerState(_eqp, -1);
            }

            // �Ǽ��縮slot�� �����
            _accSlots[_slotIdx].ChangeIcon(v_item.itemData.Icon);
            _equiItem[_slotIdx] = acs;                              // �Ǽ��縮 ����
        }

        // ���� �������� ���� ���� (�߰�ü��, ����/�������� or ġ��Ÿ)�� �߰�����
        SetPlayerState(_eqp , 1);
    }

    void SetPlayerState(Equipment v_equip , int v_h) 
    {
        // state�� ���ϸ� v_h�� 1 , state�� �����ϸ� -1

        // ��� ������ �߰�ü��
        GameManager.instance.playerManager.AddPlayerHP(v_equip.EquipmentData.addHP * v_h);

        // clothes�� ����, ���� ����
        if (v_equip is ClothesEquipment ce)
        {
            GameManager.instance.playerManager.AddPhyDefen(ce.ClothesData.physicDefencity * v_h );
            GameManager.instance.playerManager.AddMasicDefen(ce.ClothesData.masicDefencity * v_h);
        }

        // �Ǽ��縮�� ġ��Ÿ
        else if (v_equip is Accessory asc) 
        {
            GameManager.instance.playerManager.AddCounter(asc.accessoryData.counter * v_h);
        }
    }

}
