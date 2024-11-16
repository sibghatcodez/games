using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Transform player;
    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x + 2, player.position.y + 1, player.position.z);
    }
}
