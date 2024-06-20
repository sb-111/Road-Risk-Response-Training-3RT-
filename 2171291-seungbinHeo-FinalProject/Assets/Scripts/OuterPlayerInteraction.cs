using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterPlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject innerPlayer; // ��ü�� �÷��̾�
    [SerializeField] private Camera playerCamera;
    bool isCarNear = false;
    [SerializeField] private LayerMask interactableLayer;

    private DoorInteraction currentDoor; // ���� ��ȣ�ۿ��ϴ� ���� �����ϱ� ���� ����
    private void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward * 20f);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        Debug.Log($"����ó:{isCarNear}");
        if(isCarNear && Input.GetKey(KeyCode.E))
        {
            ToggleDoor();
        }
        if (isCarNear)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log($"�� ���󿩺�: {currentDoor.GetIsOpen()}");
                if (currentDoor != null && currentDoor.GetIsOpen()) // �������� ���� ž��
                {
                    EnterCar();
                }
                else
                {
                    Debug.Log("Door is not open");
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            ToggleTrunk();
        }
        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    GameManager.Instance.StartTimer();
        //}
    }
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            isCarNear = true;
        }
        // Ʈ���� ���� �ȿ� ������ DoorInteraction ������Ʈ ��������
        DoorInteraction doorInteraction = other.gameObject.GetComponentInChildren<DoorInteraction>();
        if (doorInteraction != null)
        {
            currentDoor = doorInteraction;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            isCarNear = false;
        }
        // Ʈ���� ���� ����� currentDoor �ʱ�ȭ
        DoorInteraction doorInteraction = other.gameObject.GetComponentInChildren<DoorInteraction>();
        if (doorInteraction != null && doorInteraction == currentDoor)
        {
            currentDoor = null;
        }
    }

    private void ToggleDoor()
    {
        RaycastHit hit;
        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 50f, interactableLayer))
        {
            Debug.Log("�¾Ҵ�");
            if (hit.transform.CompareTag("Door"))
            {
                DoorInteraction door = hit.transform.GetComponent<DoorInteraction>();
                if (door != null)
                {
                    
                    door.ToggleDoor();
                }
            }
        }
          

    }
    private void EnterCar()
    {
        innerPlayer.SetActive(true); // �� ��Ʈ ���� innerPlayer Ȱ��ȭ
        gameObject.SetActive(false); // ���� �÷��̾� ��Ȱ��ȭ
    }
   
    private void ToggleTrunk()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 20f, interactableLayer))
        {
            if (hit.transform.CompareTag("Trunk"))
            {
                DoorInteraction door = hit.transform.GetComponent<DoorInteraction>();
                if (door != null)
                {
                    door.ToggleDoor();
                }
            }
        }

    }
}
