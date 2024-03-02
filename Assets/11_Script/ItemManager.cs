using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UIElements;



public class ItemManager : MonoBehaviour
{
    public Inventory _inventory;

    [Header("Data FIeld")]
    ItemData data;

    CountableData countableData;
    PortionData portionData;
    BombData bombData;

    EquipmentData equipmentData;
    ClothesData clothesData;
    AccessoryData accessorydata;

    [Header("Portion Init")]
    #region portion 

    [SerializeField]
    private int _PortionInit = 3;
    [SerializeField]
    private List<Sprite> _portionSpriteList;

    int _porMaxAmount = 20;
    List<string> _porName = new List<string> { "ȸ����", "��� ȸ����", "������ ȸ����" };
    List<string> _porToolTip = new List<string> { "hp�� 30%�� ȹ���մϴ�", "hp�� 50%�� ȹ���մϴ�", "hp�� 70%�� ȹ���մϴ�" };
    List<int> _porCool = new List<int> { 3, 5, 7 };
    List<float> _porHealAmount = new List<float> { 30f, 50f, 70f };
    #endregion

    [Header("Bomb Init")]
    #region Bomb

    [SerializeField]
    private int _bombInit = 2;
    [SerializeField]
    private List<Sprite> _bombSpriteList;
    int _bombMaxAmount = 5;
    List<string> _bombName = new List<string> { "ȸ���� ����ź", "�ı� ��ź"};
    List<string> _bomToolTip = new List<string> { "����ź�� ������ ����  ���� ���� ��ġ�� �ݴϴ�", "����ź�� ������ ���� ���� �������� �ݴϴ�" };
    List<int> _bombCool = new List<int> { 30,30,30 };
    List<float> _bombDamage = new List<float> { 100f, 30f};
    List<float> _bombforce = new List<float> { 30f, 100f };
    #endregion

    [Header("Equip Clothes")]
    private Item[] equipList;
    #region Clothes

    [SerializeField]
    private int _clothesInit = 4;       // ���� ����/����/����/���� 4��
    [SerializeField]
    private List<Sprite> _clothesSpriteList;
    List<string> _EquipName = new List<string> { "��ȯ�� ȯ�� �Ӹ����", "��ȯ�� ȯ�� ����" , "��ȯ�� ȯ�� ����" , "��ȯ�� ȯ�� �尩" };
    List<string> _EquipToolTip = new List<string> { "��� �Ӹ� ��", "��� ����" , "��� ����" , "��� �尩" };
    List<float> _EquipAddHp = new List<float> { 50f, 60f, 60f, 40f};
    List<float> _EquipPhy = new List<float> { 6, 7, 7, 5 };
    List<float> _EquipMas = new List<float> { 6, 7, 7, 5};
    #endregion

    [Header("Accessory")]
    private Item[] accessList;
    #region Accessory

    [SerializeField]
    private int _accessInit = 4;       // �Ǽ��縮�� �����/�Ͱ���/����/���� 4��
    [SerializeField]
    private List<Sprite> _accessSpriteList;
    List<string> __accessSpriteListName = new List<string> { "�ŷ��� ��ȣ���� �����", "��Ȥ�� �ĸ��� �Ͱ���", "��Ȥ�� ������ ����", "������ ������ ����" };
    List<string> __accessSpriteListToolTip = new List<string> { "��� �����", "��� �����", "��� �Ͱ���", "��� ����" };
    List<float> _accessAddHp = new List<float> {10f , 5f, 5f, 7f};
    List<float> _accessCounter = new List<float> { 5f, 3f, 3f, 10f };

    #endregion


    private void Start()
    {
        InitEquiptItem();       // ��� �ʱ�ȭ
        InitAccessory();        // �Ǽ��縮 �ʱ�ȭ

    }
    public void PlayerGetPortion() 
    {
        int rand = UnityEngine.Random.Range(0, _PortionInit);
        // List�� ����Ǿ� �ִ� portion�� ���� new �ؼ� �Ѱܾߵ�
        GameManager.instance.inventory.GetAddItem(GetPortion(rand));
    }

    public void PlayerGetBomb() 
    {
        int rand = UnityEngine.Random.Range(0, _bombInit);
        GameManager.instance.inventory.GetAddItem(GetBomb(rand));
    }

    public void PlayerGetEquip() 
    {
        int rand = UnityEngine.Random.Range(0, _clothesInit);
        GameManager.instance.inventory.GetAddItem(GetEquip(rand));
    }

    public void PlayerGetAccessory() 
    {
        int rand = UnityEngine.Random.Range(0, _accessInit);
        GameManager.instance.inventory.GetAddItem(GetAccessory(rand));
    }


    private Item GetAccessory(int v_idx) 
    {
        // ��ȿ�� �ε���
        if (!IsvalidIdx(v_idx, _accessInit))
        {
            return null;
        }

        return accessList[v_idx];
    }

    private void InitAccessory()
    {
        accessList = new Item[4];   // �Ǽ��縮 4��

        for (int i = 0; i < 4; i++)
        {
            data = new ItemData();
            equipmentData = new EquipmentData();
            accessorydata = new AccessoryData();

            data.setItemDataField(i, __accessSpriteListName[i], __accessSpriteListToolTip[i], _accessSpriteList[i]);
            equipmentData.setEquipDataField(_accessAddHp[i]);
            accessorydata.setAccessoryDataField(_accessCounter[i]);

            Item acc = new Accessory(data, equipmentData, accessorydata);
            accessList[i] = acc;
        }
    }
    private void InitEquiptItem() 
    {
        equipList = new Item[4];    // ������ 4��

        for (int i = 0; i < 4; i++) 
        {

            data = new ItemData();
            equipmentData = new EquipmentData();
            clothesData = new ClothesData();

            data.setItemDataField(i, _EquipName[i], _EquipToolTip[i], _clothesSpriteList[i]);
            equipmentData.setEquipDataField(_EquipAddHp[i]);
            clothesData.setClothesDataField(_EquipPhy[i], _EquipMas[i]);

            Item eq = new ClothesEquipment(data, equipmentData, clothesData);
            equipList[i] = eq;
        }


    }

    private Item GetEquip(int v_idx) 
    {
        // ��ȿ�� �ε���
        if (!IsvalidIdx(v_idx, _clothesInit))
        {
            return null;
        }

        return equipList[v_idx];
    }

    private Item GetBomb(int v_idx) 
    {
        // ��ȿ�� �ε���
        if (!IsvalidIdx(v_idx, _PortionInit))
        {
            return null;
        }

        data = new ItemData();
        countableData = new CountableData();
        bombData = new BombData();

        data.setItemDataField(v_idx, _bombName[v_idx], _bomToolTip[v_idx], _bombSpriteList[v_idx]);
        countableData.setCountableDataField(1, _bombMaxAmount, _bombCool[v_idx]);
        bombData.setBombDataField(_bombDamage[v_idx] , _bombforce[v_idx]);

        Item bo = new Bomb(data, countableData, bombData);
        return bo;
    }

    private Item GetPortion(int v_idx) 
    {
        // ��ȿ�� �ε���
        if (!IsvalidIdx(v_idx , _PortionInit)) 
        {
            return null;
        }

        data = new ItemData();
        countableData = new CountableData();
        portionData = new PortionData();

        data.setItemDataField(v_idx, _porName[v_idx], _porToolTip[v_idx], _portionSpriteList[v_idx]);
        countableData.setCountableDataField(1, _porMaxAmount, _porCool[v_idx]);
        portionData.setPortionDataField(_porHealAmount[v_idx]);

        Item po = new Portion(data, countableData, portionData);
        return po;
    }

    private bool IsvalidIdx(int v_idx , int v_max) 
    {
        if (v_idx >= 0 && v_idx < v_max)
            return true;
        return false;
    }

}
