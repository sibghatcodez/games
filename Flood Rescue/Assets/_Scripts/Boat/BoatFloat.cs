using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatFloat : MonoBehaviour
{
    public Transform[] floaters;
    public float underWaterDrag = 3f;
    public float underWaterAngularDrag = 1f;
    public float airWaterDrag = 0f;
    public float airWaterAngularDrag = 0.05f;

    public float floatingPower = 15f;

    public float baseWaterHeight = 0f;
    public float waterHeightVariation = 2f;
    public float waveSpeed = 1.0f;
    public float waterHeight;

    [SerializeField] private Rigidbody rb;
    int floatersUnderwater;
    bool underwater;
    float diff;

    void FixedUpdate()
    {
        waterHeight = baseWaterHeight + Mathf.Sin(Time.time * waveSpeed) * (waterHeightVariation / 2f);

        floatersUnderwater = 0;
        for (int i = 0; i < floaters.Length; i++)
        {
            diff = floaters[i].position.y - waterHeight;

            if (diff < 0)
            {
                rb.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(diff), floaters[i].position, ForceMode.Force);
                floatersUnderwater++;
                if (!underwater)
                {
                    underwater = true;
                }
            }
        }
        if (underwater && floatersUnderwater == 0)
        {
            underwater = false;
        }
    }
}