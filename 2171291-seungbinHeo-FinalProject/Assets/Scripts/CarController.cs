using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class CarController : MonoBehaviour
{
    private Rigidbody rb;

    private bool emergencyLightsOn = false; // 비상등 여부
    private bool parkingBrakeOn = false;    // 사이드브레이크 여부
    private bool isBraking = false;         // 브레이크 중인지 여부

    public float accelerationForce = 10f;  // 가속력
    public float decelerationForce = 10f;  // 감속력
    public float maxSpeed = 50f;           // 최대 속도
    public float turnSpeed = 10f;          // 회전 속도

    private float currentSpeed = 0f;

    [SerializeField] private Transform startPosition;

    private PlayableDirector pd;
    [SerializeField] private TimelineAsset[] ta;

    private bool isCutCompleted = false;

    [SerializeField] private GameObject[] shotPositions;
    private void Start()
    {
        pd = GetComponent<PlayableDirector>();
        pd.stopped += OnCutsceneStopped; // 컷신이 멈췄을 때 호출될 이벤트 핸들러 등록
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
        //    // 주차 브레이크 X && 최대 속도보다 낮을 때
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
        //    rb.velocity *= 0.9f; // 브레이크 적용
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
        // 브레이크 중일 때 감소
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
            // 사이드 브레이크 해제 시 회전 가능
            if (!parkingBrakeOn)
            {
                Debug.Log($"회전값:{input}");
                //float turn = input * turnSpeed * Time.deltaTime;
                //rb.AddTorque(transform.up * turn);
                float turn = input * turnSpeed * Time.deltaTime;
                transform.Rotate(0, turn, 0);
            }
        }
    }

    public void ToggleEmergencyLights()
    {
        // 비상등 토글
        emergencyLightsOn = !emergencyLightsOn;
        if (GameManager.Instance.GetSituation() == Situation.Accident && TaskManager.Instance.IsAddedAllTasks())
        {
            if (emergencyLightsOn)
                TaskManager.Instance.CompleteTask(2, "비상등 점등 완료!");
        }
        Debug.Log("Emergency lights " + (emergencyLightsOn ? "On" : "Off"));
    }

    public void ToggleParkingBrake()
    {
        // 사이드브레이크 토글
        parkingBrakeOn = !parkingBrakeOn;
        if(GameManager.Instance.GetSituation() == Situation.Accident && TaskManager.Instance.IsAddedAllTasks())
        {
            if(parkingBrakeOn)
                TaskManager.Instance.CompleteTask(1, "정차 완료!");
        }
        if (parkingBrakeOn) // 사이드브레이크 시
        {
            // 위치와 회전 모두 고정
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
        else // 사이드브레이크 아닐 때
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
                TaskManager.Instance.CompleteTask(0, "여행 시작 완료!");
            }
            if (other.gameObject.CompareTag("Accident"))
            {
                Debug.Log("컷씬 재생");
                
                // 사고 발생 컷씬 보여줌
                other.gameObject.SetActive(false);
                pd.Play(ta[0]);

            }
        }
    }
    // 타임라인 후 설정
    private void OnCutsceneStopped(PlayableDirector director)
    {
        // 컷신이 종료된 후 TaskManager를 통해 새로운 Task를 추가
        Debug.Log("컷신 종료, 새로운 Task 추가");
        TaskManager.Instance.AddTask("정차", "무슨일이지? 일단 정차하자.", 10, "사고 후에 바로 정차하지 않음", 10f);
        TaskManager.Instance.AddTask("비상등", "비상등을 점멸해야겠어..", 10, "비상등을 점멸하지 않음", 15f);
        TaskManager.Instance.AddTask("상황 파악", "차에서 내려 상황을 파악해야겠어.", 10, "상황을 파악하지 않음", 20f);
        TaskManager.Instance.AddTask("신고", "어디에 전화를 해야하지?", 10, "신고를 이행하지 않음", 70f);
        TaskManager.Instance.AddTask("증거 수집", "증거 수집을 해야할 것 같아..", 10, "증거를 수집하지 않음", 100f);
        TaskManager.Instance.AddTask("안전 확보", "차를 안전한 곳으로 움직일 수 있는지 확인하자..", 10, "안전한 곳으로 움직이지 않음", 60f);
        TaskManager.Instance.AddTask("트렁크", "트렁크를 열어야겠어..", 10, "트렁크를 열지 않음", 70f);
        TaskManager.Instance.AddTask("삼각대", "2차사고에 대비하자..", 10, "삼각대를 제대로 설치하지 않음", 100f);
        TaskManager.Instance.AddAllTasks(); // 모든 상황 추가 완료

        foreach(var shotPosition in shotPositions)
        {
            shotPosition.SetActive(true);
        }

        GameManager.Instance.StartTimer();
    }
}

/*
 TaskManager.Instance.AddTask("사고 발생", "정차해야 할 것 같다.");
                TaskManager.Instance.AddTask("사고 발생", "비상등을 키자");
                TaskManager.Instance.AddTask("사고 발생", )
 
 */