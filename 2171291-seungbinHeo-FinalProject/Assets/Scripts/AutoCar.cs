using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCar : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CarDelete"))
        {
            Destroy(gameObject);
        }
    }
}
