using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData 
{
    // �ʵ�
    [SerializeField]
    private int _id;
    [SerializeField]
    private string _name;
    [SerializeField]
    private string _toolTip;
    [SerializeField]
    private Sprite _icon;

    // ������Ƽ
    public int Id { get => _id;}
    public string Name { get => _name; }
    public string ToolTip { get => _toolTip; }

    public Sprite Icon { get => _icon; }
    
    // �� ����
    public ItemData(int id, string name , string tooptip, Sprite icon) 
    {
        this._id = id;
        this._name = name;
        this._toolTip = tooptip;
        this._icon = icon;
    }
}
