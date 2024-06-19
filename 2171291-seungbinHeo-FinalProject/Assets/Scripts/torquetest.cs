using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torquetest : MonoBehaviour
{
    float turnSpeed = 10f;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //var input = Input.GetAxis("Horizontal");
        var input = 30f;
        float turn = input * turnSpeed * Time.deltaTime;
        rb.AddTorque(transform.up * turn);
    }
}
