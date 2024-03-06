using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessoryData 
{
    // 필드
    [SerializeField]
    float _counter;     // 치명타

    // 프로퍼티
    public float counter {get=> _counter;}

    public AccessoryData( float v_con ) 
    {
        this._counter = v_con;
    }

}
