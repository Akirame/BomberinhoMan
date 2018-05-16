using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float velocidadCamara = 5f;
    public Vector3 vOffset;
    public Vector3 rOffset;

    private void FixedUpdate()
    {
        Vector3 posicionCamara = player.transform.position + vOffset;
        transform.position = posicionCamara;
        transform.rotation = Quaternion.Euler(rOffset);
    }
    
}
