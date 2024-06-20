using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterPlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject innerPlayer; // 교체할 플레이어
    [SerializeField] private Camera playerCamera;
    bool isCarNear = false;
    [SerializeField] private LayerMask interactableLayer;

    private DoorInteraction currentDoor; // 현재 상호작용하는 문을 추적하기 위한 변수
    private void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward * 20f);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        Debug.Log($"차근처:{isCarNear}");
        if(isCarNear && Input.GetKey(KeyCode.E))
        {
            ToggleDoor();
        }
        if (isCarNear)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log($"문 개폐여부: {currentDoor.GetIsOpen()}");
                if (currentDoor != null && currentDoor.GetIsOpen()) // 문열렸을 때만 탑승
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
        // 트리거 영역 안에 있으면 DoorInteraction 컴포넌트 가져오기
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
        // 트리거 영역 벗어나면 currentDoor 초기화
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
            Debug.Log("맞았니");
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
        innerPlayer.SetActive(true); // 차 시트 하위 innerPlayer 활성화
        gameObject.SetActive(false); // 현재 플레이어 비활성화
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
