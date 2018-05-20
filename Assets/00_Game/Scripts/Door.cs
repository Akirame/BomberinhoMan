using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject OpenDoor;
    public GameObject ClosedDoor;
    private bool reveal = false;
    private void Update()
    {
        if (reveal)
            if (Game.Get().DoorOpen())
            {
                OpenDoor.SetActive(true);
                ClosedDoor.SetActive(false);
            }
            else
            {
                OpenDoor.SetActive(false);
                ClosedDoor.SetActive(true);
            }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Explosion"))
        {
            Debug.Log("HOLA");
            reveal = true;
        }
        if (other.gameObject.tag == "Player" && reveal)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
