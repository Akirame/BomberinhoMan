using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
            
    private void Update()
    {
        if (transform.childCount == 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DestroyableWall")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Enemy1")
        {
            Destroy(other.gameObject);
            Game.Get().SetScore(100);
            Game.Get().EnemyDeath();
        }
        if (other.gameObject.tag == "Player")
        {
            PlayerController.Get().OnDeath();
        }
        if (other.gameObject.tag == "PowerUp")
        {
            Destroy(other.gameObject);
        }
    }
}
