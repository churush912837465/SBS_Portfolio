using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRetunToVillage : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // 마을 안에 위치로 옮기기
        collision.gameObject.transform.position = new Vector3(105f, 45f, -152f);
    }
}
