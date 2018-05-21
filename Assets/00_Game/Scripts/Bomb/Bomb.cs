using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public GameObject explosionPrefab;
    public LayerMask levelMask;    
    public GameObject BombCollider;
    public float timerExplode = 3f;
    private bool exploded;

    /// <summary>
    /// set de layermask y llamado a invocacion de la explosion, con un tiempo.
    /// </summary>
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
    /// <summary>
    /// Instancia una bomba en el centro del player, luego calcula mediante 4 raycast hacia arriba,abajo,derecha,izquierda
    /// si es que puede instanciar una explosion dependiendo del rango, si choca contra un cubo blanco deja de instanciar,
    /// si choca contra un cubo marron instancia una mas, y luego se detiene.
    /// activa el destroy luego de un tiempo, que es acorde al tiempo que dura la explosion y activa un bool para que no se
    /// desencadenen explosiones en cadena si esta bomba ya explotó
    /// </summary>
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
    /// <summary>
    /// Raycast para calcular el rango de las explosiones
    /// </summary>
    /// <param name="direction"></param>
    private void calculateExplosion(Vector3 direction)
    {
        bool touched = false;

        for (int i = 1; i < Game.Get().GetBombRange(); i++)
        {            
            RaycastHit hit;
            Physics.Raycast(transform.position, direction, out hit, i, levelMask);
            if (!hit.collider && !touched)
            {
                if (explosionPrefab)
                    Instantiate(explosionPrefab, transform.position + (i * direction), Quaternion.identity, PlayerController.Get().GetBombGroupTransf());
            }
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("MapBrown") && !touched)
            {
                Instantiate(explosionPrefab, transform.position + (i * direction), Quaternion.identity, PlayerController.Get().GetBombGroupTransf());
                touched = true;
            }
            else
                touched = true;
        }
    }
    /// <summary>
    /// Si una explosion activa el trigger, esta cancela la invocacion de la explosion y explota inmediatamente.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (!exploded && other.gameObject.tag == "Explosion")
        {
            CancelInvoke("Explode");
            Explode();
        }
    }
    /// <summary>
    /// si el player se aleja vuelve a colisionar
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        BombCollider.SetActive(true);
    }
}
