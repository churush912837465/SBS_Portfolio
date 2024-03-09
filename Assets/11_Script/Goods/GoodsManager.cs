using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public enum GoodsType
{
    Gold, 
    Silling,
    Honor
}

public class GoodsManager : MonoBehaviour
{
    public static GoodsManager instance;
 
    [SerializeField]
    public List<Goods> _goodsList;
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
            new Gold    ( "Gold"     , 30 , _goodsListSprite[0]),
            new Shilling( "Shilling" , 40 , _goodsListSprite[1]),
            new HonorFragment ( "A Fragment of Honor" , 20 , _goodsListSprite[2])
        };
    }

    // ���� �ִ��� üũ
    public bool CheckGoodsIsValid(GoodsType v_type , int v_amt) 
    {
        if (_goodsList[(int)v_type].GoodsCount - v_amt >= 0)
            return true;
        else 
            return false;
    }

    // �÷��̾ ��ȭ ȹ��
    public void PlayerGetGoods(GoodsType v_type , int v_amt) 
    {
        _goodsList[(int)v_type].AddGoods(v_amt);        // goods Ŭ������ ������ŭ ���ϴ� �Լ� ����

        GameManager.instance.goodsUi.UPdateGoodsUI(v_type);                  // Ui �ʱ�ȭ
    }

    // �÷��̾ ��ȸ ����
    public void PlayerLostGoods(GoodsType v_type , int v_amt) 
    {
        _goodsList[(int)v_type].SubbGoods(v_amt);       // goods Ŭ������ ������ŭ ���� �Լ�

        GameManager.instance.goodsUi.UPdateGoodsUI(v_type);                  // ui �ʱ�ȭ
    }

}
