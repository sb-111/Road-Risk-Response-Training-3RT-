using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerPlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject outerPlayer; // ��ü�� �÷��̾�
    [SerializeField] private DoorInteraction doorInteraction; // ������ ������ ����
    [SerializeField] private CarController carController; // �� ���� ��ũ��Ʈ
    private void OnEnable()
    {
        // Inner Player�� Ȱ��ȭ�Ǹ� ���� rigidbody �����ؾ� ��.
        Rigidbody rb = carController.gameObject.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // ������ X, Z�� ȸ�� ����
        rb.useGravity = true;
        rb.velocity = Vector3.zero;

        // ������ Rigidbody �߰� �� CarController ��ũ��Ʈ�� rb�� ������ �� �ֵ���
        carController.Initialze(rb);
    }
    private void OnDisable()
    {
        // Inner Player�� ��Ȱ��ȭ�Ǹ� ���� rigidbody ����
        Rigidbody rb = carController.gameObject.GetComponent<Rigidbody>();
        Destroy(rb);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && doorInteraction.GetIsOpen())
        {
            ExitCar();
        }
        HandleDoorInteraction();
        HandleCarControls();
    }
    private void ExitCar()
    {
        // ������ ���� �� ����
        outerPlayer.SetActive(true);
        gameObject.SetActive(false);
    }
    private void HandleDoorInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            doorInteraction.ToggleDoor();
        }
    }

    private void HandleCarControls()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (vertical > 0)
        {
            carController.Accelerate(vertical);
        }
        else if (vertical < 0)
        {
            carController.Brake();
        }

        carController.Turn(horizontal);

        if (Input.GetKeyDown(KeyCode.B))
        {
            carController.ToggleEmergencyLights();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            carController.ToggleParkingBrake();
        }
    }

}
