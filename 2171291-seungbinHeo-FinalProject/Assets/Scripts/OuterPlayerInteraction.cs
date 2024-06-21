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

    [SerializeField] private GameObject phone;
    private bool[] isShotAvailable = new bool[4];
    private bool[] isShot = new bool[4];
    private int shotCount = 4;
    [SerializeField] private GameObject[] shotPositions;
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
        // 1번키 - 폰 토글
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TogglePhone();
     
        }
        //// 폰 활성상태 && C키 - 알림창 토글(신고)
        //if (phone.activeSelf && Input.GetKeyDown(KeyCode.C))
        //{
        //    UIManager.Instance.SetNotification("119에 신고(Y) / 112에 신고(N)");
        //    UIManager.Instance.ToggleUINotification();
        //}
        //// 폰 활성상태 && 알림창 활성화 됨 && Y키 - 신고하고 알림창 끄기
        //if (phone.activeSelf && UIManager.Instance.IsActiveNotification())
        //{
        //    if(Input.GetKeyDown(KeyCode.Y))
        //    {
        //        Call();
        //        UIManager.Instance.ToggleUINotification(); // 끄기
        //    }
        //    else if (Input.GetKeyDown(KeyCode.N))
        //    {
        //        UIManager.Instance.ToggleUINotification(); // 끄기
        //    }
        //}
        //// 폰 활성상태 && V키 - 알림창 토글(사진 촬영)
        //if(phone.activeSelf && Input.GetKeyDown(KeyCode.V))
        //{
        //    UIManager.Instance.SetNotification("사진 촬영하기");
        //    UIManager.Instance.ToggleUINotification();
        //}
        //if(phone.activeSelf && UIManager.Instance.IsActiveNotification())
        //{
        //    if (Input.GetKeyDown(KeyCode.Y))
        //    {
        //        Shot();
        //        UIManager.Instance.ToggleUINotification(); // 끄기
        //    }
        //    else if (Input.GetKeyDown(KeyCode.N))
        //    {
        //        UIManager.Instance.ToggleUINotification(); // 끄기
        //    }
        //}
        // 폰 활성상태 && C키 - 알림창 토글(신고)
        if (phone.activeSelf && Input.GetKeyDown(KeyCode.C))
        {
            UIManager.Instance.SetNotification("119에 신고(Y) / 112에 신고(N)", NotificationType.Call);
            UIManager.Instance.ToggleUINotification();
        }

        // 폰 활성상태 && V키 - 알림창 토글(사진 촬영)
        if (phone.activeSelf && Input.GetKeyDown(KeyCode.V))
        {
            UIManager.Instance.SetNotification("사진 촬영하기", NotificationType.Shot);
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
                UIManager.Instance.ToggleUINotification(); // 끄기
                UIManager.Instance.ClearNotificationType();
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                UIManager.Instance.ToggleUINotification(); // 끄기
                UIManager.Instance.ClearNotificationType();
            }
        }
        if (CheckAllShot()) // 사진을 모두 찍었다면
        {
            TaskManager.Instance.CompleteTask(5, "증거를 모두 확보했습니다!");
        }
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
        // 트리거 영역 벗어나면 currentDoor 초기화
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
    private void Call() // 신고 전화
    {
        if (GameManager.Instance.GetSituation() == Situation.Accident && TaskManager.Instance.IsAddedAllTasks())
        {
            TaskManager.Instance.CompleteTask(4, "신고 완료!");
        }
    }
    private void Shot() // 사진 촬영
    {
        if (GameManager.Instance.GetSituation() == Situation.Accident && TaskManager.Instance.IsAddedAllTasks())
        {
            for(int i= 0; i < 4; i++)
            {
                if (isShotAvailable[i])
                {
                    isShot[i] = true;
                    shotCount -= 1;
                    shotPositions[i].SetActive(false); // 차의 shotPosition 비활성화
                    if(shotCount > 0)
                        UIManager.Instance.ShowShortPanel($"사진 촬영 성공! {shotCount} 장 남았습니다.");
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
