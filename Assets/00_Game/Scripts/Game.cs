using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    private static Game instance;

    public int bombRange = 2;
    public int bombMaxCant = 1;
    public int cantEnemy = 1;
    private int bombActualCant;
    private int initialBombCant;
    private int initialRange;
    private int Health;
    private int Score = 0;
    private int enemyActualCant;

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
        initialRange = bombRange;
        initialBombCant = bombMaxCant;
        bombActualCant = bombMaxCant;
        enemyActualCant = cantEnemy;
    }
    private void Update()
    {
        if (Health <= 0)
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);       
    }

    public void HealthPlusPLus()
    {
        Health++;        
    }
    public int GetBombRange()
    {
        return bombRange;
    }
    /// <summary>
    /// Si se puede activar una bomba devuelve true
    /// </summary>
    /// <returns></returns>
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
    }
    public void BombCounterDOWN()
    {
        if (bombActualCant > 0)
            bombActualCant--;        
    }
    public void BombCantPlusPlus()
    {
        bombMaxCant++;
        bombActualCant++;        
    }
    public void BombRangePlusPLus()
    {        
        bombRange++;        
    }
    public void SetScore(int _score)
    {
        Score += _score;
    }
    public void PlayerDeath()
    {
        Health--;
        bombMaxCant = initialBombCant;
        bombActualCant = bombMaxCant;
        bombRange = initialRange;
    }
    public int GetCantEnemy()
    {
        return cantEnemy;
    }
    public void EnemyDeath()
    {
        enemyActualCant--;
    }    
    /// <summary>
    /// Devuelve true si la cantidad de enemigos es 0
    /// </summary>
    /// <returns></returns>
    public bool DoorOpen()
    {
        if (enemyActualCant <= 0)
            return true;
        else
            return false;
    }
}
