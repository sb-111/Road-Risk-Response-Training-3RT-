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

    [SerializeField] private GameObject phone;
    private bool[] isShotAvailable = new bool[4];
    private bool[] isShot = new bool[4];
    private int shotCount = 4;
    [SerializeField] private GameObject[] shotPositions;
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
        // 1��Ű - �� ���
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TogglePhone();
     
        }
        //// �� Ȱ������ && CŰ - �˸�â ���(�Ű�)
        //if (phone.activeSelf && Input.GetKeyDown(KeyCode.C))
        //{
        //    UIManager.Instance.SetNotification("119�� �Ű�(Y) / 112�� �Ű�(N)");
        //    UIManager.Instance.ToggleUINotification();
        //}
        //// �� Ȱ������ && �˸�â Ȱ��ȭ �� && YŰ - �Ű��ϰ� �˸�â ����
        //if (phone.activeSelf && UIManager.Instance.IsActiveNotification())
        //{
        //    if(Input.GetKeyDown(KeyCode.Y))
        //    {
        //        Call();
        //        UIManager.Instance.ToggleUINotification(); // ����
        //    }
        //    else if (Input.GetKeyDown(KeyCode.N))
        //    {
        //        UIManager.Instance.ToggleUINotification(); // ����
        //    }
        //}
        //// �� Ȱ������ && VŰ - �˸�â ���(���� �Կ�)
        //if(phone.activeSelf && Input.GetKeyDown(KeyCode.V))
        //{
        //    UIManager.Instance.SetNotification("���� �Կ��ϱ�");
        //    UIManager.Instance.ToggleUINotification();
        //}
        //if(phone.activeSelf && UIManager.Instance.IsActiveNotification())
        //{
        //    if (Input.GetKeyDown(KeyCode.Y))
        //    {
        //        Shot();
        //        UIManager.Instance.ToggleUINotification(); // ����
        //    }
        //    else if (Input.GetKeyDown(KeyCode.N))
        //    {
        //        UIManager.Instance.ToggleUINotification(); // ����
        //    }
        //}
        // �� Ȱ������ && CŰ - �˸�â ���(�Ű�)
        if (phone.activeSelf && Input.GetKeyDown(KeyCode.C))
        {
            UIManager.Instance.SetNotification("119�� �Ű�(Y) / 112�� �Ű�(N)", NotificationType.Call);
            UIManager.Instance.ToggleUINotification();
        }

        // �� Ȱ������ && VŰ - �˸�â ���(���� �Կ�)
        if (phone.activeSelf && Input.GetKeyDown(KeyCode.V))
        {
            UIManager.Instance.SetNotification("���� �Կ��ϱ�", NotificationType.Shot);
            UIManager.Instance.ToggleUINotification();
        }

        if (phone.activeSelf && UIManager.Instance.IsActiveNotification())
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                if (UIManager.Instance.GetCurrentNotificationType() == NotificationType.Call)
                {
                    Call();
                }
                else if (UIManager.Instance.GetCurrentNotificationType() == NotificationType.Shot)
                {
                    Shot();
                }
                UIManager.Instance.ToggleUINotification(); // ����
                UIManager.Instance.ClearNotificationType();
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                UIManager.Instance.ToggleUINotification(); // ����
                UIManager.Instance.ClearNotificationType();
            }
        }
        if (CheckAllShot()) // ������ ��� ����ٸ�
        {
            TaskManager.Instance.CompleteTask(5, "���Ÿ� ��� Ȯ���߽��ϴ�!");
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
        if (other.gameObject.CompareTag("Shot1")) isShotAvailable[0] = true;
        if (other.gameObject.CompareTag("Shot2")) isShotAvailable[1] = true;
        if (other.gameObject.CompareTag("Shot3")) isShotAvailable[2] = true;
        if (other.gameObject.CompareTag("Shot4")) isShotAvailable[3] = true;
        
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
        if (other.gameObject.CompareTag("Shot1")) isShotAvailable[0] = false;
        if (other.gameObject.CompareTag("Shot2")) isShotAvailable[1] = false;
        if (other.gameObject.CompareTag("Shot3")) isShotAvailable[2] = false;
        if (other.gameObject.CompareTag("Shot4")) isShotAvailable[3] = false;
    }
    private void TogglePhone()
    {
        phone.SetActive(!phone.activeSelf);
        if(UIManager.Instance.IsActiveNotification())
        {
            UIManager.Instance.ToggleUINotification();
        }
    }
    private void Call() // �Ű� ��ȭ
    {
        if (GameManager.Instance.GetSituation() == Situation.Accident && TaskManager.Instance.IsAddedAllTasks())
        {
            TaskManager.Instance.CompleteTask(4, "�Ű� �Ϸ�!");
        }
    }
    private void Shot() // ���� �Կ�
    {
        if (GameManager.Instance.GetSituation() == Situation.Accident && TaskManager.Instance.IsAddedAllTasks())
        {
            for(int i= 0; i < 4; i++)
            {
                if (isShotAvailable[i])
                {
                    isShot[i] = true;
                    shotCount -= 1;
                    shotPositions[i].SetActive(false); // ���� shotPosition ��Ȱ��ȭ
                    if(shotCount > 0)
                        UIManager.Instance.ShowShortPanel($"���� �Կ� ����! {shotCount} �� ���ҽ��ϴ�.");
                    break;
                }
            }
        }
    }
    private bool CheckAllShot()
    {
        for(int i= 0; i < 4; i++)
        {
            if (isShot[i] == false)
            {
                return false;
            }
        }
        return true;
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
