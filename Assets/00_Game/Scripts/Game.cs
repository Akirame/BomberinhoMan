using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    private static Game instance;

    public int bombRange = 2;
    public int bombMaxCant = 1;
    private int bombActualCant;
    private int Health;
    private int Score = 0;

    public static Game Get()
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

    private void Start()
    {
        Health = 2;
        bombActualCant = bombMaxCant;
    }
    private void Update()
    {
        if (Health <= 0)
            Debug.Log("HOLISWI");
    }

    public int GetBombRange()
    {
        return bombRange;
    }
    public bool BombCantOK()
    {
        if (bombActualCant != 0)
            return true;
        else
            return false;                
    }
    public void BombCounterUP()
    {
        if (bombActualCant < bombMaxCant)
            bombActualCant++;
        Debug.Log(bombActualCant);
    }
    public void BombCounterDOWN()
    {
        if (bombActualCant > 0)
            bombActualCant--;
        Debug.Log(bombActualCant);
    }
    public void SetScore(int _score)
    {
        Score += _score;
    }
    public void PlayerDeath()
    {
        Health--;
    }
}
