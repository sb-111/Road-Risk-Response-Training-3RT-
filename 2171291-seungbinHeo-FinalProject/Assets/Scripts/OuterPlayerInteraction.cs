using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterPlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject innerPlayer;
    [SerializeField] private Camera playerCamra;
    bool isCarNear = false;
    [SerializeField] private LayerMask interactableLayer;

    private DoorInteraction currentDoor; // ���� ��ȣ�ۿ��ϴ� ���� �����ϱ� ���� ����
    private void Update()
    {
        Ray ray = new Ray(playerCamra.transform.position, playerCamra.transform.forward * 10f);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        if(isCarNear && Input.GetKey(KeyCode.E))
        {
            OpenDoor();
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

    private void OpenDoor()
    {
        RaycastHit hit;
        if(Physics.Raycast(playerCamra.transform.position, playerCamra.transform.forward, out hit, 10f, interactableLayer))
        {
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
        if (Physics.Raycast(playerCamra.transform.position, playerCamra.transform.forward, out hit, 10f, interactableLayer))
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
