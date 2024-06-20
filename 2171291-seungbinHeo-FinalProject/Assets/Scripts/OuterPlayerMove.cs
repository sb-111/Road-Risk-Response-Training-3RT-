using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class OuterPlayerMove : MonoBehaviour
{
    CharacterController controller;
    private Vector3 velocity; // �̵� ����
    [SerializeField] private float moveSpeed = 10.0f; // �÷��̾� �ӷ�
    private float mouseX;
    private float mouseY;
    [SerializeField] private float sensivity = 10f;
    public GameObject cameraArm;            // ī�޶��
    private float cameraVerticalAngle = 0f; // ī�޶��� ���� ȸ�� ���� ����
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
        float gravity = 20.0f; // rigidbody ������ ������� �ʿ�

        if (controller.isGrounded) // ���� �ִ��� �˻�
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 moveHorizontal = transform.right * h;
            Vector3 moveVertical = transform.forward * v;

            velocity = moveHorizontal + moveVertical;
            velocity = velocity.normalized;

            //velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //velocity = velocity.normalized; // ũ��: 0 OR 1

            //// �޸��ٸ� ũ��� 0 OR 2
            //if (Input.GetKey(KeyCode.LeftShift) && !isSat)
            //{
            //    velocity *= 2.0f;
            //}

            // Move �ִϸ��̼� ����
            //animator.SetFloat("MoveSpeed", velocity.magnitude); // 0 OR 1 OR 2

            //if (Input.GetButtonDown("Sit")) // �ɰų�
            //{
            //    // TODO: ī�޶� �þ� ������

            //    // Sit �ִϸ��̼� ����
            //    isSat = true;

            //}
            //else if (Input.GetButtonUp("Sit"))
            //{
            //    // TODO: ī�޶� �þ� ������

            //    isSat = false;
            //}
            // � �̵�Ű�� ���ȴٸ�
            //if (velocity.magnitude > 0.5)
            //{
            //    // �̵����� �Ĵٺ���
            //    transform.LookAt(transform.position + velocity);
            //}
        }

        // �߷� ����
        velocity.y -= gravity * Time.deltaTime;
        // ���� �̵�
        controller.Move(velocity * moveSpeed * Time.deltaTime);

    }
    void Rotate()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        var rotVec = Vector3.up * mouseX;
        transform.Rotate(rotVec * sensivity * Time.deltaTime); // �÷��̾� ���� ȸ��

        // ī�޶� ���� ȸ�� ó��
        cameraVerticalAngle -= mouseY * sensivity * Time.deltaTime;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, cameraVerticalMin, cameraVerticalMax);

        cameraArm.transform.localEulerAngles = new Vector3(cameraVerticalAngle, 0, 0);
    }
}
