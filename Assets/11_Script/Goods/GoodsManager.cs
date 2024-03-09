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

    // 다른 스크립트에서 이걸 가져다 쓸 때
    // GoodsType.Gold 이케 하면? 편하지 않을까?
    private void InitGoods() 
    {
        _goodsList = new List<Goods>
        {
            new Gold    ( "Gold"     , 30 , _goodsListSprite[0]),
            new Shilling( "Shilling" , 40 , _goodsListSprite[1]),
            new HonorFragment ( "A Fragment of Honor" , 20 , _goodsListSprite[2])
        };
    }

    // 돈이 있는지 체크
    public bool CheckGoodsIsValid(GoodsType v_type , int v_amt) 
    {
        if (_goodsList[(int)v_type].GoodsCount - v_amt >= 0)
            return true;
        else 
            return false;
    }

    // 플레이어가 재화 획득
    public void PlayerGetGoods(GoodsType v_type , int v_amt) 
    {
        _goodsList[(int)v_type].AddGoods(v_amt);        // goods 클래스의 수량만큼 더하는 함수 실행

        GameManager.instance.goodsUi.UPdateGoodsUI(v_type);                  // Ui 초기화
    }

    // 플레이어가 재회 잃음
    public void PlayerLostGoods(GoodsType v_type , int v_amt) 
    {
        _goodsList[(int)v_type].SubbGoods(v_amt);       // goods 클래스의 수량만큼 빼는 함수

        GameManager.instance.goodsUi.UPdateGoodsUI(v_type);                  // ui 초기화
    }

}
