using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float rotationSpeed = 10;
    public float angle;
    public GameObject Bomb;
    private Vector3 vBombCentered;

    private void Start()
    {
        vBombCentered = Vector3.zero;
    }
    private void Update()
    {
        movementAndRotation();
        vBombCentered = CenterBombGrid();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(Bomb, vBombCentered, Bomb.transform.rotation);
        }
    }

    private float GetRealAngle(Vector3 from, Vector3 to)
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
    private void movementAndRotation()
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
    Vector3 CenterBombGrid()
    {
        return new Vector3(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.transform.position.z));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Explosion")
            Debug.Log("HOLA");
        if (collision.gameObject.tag == "Explosion")
            Debug.Log("HOLA");
    }
}
