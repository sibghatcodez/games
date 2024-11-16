using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] public float forwardSpeed = 1000f;
    [SerializeField] public float moveSpeed = 500f;
    [SerializeField] public Rigidbody rb;


    void Update () {
        rb.AddForce(-UnityEngine.Vector3.right * forwardSpeed * Time.deltaTime);

        if(Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow) ) {
            rb.AddForce(UnityEngine.Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if(Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow) ) {
            rb.AddForce(-UnityEngine.Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}
