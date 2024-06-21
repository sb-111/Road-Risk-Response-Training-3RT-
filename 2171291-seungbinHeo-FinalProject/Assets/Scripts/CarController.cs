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

    private bool isCutCompleted = false;

    [SerializeField] private GameObject[] shotPositions;
    private void Start()
    {
        pd = GetComponent<PlayableDirector>();
        pd.stopped += OnCutsceneStopped; // �ƽ��� ������ �� ȣ��� �̺�Ʈ �ڵ鷯 ���
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
        if (GameManager.Instance.GetSituation() == Situation.Accident && TaskManager.Instance.IsAddedAllTasks())
        {
            if (emergencyLightsOn)
                TaskManager.Instance.CompleteTask(2, "���� ���� �Ϸ�!");
        }
        Debug.Log("Emergency lights " + (emergencyLightsOn ? "On" : "Off"));
    }

    public void ToggleParkingBrake()
    {
        // ���̵�극��ũ ���
        parkingBrakeOn = !parkingBrakeOn;
        if(GameManager.Instance.GetSituation() == Situation.Accident && TaskManager.Instance.IsAddedAllTasks())
        {
            if(parkingBrakeOn)
                TaskManager.Instance.CompleteTask(1, "���� �Ϸ�!");
        }
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
        if(GameManager.Instance.GetSituation() == Situation.Accident)
        {
            if (other.gameObject.CompareTag("RoadEntry"))
            {
                gameObject.transform.position = startPosition.position;
                TaskManager.Instance.CompleteTask(0, "���� ���� �Ϸ�!");
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
    // Ÿ�Ӷ��� �� ����
    private void OnCutsceneStopped(PlayableDirector director)
    {
        // �ƽ��� ����� �� TaskManager�� ���� ���ο� Task�� �߰�
        Debug.Log("�ƽ� ����, ���ο� Task �߰�");
        TaskManager.Instance.AddTask("����", "����������? �ϴ� ��������.", 10, "��� �Ŀ� �ٷ� �������� ����", 10f);
        TaskManager.Instance.AddTask("����", "������ �����ؾ߰ھ�..", 10, "������ �������� ����", 15f);
        TaskManager.Instance.AddTask("��Ȳ �ľ�", "������ ���� ��Ȳ�� �ľ��ؾ߰ھ�.", 10, "��Ȳ�� �ľ����� ����", 20f);
        TaskManager.Instance.AddTask("�Ű�", "��� ��ȭ�� �ؾ�����?", 10, "�Ű� �������� ����", 70f);
        TaskManager.Instance.AddTask("���� ����", "���� ������ �ؾ��� �� ����..", 10, "���Ÿ� �������� ����", 100f);
        TaskManager.Instance.AddTask("���� Ȯ��", "���� ������ ������ ������ �� �ִ��� Ȯ������..", 10, "������ ������ �������� ����", 60f);
        TaskManager.Instance.AddTask("Ʈ��ũ", "Ʈ��ũ�� ����߰ھ�..", 10, "Ʈ��ũ�� ���� ����", 70f);
        TaskManager.Instance.AddTask("�ﰢ��", "2����� �������..", 10, "�ﰢ�븦 ����� ��ġ���� ����", 100f);
        TaskManager.Instance.AddAllTasks(); // ��� ��Ȳ �߰� �Ϸ�

        foreach(var shotPosition in shotPositions)
        {
            shotPosition.SetActive(true);
        }

        GameManager.Instance.StartTimer();
    }
}

/*
 TaskManager.Instance.AddTask("��� �߻�", "�����ؾ� �� �� ����.");
                TaskManager.Instance.AddTask("��� �߻�", "������ Ű��");
                TaskManager.Instance.AddTask("��� �߻�", )
 
 */