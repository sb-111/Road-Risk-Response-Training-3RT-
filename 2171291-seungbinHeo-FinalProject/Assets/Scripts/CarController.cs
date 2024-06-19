using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody rb;

    private bool emergencyLightsOn = false; // 비상등 여부
    private bool parkingBrakeOn = false;    // 사이드브레이크 여부


    public float accelerationForce = 10f;  // 가속력
    public float brakeForce = 20f;         // 브레이크력
    public float maxSpeed = 50f;           // 최대 속도
    public float turnSpeed = 10f;          // 회전 속도

    public void Initialze(Rigidbody rb)
    {
        this.rb = rb;
    }
    public void Accelerate(float input)
    {
        if (rb != null)
        {
            // 주차 브레이크 X && 최대 속도보다 낮을 때
            if (!parkingBrakeOn && rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(transform.forward * input * accelerationForce);
                Debug.Log("Accelerating");
            }
        }
    }

    public void Brake()
    {
        if (rb != null)
        {
            rb.velocity *= 0.9f; // 브레이크 적용
            Debug.Log("Braking");
        }
    }

    public void Turn(float input)
    {
        if (rb != null)
        {
            // 사이드 브레이크 해제 시 회전 가능
            if (!parkingBrakeOn)
            {
                Debug.Log($"회전값:{input}");
                //float turn = input * turnSpeed * Time.deltaTime;
                //rb.AddTorque(transform.up * turn);
                float turn = input * turnSpeed * Time.deltaTime;
                transform.Rotate(0, turn, 0);
            }
        }
    }

    public void ToggleEmergencyLights()
    {
        // 비상등 토글
        emergencyLightsOn = !emergencyLightsOn;
        Debug.Log("Emergency lights " + (emergencyLightsOn ? "On" : "Off"));
    }

    public void ToggleParkingBrake()
    {
        // 사이드브레이크 토글
        parkingBrakeOn = !parkingBrakeOn;
        if (parkingBrakeOn) // 사이드브레이크 시
        {
            // 위치와 회전 모두 고정
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
        else // 사이드브레이크 아닐 때
        {
            rb.constraints = RigidbodyConstraints.None;
        }
        Debug.Log("Parking brake " + (parkingBrakeOn ? "Engaged" : "Released"));
    }
}
