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

        data = new ItemData();
        equipmentData = new EquipmentData();
        accessorydata = new AccessoryData();

        data.setItemDataField(v_idx , __accessSpriteListName[v_idx] , __accessSpriteListToolTip[v_idx] , _accessSpriteList[v_idx]);
        equipmentData.setEquipDataField(_accessAddHp[v_idx]);
        accessorydata.setAccessoryDataField(_accessCounter[v_idx]);

        Item acc = new Accessory(data , equipmentData , accessorydata);
        return acc;
    }

    private Item GetEquip(int v_idx) 
    {
        // ��ȿ�� �ε���
        if (!IsvalidIdx(v_idx, _clothesInit))
        {
            return null;
        }

        data = new ItemData();
        equipmentData = new EquipmentData();
        clothesData = new ClothesData();

        data.setItemDataField(v_idx, _EquipName[v_idx] , _EquipToolTip[v_idx] , _clothesSpriteList[v_idx]);
        equipmentData.setEquipDataField(_EquipAddHp[v_idx]);
        clothesData.setClothesDataField(_EquipPhy[v_idx] , _EquipMas[v_idx]);

        Item eq = new ClothesEquipment(data, equipmentData , clothesData);
        return eq;
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
