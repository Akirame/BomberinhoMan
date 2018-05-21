using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject FpsCamera;
    public GameObject BombermanCamera;
    public GameObject Player;
    private bool fpsON = false;
    private void Start()
    {
        BombermanCamera.SetActive(true);
        Player.SetActive(true);
        FpsCamera.SetActive(false);        
    }
    private void Update()
    {
        if (fpsON)
            Player.transform.position = FpsCamera.transform.position;
        else
        {
            FpsCamera.transform.position = Player.transform.position;            
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && fpsON)
        {
            BombermanCamera.SetActive(true);
            Player.SetActive(true);
            FpsCamera.SetActive(false);
            fpsON = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !fpsON)
        {
            BombermanCamera.SetActive(false);
            Player.SetActive(false);
            FpsCamera.SetActive(true);
            fpsON = true;            
        }
    }
}
