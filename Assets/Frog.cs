using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frog : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRend;
    [SerializeField] Animator animator;
    [SerializeField] Joystick joy;
    [SerializeField] Source source;
    [SerializeField, Range(0, 2)] float speed;

    enum Source
    {
        Keyboard,
        Joystick,
        Accelerometer,
        Gyroscope
    }

    private void Start()
    {
        Debug.Log("Accelerometer: " + SystemInfo.supportsAccelerometer);
        Debug.Log("Gyroscope: " + SystemInfo.supportsGyroscope);
        Input.gyro.enabled = true;
    }

    private void Update()
    {
        Vector2 moveDir = Vector2.zero;
        switch (source)
        {
            case Source.Keyboard:
                moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                Debug.Log(moveDir);
                break;
            case Source.Joystick:
                moveDir = joy.Direction;
                Debug.Log(moveDir);
                break;
            case Source.Accelerometer:
                moveDir = (Vector2)Input.acceleration;
                Debug.Log(Input.acceleration);
                break;
            case Source.Gyroscope:
                moveDir = (Vector2)Input.gyro.gravity;
                // Debug.Log(moveDir);
                // Debug.Log(Input.gyro.gravity);
                break;
            default:
                break;
        }

        // var moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // var moveDir = joy.Direction;

        if (Input.gyro.rotationRate.magnitude > 10)
        {
            Debug.Log("Shake");
        }

        this.transform.Translate(moveDir * Time.deltaTime * speed);
        if (moveDir.x > 0)
            spriteRend.flipX = false;
        else if (moveDir.x < 0)
            spriteRend.flipX = true;

        animator.SetBool("IsMoving", moveDir != Vector2.zero);
    }
}
