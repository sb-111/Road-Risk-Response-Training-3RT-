using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class CarController : MonoBehaviour
{
    private Rigidbody rb;

    private bool emergencyLightsOn = false; // ���� ����
    private bool parkingBrakeOn = false;    // ���̵�극��ũ ����
    private bool isBraking = false;         // �극��ũ ������ ����

    public float accelerationForce = 10f;  // ���ӷ�
    public float decelerationForce = 10f;  // ���ӷ�
    public float maxSpeed = 50f;           // �ִ� �ӵ�
    public float turnSpeed = 10f;          // ȸ�� �ӵ�

    private float currentSpeed = 0f;

    [SerializeField] private Transform startPosition;

    private PlayableDirector pd;
    [SerializeField] private TimelineAsset[] ta;
    private void Start()
    {
        pd = GetComponent<PlayableDirector>();
    }
    public void Initialze(Rigidbody rb)
    {
        this.rb = rb;
    }
    private void FixedUpdate()
    {
        if (isBraking)
        {
            Decelerate();
        }
    }
    public void Accelerate(float input)
    {
        //if (rb != null)
        //{
        //    // ���� �극��ũ X && �ִ� �ӵ����� ���� ��
        //    if (!parkingBrakeOn && rb.velocity.magnitude < maxSpeed)
        //    {
        //        rb.AddForce(transform.forward * input * accelerationForce);
        //        Debug.Log("Accelerating");
        //    }
        //}
        if (!parkingBrakeOn)
        {
            currentSpeed += input * accelerationForce * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
            Debug.Log("Accelerating");
        }
    }

    public void Brake()
    {
        //if (rb != null)
        //{
        //    rb.velocity *= 0.9f; // �극��ũ ����
        //    Debug.Log("Braking");
        //}

        //if (!parkingBrakeOn)
        //{
        //    currentSpeed -= decelerationForce * Time.deltaTime;
        //    currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        //    transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        //    Debug.Log("Braking");
        //}
        if (!parkingBrakeOn)
        {
            isBraking = true;
        }
    }
    public void StopBrake()
    {
        isBraking = false;
    }
    private void Decelerate()
    {
        // �극��ũ ���� �� ����
        if (isBraking)
        {
            currentSpeed -= decelerationForce * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

            Debug.Log("Decelerating");
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RoadEntry"))
        {
            gameObject.transform.position = startPosition.position;
        }
        if (other.gameObject.CompareTag("Accident"))
        {
            Debug.Log("�ƾ� ���");
            // ��� �߻� �ƾ� ������
            other.gameObject.SetActive(false);
            pd.Play(ta[0]);

        }
    }
}
