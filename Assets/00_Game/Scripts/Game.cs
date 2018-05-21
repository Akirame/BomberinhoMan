using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    private static Game instance;
    public GameObject map;
    public GameObject player;
    public GameObject gameUI;
    public GameObject finalUI;
    public int bombRange = 2;
    public int bombMaxCant = 1;
    public int cantEnemy = 1;
    private int bombActualCant;
    private int initialBombCant;
    private int initialRange;
    private int Health;
    private int Score;
    private int enemyActualCant;
    private int enemiesKilled;
    private float totalTimer;

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
        Score = 0;
        enemiesKilled = 0;
        totalTimer = 0;
        initialRange = bombRange;
        initialBombCant = bombMaxCant;
        bombActualCant = bombMaxCant;
        enemyActualCant = cantEnemy;
    }
    private void Update()
    {
        totalTimer += Time.deltaTime;
        if (Health <= 0)
        {
            EndGame();
        }    
    }

    public void HealthPlusPLus()
    {
        Health++;        
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
        enemiesKilled = 0;
    }
    public void EnemyDeath()
    {
        enemyActualCant--;
        enemiesKilled++;        
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
    public void EndGame()
    {
        map.SetActive(false);
        player.SetActive(false);
        gameUI.SetActive(false);
        finalUI.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public int GetCantEnemy()
    {
        return cantEnemy;
    }
    public int GetBombRange()
    {
        return bombRange;
    }
    public int GetTime()
    {

        int timer = Mathf.RoundToInt(totalTimer);
        return timer;
    }
    public int GetBombCant()
    {
        return bombMaxCant;
    }
    public int GetHealth()
    {
        return Health;
    }
    public int GetScore()
    {        
        return Score;
    }
    public int GetEnemiesKilled()
    {        
        return enemiesKilled;
    }

}
