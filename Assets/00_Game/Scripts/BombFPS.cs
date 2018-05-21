using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFPS : MonoBehaviour
{
    public GameObject Bomb;
    private Vector3 startPos = new Vector3(12, 0.2f, 12);
    private Vector3 vBombCentered;
    private GameObject bombGroup;
    private bool bombInPlace = false;

    private void Start()
    {
        transform.position = startPos;
        vBombCentered = Vector3.zero;
        InitBombGroup();
    }
    private void Update()
    {
        vBombCentered = CenterBombGrid();
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)) && Game.Get().BombCantOK() && !bombInPlace)
        {
            Instantiate(Bomb, vBombCentered, Bomb.transform.rotation, GetBombGroupTransf());
            bombInPlace = true;
        }
    }

    /// <summary>
    /// Devuelve un Vector3 centrado en la cuadrilla
    /// </summary>
    /// <returns></returns>
    private Vector3 CenterBombGrid()
    {
        return new Vector3(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.transform.position.z));
    }

    private void InitBombGroup()
    {
        bombGroup = new GameObject();
        bombGroup.name = "Bombs";
    }
    /// <summary>
    /// Al morir, reset de la posicion
    /// <para>Health - 1</para>
    /// <para>Re-instanciacion del grupo de bombas</para>
    /// <para>LLamado a reset a Game</para>
    /// </summary>
    public void OnDeath()
    {
        transform.position = startPos;
        Game.Get().PlayerDeath();
        Destroy(bombGroup);
        InitBombGroup();
        MapInstancer.Get().ResetMap();
    }
    public Transform GetBombGroupTransf()
    {
        return bombGroup.transform;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy1")
            OnDeath();
    }
    /// <summary>
    /// si salgo del rango de la bomba, se activa el bool que deja volver a colocar otra bomba
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bomb")
        {
            bombInPlace = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Explosion")
            OnDeath();
    }
}
