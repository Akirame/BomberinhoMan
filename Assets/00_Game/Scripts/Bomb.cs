using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public GameObject explosionPrefab;
    public LayerMask levelMask;
    public GameObject BombCollider;
    public float timerExplode = 3f;
    private bool exploded;

    private void Start()
    {        
        exploded = false;
        Invoke("Explode", timerExplode);
        Game.Get().BombCounterDOWN();
    }
    private void OnDestroy()
    {
        Game.Get().BombCounterUP();
    }
    private void Explode()
    {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity,PlayerController.Get().GetBombGroupTransf());
            calculateExplosion(Vector3.forward);
            calculateExplosion(Vector3.back);
            calculateExplosion(Vector3.right);
            calculateExplosion(Vector3.left);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            BombCollider.SetActive(false);
            exploded = true;            
            Destroy(this.gameObject, 0.5f);        
    }
    private void calculateExplosion(Vector3 direction)
    {
        bool touched = false;

        for (int i = 1; i < Game.Get().GetBombRange(); i++)
        {            
            RaycastHit hit;
            Physics.Raycast(transform.position, direction, out hit, i, levelMask);
            if (!hit.collider && !touched)
            {
                if(explosionPrefab)
                Instantiate(explosionPrefab, transform.position + (i * direction), Quaternion.identity,PlayerController.Get().GetBombGroupTransf());                
            }
            else
                touched = true;            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!exploded && other.gameObject.tag == "Explosion")
        {
            CancelInvoke("Explode");
            Explode();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        BombCollider.SetActive(true);
    }
}
