using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyState {
    Idle,
    ChooseDir,
    Moving,
}
enum directions {
    Left,
    Right,
    Up,
    Down,
}
public class Enemy : MonoBehaviour {


    public float speed = 5;

    private LayerMask levelMask;
    private EnemyState state;
    private directions dir;
    private Rigidbody rBody;
    private float timer;
    
    private void Start()
    {
        levelMask = (1 << 8 | 1 << 9);
        state = EnemyState.Idle;
        rBody = GetComponent<Rigidbody>();
        timer = 0;        
    }

    private void Update()
    {
        EnemyStateMachine();
    }
    /// <summary>
    /// Contador entre movimiento y movimiento
    /// </summary>
    private void TimerCount()
    {
        if (timer > 1)
        {
            if (state == EnemyState.Idle)
            {
                state = EnemyState.ChooseDir;
            }
            else if (state == EnemyState.Moving)
                state = EnemyState.Idle;
            timer = 0;
        }
        else
            timer += Time.deltaTime;
    }
    /// <summary>
    /// Funcion para que el enemy elija que camino ir sin, sin chocar con una pared.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private bool calculateDir(Vector3 direction)
    {        
            RaycastHit hit;
            Physics.Raycast(transform.position, direction, out hit, 1, levelMask);
        if (!hit.collider)
        {            
            return true;
        }
        else
        {            
            return false;
        }
        
    }
    /// <summary>
    /// Centra posición en x & z
    /// </summary>
    /// <returns></returns>
    private Vector3 CenteredPos()
    {
        return new Vector3(Mathf.RoundToInt(transform.position.x),
                            transform.position.y, 
                            Mathf.RoundToInt(transform.position.z));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy1")
        {
            if (dir == directions.Up)
                dir = directions.Down;
            else if (dir == directions.Down)
                dir = directions.Up;
            else if (dir == directions.Left)
                dir = directions.Right;
            else if (dir == directions.Right)
                dir = directions.Left;
            state = EnemyState.Moving;            
        }
    }
    /// <summary>
    /// Cuenta de 3 estados:
    /// <para> Idle: Centra posicion y cambia a ChooseDir</para>
    /// <para> ChooseDir: Dependiendo de un Random que elije una direccion, calcula mediante raycast
    /// si puede mover a esa direccion, si es así cambia a Mooving, sino vuelve a calcular otra dirección</para>
    /// <para> Mooving: se mueve 1 posición hacia la dirección elegida</para>
    /// </summary>
    private void EnemyStateMachine()
    {
        switch (state)
        {
            case EnemyState.Idle:
                rBody.velocity = Vector3.zero;
                transform.position = CenteredPos();
                state = EnemyState.ChooseDir;
                break;
            case EnemyState.ChooseDir:
                switch (Random.Range(0, 4))
                {
                    case 0:
                        if (calculateDir(Vector3.forward))
                        {
                            dir = directions.Up;
                            state = EnemyState.Moving;
                        }
                        else
                            state = EnemyState.ChooseDir;
                        break;
                    case 1:
                        if (calculateDir(Vector3.back))
                        {
                            dir = directions.Down;
                            state = EnemyState.Moving;
                        }
                        else
                            state = EnemyState.ChooseDir;
                        break;
                    case 2:
                        if (calculateDir(Vector3.right))
                        {
                            dir = directions.Right;
                            state = EnemyState.Moving;
                        }
                        else
                            state = EnemyState.ChooseDir;
                        break;
                    case 3:
                        if (calculateDir(Vector3.left))
                        {
                            dir = directions.Left;
                            state = EnemyState.Moving;
                        }
                        else
                            state = EnemyState.ChooseDir;
                        break;
                }
                break;
            case (EnemyState.Moving):
                TimerCount();
                switch (dir)
                {
                    case directions.Up:
                        rBody.velocity = Vector3.forward;
                        break;
                    case directions.Down:
                        rBody.velocity = Vector3.back;
                        break;
                    case directions.Right:
                        rBody.velocity = Vector3.right;
                        break;
                    case directions.Left:
                        rBody.velocity = Vector3.left;
                        break;
                }
                break;
        }
    }
}