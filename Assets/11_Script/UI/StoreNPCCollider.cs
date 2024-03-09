using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreNPCCollider : MonoBehaviour
{
    [SerializeField]
    GameObject _storePanel;

    private void OnTriggerEnter(Collider other)
    {
        _storePanel.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        _storePanel.gameObject.SetActive(false);
    }
}
