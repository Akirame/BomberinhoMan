using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownCube : MonoBehaviour
{
    public GameObject bombPowerUp;
    public GameObject firePowerUp;
    public GameObject healthPowerUp;
    public float chancePowerUp = 0.85f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Explosion")
        {
            if (Random.value >= chancePowerUp) 
                if(Random.value >= chancePowerUp)
                    Instantiate(healthPowerUp, transform.position, healthPowerUp.transform.rotation);
            else
            switch (Random.Range(0, 2))
            {
                case 0:
                    Instantiate(bombPowerUp, transform.position, bombPowerUp.transform.rotation);
                    break;
                case 1:
                    Instantiate(firePowerUp, transform.position, firePowerUp.transform.rotation);
                    break;
            }
        }
    }
}
