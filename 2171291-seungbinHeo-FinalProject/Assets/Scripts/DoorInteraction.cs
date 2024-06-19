using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public enum Axis { X, Y, Z };
    private bool isOpen = false;
    [SerializeField] private float openAngle;
    [SerializeField] private float closeAngle;
    [SerializeField] private float smooth;
    [SerializeField] private Axis axis; // 회전할 축 지정
    private Vector3 rotDir;
    private Quaternion openRotation;
    private Quaternion closeRotation;
    // Start is called before the first frame update
    void Start()
    {
        switch (axis)
        {
            case Axis.X:
                rotDir = Vector3.right;
                break;
            case Axis.Y:
                rotDir = Vector3.up;
                break;
            case Axis.Z:
                rotDir = Vector3.forward;
                break;
        }
        // 쿼터니언 회전값
        closeRotation = transform.localRotation;
        openRotation = Quaternion.Euler(transform.localEulerAngles + rotDir * openAngle);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleDoor()
    {
        isOpen = !isOpen;
        StopAllCoroutines();
        StartCoroutine(AnimateDoor(isOpen ? openRotation : closeRotation));
    }
    private IEnumerator AnimateDoor(Quaternion targetRoation)
    {
        while(Quaternion.Angle(transform.localRotation, targetRoation) > 0.01f)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRoation, smooth * Time.deltaTime); ;
            yield return null;
        }
        transform.localRotation = targetRoation;
    }
    public bool GetIsOpen()
    {
        return isOpen;
    }
}
