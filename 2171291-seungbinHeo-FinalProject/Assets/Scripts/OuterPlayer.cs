using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterPlayer : MonoBehaviour
{
    float vertical;
    float horizontal;
    float mouseX;
    float mouseY;

    [SerializeField] private float speed = 3f;
    [SerializeField] private float sensivity = 20f;

    public GameObject camera;
    private float cameraVerticalAngle = 0f; // 카메라의 수직 회전 각도 추적
    [SerializeField] private float cameraVerticalMin;
    [SerializeField] private float cameraVerticalMax;

    OuterPlayerInteraction carInteraction;
    // Start is called before the first frame update
    private void Awake()
    {
        carInteraction = GetComponent<OuterPlayerInteraction>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputKey();
        Move();
        Rotate();
    }
    void InputKey()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }
    
    void Move()
    {
        var moveVec = new Vector3(horizontal, 0, vertical);
        transform.Translate(moveVec * speed * Time.deltaTime);
    }
    void Rotate()
    {
        var rotVec = Vector3.up * mouseX;
        transform.Rotate(rotVec * sensivity * Time.deltaTime); // 플레이어 수평 회전

        // 카메라 수직 회전 처리
        cameraVerticalAngle -= mouseY * sensivity * Time.deltaTime;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, cameraVerticalMin, cameraVerticalMax);

        camera.transform.localEulerAngles = new Vector3(cameraVerticalAngle, 0, 0);
    }
    
}
