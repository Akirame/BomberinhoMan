using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed = 10;
    public float rotationSpeed = 10;
    public float angle;


    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal * speed, 0, vertical * speed);

        Vector3 lastPosition = transform.position;

        Vector3 newPosition = transform.position + movement * Time.deltaTime;

        float newAngleY = GetRealAngle(lastPosition, newPosition);

        Quaternion currentRotation = transform.rotation;
        Quaternion newRotation = Quaternion.Slerp(currentRotation, Quaternion.AngleAxis(newAngleY, Vector3.up), rotationSpeed * Time.deltaTime);

        transform.position = newPosition;
        if (horizontal != 0 || vertical != 0)
        {
            angle = newAngleY;
            transform.rotation = newRotation;
        }

    }

    float GetRealAngle(Vector3 from, Vector3 to)
    {
        Vector3 right = Vector3.right;

        Vector3 dir = to - from;

        float angleBtw = Vector3.Angle(right, dir);
        Vector3 cross = Vector3.Cross(right, dir);
        if (cross.y < 0)
        {
            angleBtw = 360 - angleBtw;
        }

        return angleBtw;
    }
}
