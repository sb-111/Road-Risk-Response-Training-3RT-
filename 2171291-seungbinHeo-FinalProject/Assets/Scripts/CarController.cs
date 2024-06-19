using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody rb;

    private bool emergencyLightsOn = false; // ���� ����
    private bool parkingBrakeOn = false;    // ���̵�극��ũ ����


    public float accelerationForce = 10f;  // ���ӷ�
    public float brakeForce = 20f;         // �극��ũ��
    public float maxSpeed = 50f;           // �ִ� �ӵ�
    public float turnSpeed = 10f;          // ȸ�� �ӵ�

    public void Initialze(Rigidbody rb)
    {
        this.rb = rb;
    }
    public void Accelerate(float input)
    {
        if (rb != null)
        {
            // ���� �극��ũ X && �ִ� �ӵ����� ���� ��
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
            rb.velocity *= 0.9f; // �극��ũ ����
            Debug.Log("Braking");
        }
    }

    public void Turn(float input)
    {
        if (rb != null)
        {
            // ���̵� �극��ũ ���� �� ȸ�� ����
            if (!parkingBrakeOn)
            {
                Debug.Log($"ȸ����:{input}");
                //float turn = input * turnSpeed * Time.deltaTime;
                //rb.AddTorque(transform.up * turn);
                float turn = input * turnSpeed * Time.deltaTime;
                transform.Rotate(0, turn, 0);
            }
        }
    }

    public void ToggleEmergencyLights()
    {
        // ���� ���
        emergencyLightsOn = !emergencyLightsOn;
        Debug.Log("Emergency lights " + (emergencyLightsOn ? "On" : "Off"));
    }

    public void ToggleParkingBrake()
    {
        // ���̵�극��ũ ���
        parkingBrakeOn = !parkingBrakeOn;
        if (parkingBrakeOn) // ���̵�극��ũ ��
        {
            // ��ġ�� ȸ�� ��� ����
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
        else // ���̵�극��ũ �ƴ� ��
        {
            rb.constraints = RigidbodyConstraints.None;
        }
        Debug.Log("Parking brake " + (parkingBrakeOn ? "Engaged" : "Released"));
    }
}
