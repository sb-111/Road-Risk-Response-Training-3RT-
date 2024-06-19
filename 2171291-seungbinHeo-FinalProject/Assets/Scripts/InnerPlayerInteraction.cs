using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerPlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject outerPlayer; // 교체할 플레이어
    [SerializeField] private DoorInteraction doorInteraction; // 운전석 문으로 설정
    [SerializeField] private CarController carController; // 차 조종 스크립트
    private void OnEnable()
    {
        // Inner Player가 활성화되면 차에 rigidbody 부착해야 함.
        Rigidbody rb = carController.gameObject.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // 차량의 X, Z축 회전 고정
        rb.useGravity = true;
        rb.velocity = Vector3.zero;

        // 차량에 Rigidbody 추가 후 CarController 스크립트가 rb에 접근할 수 있도록
        carController.Initialze(rb);
    }
    private void OnDisable()
    {
        // Inner Player가 비활성화되면 차의 rigidbody 해제
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
        // 차에서 하차 시 설정
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
