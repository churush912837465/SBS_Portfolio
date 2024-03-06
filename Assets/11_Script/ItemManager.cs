using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UIElements;

public enum PortionType 
{
    potion,             // 일반 회복약
    highPortion,        // 고급 회복약
    spiritsPortin       // 정령의 회복약
}
public enum BombType 
{ 
    tornadoBomb,        // 회오리 수류탄
    destroyBomb         // 파괴 폭탄
}

public class ItemManager : MonoBehaviour
{
    [Header("Item List")]
    private List<Item> _portionList;
    private List<Item> _bombList;
    private List<Item> _equipList;
    private List<Item> _accessoryList;

    [Header("Portion")]
    [SerializeField]
    private List<Sprite> _portionSpriteList;

    [Header("Bomb")]
    [SerializeField]
    private List<Sprite> _bombSpriteList;

    [Header("Equip Clothes")]
    [SerializeField]
    private List<Sprite> _clothesSpriteList;

    [Header("Accessory")]
    [SerializeField]
    private List<Sprite> _accessSpriteList;

    private void Start()
    {
        InitPortion();          // 포션 초기화
        InitBomb();             // 폭탄 초기화
        InitEquiptItem();       // 장비 초기화
        InitAccessory();        // 악세사리 초기화

    }

    #region 나중에 수정 & 이동할 내용 (지금은 버튼이랑 연결되어 있음)

    public void PlayerGetPortion(PortionType v_type) 
    {
        GameManager.instance.inventory.GetAddItem(_portionList[(int)v_type]);
    }

    public void PlayerGetBomb(BombType v_type) 
    {
        GameManager.instance.inventory.GetAddItem(_bombList[(int)v_type]);
    }

    public void PlayerGetEquip(int v_idx) 
    {
        GameManager.instance.inventory.GetAddItem(_equipList[v_idx]);
    }

    public void PlayerGetAccessory(int v_idx) 
    {
        GameManager.instance.inventory.GetAddItem(_accessoryList[v_idx]);
    }

    #endregion

    #region Store manager 에서 사용중인
    public Item ReturnPortion(PortionType v_type) 
    {
        return _portionList[(int)v_type];
    }
    public Item ReturnBomb(BombType v_type) 
    {
        return _bombList[(int)v_type];
    }
    #endregion

    private void InitPortion() 
    {
        _portionList = new List<Item>
        {
            new Portion
            (
                new ItemData ( 0 , "회복약" , "hp의 30%를 획득합니다" , _portionSpriteList[0]),
                new CountableData ( 1, 10 , 3),
                new PortionData( 30f )
            ),
            new Portion
            (
                new ItemData ( 1 , "고급 회복약" , "hp의 50%를 획득합니다" , _portionSpriteList[1]),
                new CountableData ( 1, 10 , 5),
                new PortionData( 50f )
            ),
            new Portion
            (
                new ItemData ( 2 , "정령의 회복약" , "hp의 70%를 획득합니다" , _portionSpriteList[0]),
                new CountableData ( 1, 10 , 7),
                new PortionData( 70f )
            )
        };
    }

    private void InitBomb() 
    {
        _bombList = new List<Item>
        {
            new Bomb
            (
                new ItemData ( 0 , "회오리 수류탄" , "파괴 폭탄" , _bombSpriteList[0]),
                new CountableData ( 1, 5 , 30),
                new BombData( 100f , 30f )
            ),
            new Bomb
            (
                new ItemData ( 1 , "고급 회복약" , "hp의 50%를 획득합니다" , _bombSpriteList[1]),
                new CountableData ( 1, 5 , 30),
                new BombData( 30f , 100f )
            )
        };
    }

    private void InitEquiptItem() 
    {
        _equipList = new List<Item>
        {
            new ClothesEquipment
                (
                    new ItemData ( 0 , "몽환의 환각 머리장식" , "고대 머리 방어구" , _clothesSpriteList[0]),
                    new EquipmentData (50f),
                    new ClothesData( 6f, 6f )
                 ),
            new ClothesEquipment
                (
                    new ItemData ( 1 , "몽환의 환각 상의" , "고대 상의" , _clothesSpriteList[1]),
                    new EquipmentData (60f),
                    new ClothesData( 7f, 7f )
                 ),
            new ClothesEquipment
                (
                    new ItemData ( 2 , "몽환의 환각 하의" , "고대 하의" , _clothesSpriteList[2]),
                    new EquipmentData (60f),
                    new ClothesData( 7f, 7f )
                 ),
            new ClothesEquipment
                (
                    new ItemData ( 3 , "몽환의 환각 장갑" , "고대 장갑" , _clothesSpriteList[3]),
                    new EquipmentData ( 40f),
                    new ClothesData( 5f, 5f )
                 )
        };
    }

    private void InitAccessory()
    {
        _accessoryList = new List<Item>
        {
            new Accessory
                (
                    new ItemData ( 0 , "거룩한 수호자의 목걸이" , "고대 목걸이" , _accessSpriteList[0]),
                    new EquipmentData (10f),
                    new AccessoryData(5f)
                 ),
            new Accessory
                (
                    new ItemData ( 1 , "참혹한 파멸의 귀걸이" , "고대 귀걸이" , _accessSpriteList[1]),
                    new EquipmentData (5f),
                    new AccessoryData(3f)
                 ),
            new Accessory
                (
                    new ItemData ( 2 , "참혹한 종말의 반지" , "고대 반지" , _accessSpriteList[2]),
                    new EquipmentData (5f),
                    new AccessoryData(3f)
                 ),
            new Accessory
                (
                    new ItemData ( 3 , "찬란한 영웅의 팔찌" , "고대 팔찌" , _accessSpriteList[3]),
                    new EquipmentData (7f),
                    new AccessoryData(10f)
                 )
        };
    }


}
