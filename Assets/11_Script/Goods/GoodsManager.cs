using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public enum GoodsType
{
    Gold, 
    Silling
}

public class GoodsManager : MonoBehaviour
{
    public static GoodsManager instance;

    [SerializeField]
    List<Goods> _goodsList;
    [SerializeField]
    Sprite[] _goodsListSprite;

    private void Awake()
    {
        instance = this;    
    }

    private void Start()
    {
        InitGoods();        // Init
    }

    // �ٸ� ��ũ��Ʈ���� �̰� ������ �� ��
    // GoodsType.Gold ���� �ϸ�? ������ ������?
    private void InitGoods() 
    {
        _goodsList = new List<Goods>
        {
            new Gold    ( "Gold"     , 9999 , _goodsListSprite[0]),
            new Shilling( "Shilling" , 9999 , _goodsListSprite[1])
        };
    }

    // �÷��̾ ��ȭ ȹ��
    public void PlayerGetGoods(GoodsType v_type , int v_amt) 
    {
        _goodsList[(int)v_type].AddGoods(v_amt);        // goods Ŭ������ ������ŭ ���ϴ� �Լ� ����
    }

    // �÷��̾ ��ȸ ����
    public void PlayerLostGoods(GoodsType v_type , int v_amt) 
    {
        _goodsList[(int)v_type].SubbGoods(v_amt);       // goods Ŭ������ ������ŭ ���� �Լ�
    }

}
