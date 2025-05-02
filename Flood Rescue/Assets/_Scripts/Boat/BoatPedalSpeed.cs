using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class BoatPedalSpeed : MonoBehaviour
{
    [SerializeField] private Animator pedalLeft, pedalRight;
    [SerializeField] private Rigidbody boatRigidbody;

    private void FixedUpdate()
    {
        pedalLeft.speed = GetVelocity();
        pedalRight.speed = GetVelocity();
    }
    private float GetVelocity() => Mathf.Clamp01(boatRigidbody.linearVelocity.magnitude);
}