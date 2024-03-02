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
    List<string> _porName = new List<string> { "회복약", "고급 회복약", "정령의 회복약" };
    List<string> _porToolTip = new List<string> { "hp의 30%를 획득합니다", "hp의 50%를 획득합니다", "hp의 70%를 획득합니다" };
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
    List<string> _bombName = new List<string> { "회오리 수류탄", "파괴 폭탄"};
    List<string> _bomToolTip = new List<string> { "수류탄을 적에게 던져  강한 무력 수치를 줍니다", "수류탄을 적에게 던져 강한 데미지를 줍니다" };
    List<int> _bombCool = new List<int> { 30,30,30 };
    List<float> _bombDamage = new List<float> { 100f, 30f};
    List<float> _bombforce = new List<float> { 30f, 100f };
    #endregion

    [Header("Equip Clothes")]
    #region Clothes

    [SerializeField]
    private int _clothesInit = 4;       // 장비는 투구/상의/하의/무기 4종
    [SerializeField]
    private List<Sprite> _clothesSpriteList;
    List<string> _EquipName = new List<string> { "몽환의 환각 머리장식", "몽환의 환각 상의" , "몽환의 환각 하의" , "몽환의 환각 장갑" };
    List<string> _EquipToolTip = new List<string> { "고대 머리 방어구", "고대 상의" , "고대 하의" , "고대 장갑" };
    List<float> _EquipAddHp = new List<float> { 50f, 60f, 60f, 40f};
    List<float> _EquipPhy = new List<float> { 6, 7, 7, 5 };
    List<float> _EquipMas = new List<float> { 6, 7, 7, 5};
    #endregion

    [Header("Accessory")]
    #region Accessory

    [SerializeField]
    private int _accessInit = 4;       // 악세사리는 목걸이/귀걸이/반지/팔지 4종
    [SerializeField]
    private List<Sprite> _accessSpriteList;
    List<string> __accessSpriteListName = new List<string> { "거룩한 수호자의 목걸이", "참혹한 파멸의 귀걸이", "참혹한 종말의 반지", "찬란한 영웅의 팔찌" };
    List<string> __accessSpriteListToolTip = new List<string> { "고대 목걸이", "고대 목걸이", "고대 귀걸이", "고대 팔찌" };
    List<float> _accessAddHp = new List<float> {10f , 5f, 5f, 7f};
    List<float> _accessCounter = new List<float> { 5f, 3f, 3f, 10f };

    #endregion

    public void PlayerGetPortion() 
    {
        int rand = UnityEngine.Random.Range(0, _PortionInit);
        // List에 저장되어 있는 portion을 새로 new 해서 넘겨야됨
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
        // 유효한 인덱스
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
        // 유효한 인덱스
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
        // 유효한 인덱스
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
        // 유효한 인덱스
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
