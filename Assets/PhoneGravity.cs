using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneGravity : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float gravityMagnitude;
    bool useGyro;
    Vector3 gravityDir;

    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            useGyro = true;
            Input.gyro.enabled = true;
        }
    }

    void Update()
    {
        var InputDir = useGyro ? Input.gyro.gravity : Input.acceleration;

        gravityDir = new Vector3(InputDir.x, InputDir.z, InputDir.y);
    }

    void FixedUpdate()
    {
        rb.AddForce(gravityDir * gravityMagnitude, ForceMode.Acceleration);
    }
}
