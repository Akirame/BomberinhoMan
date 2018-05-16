using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInstancer : MonoBehaviour
{

    public GameObject whiteCube;
    public GameObject wall;
    public GameObject brownCube;
    private GameObject cubesGroup;
    private int[,] mapMatrix;

    void Start()
    {
        cubesGroup = new GameObject();
        cubesGroup.transform.parent = transform;
        cubesGroup.name = "Cubes";
        Instantiate(wall, new Vector3(11.5f, 0, 12), Quaternion.identity, transform);
        mapMatrix = new int[25, 25];
        for (int i = 0; i < mapMatrix.GetLength(0); i++)
            for (int j = 0; j < mapMatrix.GetLength(1); j++)
            {
                if (i != 12 && j != 12)
                {
                    if (i != 0 && i != mapMatrix.GetLength(0) - 1 && j != 0 && j != mapMatrix.GetLength(1))
                    {
                        if (i % 2 != 0 && j % 2 != 0)
                        {
                            Instantiate(whiteCube, new Vector3(i * 1, 0, j * 1), Quaternion.identity, cubesGroup.transform);
                        }
                        else
                        {
                            if (Random.value > 0.7)
                                Instantiate(brownCube, new Vector3(i * 1, 0, j * 1), Quaternion.identity, cubesGroup.transform);
                        }
                    }
                    else
                    {
                        if (Random.value > 0.7)
                            Instantiate(brownCube, new Vector3(i * 1, 0, j * 1), Quaternion.identity, cubesGroup.transform);
                    }
                }
            }

    }
}
