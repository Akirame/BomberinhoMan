using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInstancer : MonoBehaviour
{
    private static MapInstancer instance;

    public GameObject whiteCube;
    public GameObject wall;
    public GameObject brownCube;
    public GameObject enemyPrefab;
    public GameObject door;
    private GameObject cubesGroup;
    private GameObject brownCubesGroup;
    private GameObject whiteCubesGroup;
    private GameObject enemiesGroup;
    private int[,] mapMatrix;

    public static MapInstancer Get()
    {
        return instance;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        InitGroups();
        Instantiate(wall, new Vector3(11.5f, 0, 12), Quaternion.identity, transform);
        mapMatrix = new int[25, 25];
        InitMatrix();
        SetMatrix();
        InstantiateOnMatrix();
   
    }
    /// <summary>
    /// Inicializacion de la matriz en 0
    /// </summary>
    private void InitMatrix()
    {                
        for (int i = 0; i < mapMatrix.GetLength(0); i++)
            for (int j = 0; j < mapMatrix.GetLength(1); j++)
                mapMatrix[i, j] = 0;
    }
    /// <summary>
    /// Darle valor a la matriz dependiendo del tipo de bloque a instanciar
    /// <para>0: Libre</para>
    /// <para>1: Cubo Blanco</para>
    /// <para>2: Cubo Marron</para>
    /// <para>3: Player</para>
    /// </summary>
    private void SetMatrix()
    {
        for (int i = 0; i < mapMatrix.GetLength(0); i++)
            for (int j = 0; j < mapMatrix.GetLength(1); j++)
            {
                if (i != 12 && j != 12)
                {
                    if (i != 0 && i != mapMatrix.GetLength(0) - 1 && j != 0 && j != mapMatrix.GetLength(1))
                    {
                        if (i % 2 != 0 && j % 2 != 0)
                        {
                            mapMatrix[i, j] = 1;
                        }
                        else
                        {
                            if (Random.value > 0.7)
                                mapMatrix[i, j] = 2;
                        }
                    }
                    else
                    {
                        if (Random.value > 0.7)
                            mapMatrix[i, j] = 2;
                    }
                }
            }
        // posición inicial del jugador
        mapMatrix[12, 12] = 3;
    }
    /// <summary>
    /// A partir del valor de la matriz, instancia diferente tipo de objeto
    /// </summary>
    private void InstantiateOnMatrix()
    {
        int randomX = 0;
        int randomZ = 0;
        //Cubos
        for (int i = 0; i < mapMatrix.GetLength(0); i++)
            for (int j = 0; j < mapMatrix.GetLength(1); j++)
            {
                if (mapMatrix[i, j] == 1)
                    Instantiate(whiteCube, new Vector3(i * 1, 0, j * 1), Quaternion.identity, whiteCubesGroup.transform);
                else
                if (mapMatrix[i, j] == 2)
                    Instantiate(brownCube, new Vector3(i * 1, 0, j * 1), Quaternion.identity, brownCubesGroup.transform);
            }
        // Enemigos
        for (int i = 0; i < Game.Get().GetCantEnemy(); i++)
        {
             randomX = (Random.Range(0, mapMatrix.GetLength(0)));
             randomZ = (Random.Range(0, mapMatrix.GetLength(1)));
            while (mapMatrix[randomX, randomZ] != 0 && mapMatrix[randomX, randomZ] != 3)
            {
                randomX = (Random.Range(0, mapMatrix.GetLength(0)));
                randomZ = (Random.Range(0, mapMatrix.GetLength(1)));
            }
                Instantiate(enemyPrefab, new Vector3( randomX * 1, 0,randomZ * 1), Quaternion.identity,enemiesGroup.transform);            
        }
        //Puerta
        randomX = (Random.Range(0, mapMatrix.GetLength(0)));
        randomZ= (Random.Range(0, mapMatrix.GetLength(1)));
        while (mapMatrix[randomX, randomZ] != 2)
        {
            randomX = (Random.Range(0, mapMatrix.GetLength(0)));
            randomZ = (Random.Range(0, mapMatrix.GetLength(1)));
        }
        Instantiate(door, new Vector3(randomX * 1, -0.5f, randomZ * 1), door.transform.rotation,cubesGroup.transform);
    }
    /// <summary>
    /// Instanciacion de los grupos para mantener orden dentro del MapInstancer
    /// </summary>
    private void InitGroups()
    {
        cubesGroup = new GameObject();
        cubesGroup.transform.parent = transform;
        cubesGroup.name = "Cubes";
        enemiesGroup = new GameObject();
        enemiesGroup.transform.parent = transform;
        enemiesGroup.name = "Enemies";
        brownCubesGroup = new GameObject();
        brownCubesGroup.transform.parent = cubesGroup.transform;
        brownCubesGroup.name = "BrownCubes";
        whiteCubesGroup = new GameObject();
        whiteCubesGroup.transform.parent = cubesGroup.transform;
        whiteCubesGroup.name = "WhiteCubes";
    }
    /// <summary>
    /// Re-Instanciacion del mapa y los enemigos
    /// </summary>
    public void ResetMap()
    {
        Destroy(cubesGroup.gameObject);
        Destroy(enemiesGroup.gameObject);
        InitGroups();
        InstantiateOnMatrix();
    }
}
