using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collider : MonoBehaviour
{
    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Player collided!");
            FindAnyObjectByType<Player>().enabled = false;
        }
    }
}
