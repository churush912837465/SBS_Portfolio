using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRetunToVillage : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // ���� �ȿ� ��ġ�� �ű��
        collision.gameObject.transform.position = new Vector3(105f, 45f, -152f);
    }
}
