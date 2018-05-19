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
    public int CantEnemy = 1;
    private GameObject cubesGroup;
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
    /// <para>0:Libre
    /// 1:Cubo Blanco
    /// 2:Cubo Marron
    /// 3:Player
    /// </para>
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
        for (int i = 0; i < mapMatrix.GetLength(0); i++)
            for (int j = 0; j < mapMatrix.GetLength(1); j++)
            {
                if (mapMatrix[i, j] == 1)
                    Instantiate(whiteCube, new Vector3(i * 1, 0, j * 1), Quaternion.identity, cubesGroup.transform);
                else
                if (mapMatrix[i, j] == 2)
                    Instantiate(brownCube, new Vector3(i * 1, 0, j * 1), Quaternion.identity, cubesGroup.transform);
            }
        for (int i = 0; i < CantEnemy; i++)
        {
            int randomX = (Random.Range(0, mapMatrix.GetLength(0)));
            int randomZ = (Random.Range(0, mapMatrix.GetLength(1)));
            while (mapMatrix[randomX, randomZ] != 0 && mapMatrix[randomX, randomZ] != 3)
            {
                randomX = (Random.Range(0, mapMatrix.GetLength(0)));
                randomZ = (Random.Range(0, mapMatrix.GetLength(1)));
            }
                Instantiate(enemyPrefab, new Vector3( randomX * 1, 0,randomZ * 1), Quaternion.identity,enemiesGroup.transform);            
        }
    }
    private void InitGroups()
    {
        cubesGroup = new GameObject();
        cubesGroup.transform.parent = transform;
        cubesGroup.name = "Cubes";
        enemiesGroup = new GameObject();
        enemiesGroup.transform.parent = transform;
        enemiesGroup.name = "Enemies";
    }
    public void ResetMap()
    {
        Destroy(cubesGroup.gameObject);
        Destroy(enemiesGroup.gameObject);
        InitGroups();
        InstantiateOnMatrix();
    }
}
