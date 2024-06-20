using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class OuterPlayerMove : MonoBehaviour
{
    CharacterController controller;
    private Vector3 velocity; // 이동 벡터
    [SerializeField] private float moveSpeed = 10.0f; // 플레이어 속력
    private float mouseX;
    private float mouseY;
    [SerializeField] private float sensivity = 10f;
    public GameObject cameraArm;            // 카메라암
    private float cameraVerticalAngle = 0f; // 카메라의 수직 회전 각도 추적
    [SerializeField] private float cameraVerticalMin;
    [SerializeField] private float cameraVerticalMax;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }
    private void Move()
    {
        float gravity = 20.0f; // rigidbody 없으니 대안으로 필요

        if (controller.isGrounded) // 땅에 있는지 검사
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 moveHorizontal = transform.right * h;
            Vector3 moveVertical = transform.forward * v;

            velocity = moveHorizontal + moveVertical;
            velocity = velocity.normalized;

            //velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //velocity = velocity.normalized; // 크기: 0 OR 1

            //// 달린다면 크기는 0 OR 2
            //if (Input.GetKey(KeyCode.LeftShift) && !isSat)
            //{
            //    velocity *= 2.0f;
            //}

            // Move 애니메이션 설정
            //animator.SetFloat("MoveSpeed", velocity.magnitude); // 0 OR 1 OR 2

            //if (Input.GetButtonDown("Sit")) // 앉거나
            //{
            //    // TODO: 카메라 시야 내리기

            //    // Sit 애니메이션 설정
            //    isSat = true;

            //}
            //else if (Input.GetButtonUp("Sit"))
            //{
            //    // TODO: 카메라 시야 내리기

            //    isSat = false;
            //}
            // 어떤 이동키라도 눌렸다면
            //if (velocity.magnitude > 0.5)
            //{
            //    // 이동방향 쳐다보기
            //    transform.LookAt(transform.position + velocity);
            //}
        }

        // 중력 적용
        velocity.y -= gravity * Time.deltaTime;
        // 실제 이동
        controller.Move(velocity * moveSpeed * Time.deltaTime);

    }
    void Rotate()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        var rotVec = Vector3.up * mouseX;
        transform.Rotate(rotVec * sensivity * Time.deltaTime); // 플레이어 수평 회전

        // 카메라 수직 회전 처리
        cameraVerticalAngle -= mouseY * sensivity * Time.deltaTime;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, cameraVerticalMin, cameraVerticalMax);

        cameraArm.transform.localEulerAngles = new Vector3(cameraVerticalAngle, 0, 0);
    }
}
