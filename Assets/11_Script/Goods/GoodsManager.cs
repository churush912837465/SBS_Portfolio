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

    // 다른 스크립트에서 이걸 가져다 쓸 때
    // GoodsType.Gold 이케 하면? 편하지 않을까?
    private void InitGoods() 
    {
        _goodsList = new List<Goods>
        {
            new Gold    ( "Gold"     , 9999 , _goodsListSprite[0]),
            new Shilling( "Shilling" , 9999 , _goodsListSprite[1])
        };
    }

    // 플레이어가 재화 획득
    public void PlayerGetGoods(GoodsType v_type , int v_amt) 
    {
        _goodsList[(int)v_type].AddGoods(v_amt);        // goods 클래스의 수량만큼 더하는 함수 실행
    }

    // 플레이어가 재회 잃음
    public void PlayerLostGoods(GoodsType v_type , int v_amt) 
    {
        _goodsList[(int)v_type].SubbGoods(v_amt);       // goods 클래스의 수량만큼 빼는 함수
    }

}
