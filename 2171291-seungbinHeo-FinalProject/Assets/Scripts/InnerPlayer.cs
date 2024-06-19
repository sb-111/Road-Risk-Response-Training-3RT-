using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class InnerPlayer : MonoBehaviour
{
    float mouseX;
    float mouseY;
    float cameraVerticalAngle;
    float cameraHorizontalAngle;
    [SerializeField] private float cameraVerticalMin;
    [SerializeField] private float cameraVerticalMax;
    [SerializeField] private float cameraHorizontalMin;
    [SerializeField] private float cameraHorizontalMax;
    [SerializeField] private float sensivity;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject outerPlayer;
    private void OnEnable()
    {
    }

    // Update is called once per frame
    void Update()
    {
        InputKey();
        Rotate();
        if (Input.GetKey(KeyCode.Q))
        {
            ExitCar();
        }
    }
    void InputKey()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
    }
    private void Rotate()
    {
        cameraVerticalAngle -= mouseY * sensivity * Time.deltaTime;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, cameraVerticalMin, cameraVerticalMax);

        cameraHorizontalAngle += mouseX * sensivity * Time.deltaTime;
        cameraHorizontalAngle = Mathf.Clamp(cameraHorizontalAngle, cameraHorizontalMin, cameraHorizontalMax);

        camera.transform.localEulerAngles = new Vector3(cameraVerticalAngle, cameraHorizontalAngle, 0);
    }
    private void ExitCar()
    {
        outerPlayer.SetActive(true);
        gameObject.SetActive(false);
    }
}
