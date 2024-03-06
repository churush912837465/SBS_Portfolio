using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UIElements;

public enum PortionType 
{
    potion,             // �Ϲ� ȸ����
    highPortion,        // ��� ȸ����
    spiritsPortin       // ������ ȸ����
}
public enum BombType 
{ 
    tornadoBomb,        // ȸ���� ����ź
    destroyBomb         // �ı� ��ź
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
        InitPortion();          // ���� �ʱ�ȭ
        InitBomb();             // ��ź �ʱ�ȭ
        InitEquiptItem();       // ��� �ʱ�ȭ
        InitAccessory();        // �Ǽ��縮 �ʱ�ȭ

    }

    #region ���߿� ���� & �̵��� ���� (������ ��ư�̶� ����Ǿ� ����)

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

    #region Store manager ���� �������
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
                new ItemData ( 0 , "ȸ����" , "hp�� 30%�� ȹ���մϴ�" , _portionSpriteList[0]),
                new CountableData ( 1, 10 , 3),
                new PortionData( 30f )
            ),
            new Portion
            (
                new ItemData ( 1 , "��� ȸ����" , "hp�� 50%�� ȹ���մϴ�" , _portionSpriteList[1]),
                new CountableData ( 1, 10 , 5),
                new PortionData( 50f )
            ),
            new Portion
            (
                new ItemData ( 2 , "������ ȸ����" , "hp�� 70%�� ȹ���մϴ�" , _portionSpriteList[0]),
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
                new ItemData ( 0 , "ȸ���� ����ź" , "�ı� ��ź" , _bombSpriteList[0]),
                new CountableData ( 1, 5 , 30),
                new BombData( 100f , 30f )
            ),
            new Bomb
            (
                new ItemData ( 1 , "��� ȸ����" , "hp�� 50%�� ȹ���մϴ�" , _bombSpriteList[1]),
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
                    new ItemData ( 0 , "��ȯ�� ȯ�� �Ӹ����" , "��� �Ӹ� ��" , _clothesSpriteList[0]),
                    new EquipmentData (50f),
                    new ClothesData( 6f, 6f )
                 ),
            new ClothesEquipment
                (
                    new ItemData ( 1 , "��ȯ�� ȯ�� ����" , "��� ����" , _clothesSpriteList[1]),
                    new EquipmentData (60f),
                    new ClothesData( 7f, 7f )
                 ),
            new ClothesEquipment
                (
                    new ItemData ( 2 , "��ȯ�� ȯ�� ����" , "��� ����" , _clothesSpriteList[2]),
                    new EquipmentData (60f),
                    new ClothesData( 7f, 7f )
                 ),
            new ClothesEquipment
                (
                    new ItemData ( 3 , "��ȯ�� ȯ�� �尩" , "��� �尩" , _clothesSpriteList[3]),
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
                    new ItemData ( 0 , "�ŷ��� ��ȣ���� �����" , "��� �����" , _accessSpriteList[0]),
                    new EquipmentData (10f),
                    new AccessoryData(5f)
                 ),
            new Accessory
                (
                    new ItemData ( 1 , "��Ȥ�� �ĸ��� �Ͱ���" , "��� �Ͱ���" , _accessSpriteList[1]),
                    new EquipmentData (5f),
                    new AccessoryData(3f)
                 ),
            new Accessory
                (
                    new ItemData ( 2 , "��Ȥ�� ������ ����" , "��� ����" , _accessSpriteList[2]),
                    new EquipmentData (5f),
                    new AccessoryData(3f)
                 ),
            new Accessory
                (
                    new ItemData ( 3 , "������ ������ ����" , "��� ����" , _accessSpriteList[3]),
                    new EquipmentData (7f),
                    new AccessoryData(10f)
                 )
        };
    }


}
