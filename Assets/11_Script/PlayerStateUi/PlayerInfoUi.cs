using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        // ����̸�?
        if (v_item is Equipment ep)
        {
            // ��� slot�� �����
            // Slot�� ���� �� �ش� equip�� ���� �Ǿ� �ִ� index�� �����ؼ�,
            // �� index�� �ش��ϴ� slot�� set�ؾ���



        }
        else if (v_item is Accessory acs)
        {
            // �Ǽ��縮slot�� �����
            
        }

    }

}
