using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessoryData 
{
    // �ʵ�
    [SerializeField]
    float _counter;     // ġ��Ÿ

    // ������Ƽ
    public float counter {get=> _counter;}

    public AccessoryData( float v_con ) 
    {
        this._counter = v_con;
    }

}
