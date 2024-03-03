using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoryObject : MonoBehaviour
{
    [SerializeField]
    GameObject _mapObejct;

    void Start()
    {
        DontDestroyOnLoad(_mapObejct);
    }


}
