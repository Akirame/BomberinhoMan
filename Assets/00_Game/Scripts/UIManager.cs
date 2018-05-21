using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{

    public Text healthText;
    public Text bombText;
    public Text rangeText;
    public Text timerText;
    public Text scoreText;
    public Text enemiesKilled;
    private int health;
    private int bombCant;
    private int range;
    private int timer;
    private int score;
    private int enemiesK;

    private void Start()
    {
        health = Game.Get().GetHealth();
        bombCant = Game.Get().GetBombCant();
        range = Game.Get().GetBombRange();
        timer = Game.Get().GetTime();
        score = Game.Get().GetScore();
        enemiesK = Game.Get().GetEnemiesKilled();

        DrawHealthText();
        DrawBombText();
        DrawRangeText();
        DrawTimerText();
        DrawScoreText();
        DrawEnemiesKilled();
    }
    private void Update()
    {
        CheckHealth();
        CheckBomb();
        CheckRange();
        CheckTime();
        CheckScore();
        CheckEnemyKill();
    }

    private void DrawHealthText()
    {
        healthText.text = ("x" + Game.Get().GetHealth());
    }
    private void DrawBombText()
    {
        bombText.text = ("x" + Game.Get().GetBombCant());
    }
    private void DrawRangeText()
    {
        rangeText.text = ("x" + Game.Get().GetBombRange());
    }
    private void DrawTimerText()
    {
        timerText.text = ("" + Game.Get().GetTime());
    }
    private void DrawScoreText()
    {
        int scoreAux = Game.Get().GetScore();
        if (scoreAux <= 0)
            scoreText.text = ("000000" + Game.Get().GetScore());
        else if (scoreAux > 0 && scoreAux < 1000)
            scoreText.text = ("0000" + Game.Get().GetScore());
        else if (scoreAux >= 1000 && scoreAux < 10000)
            scoreText.text = ("000" + Game.Get().GetScore());
    }
    private void DrawEnemiesKilled()
    {
        enemiesKilled.text = ("" + Game.Get().GetEnemiesKilled());
    }
    /// <summary>
    /// Si el HP cambió, actualiza el texto.
    /// </summary>
    private void CheckHealth()
    {
        if (health != Game.Get().GetHealth())
        {
            DrawHealthText();
            health = Game.Get().GetHealth();
        }
    }
    /// <summary>
    /// Si la cantidad de bombas cambió, actualiza el texto.
    /// </summary>
    private void CheckBomb()
    {
        if (bombCant != Game.Get().GetBombCant())
        {
            DrawBombText();
            bombCant = Game.Get().GetBombCant();
        }
    }
    /// <summary>
    /// Si el rango de las bombas cambió, actualiza el texto.
    /// </summary>
    private void CheckRange()
    {
        if (range != Game.Get().GetBombRange())
        {
            DrawRangeText();
            range = Game.Get().GetBombRange();
        }
    }
    /// <summary>
    /// Si el tiempo cambió, actualiza el texto.
    /// </summary>
    private void CheckTime()
    {
        if (timer != Game.Get().GetTime())
        {
            DrawTimerText();
            timer = Game.Get().GetTime();
        }
    }
    /// <summary>
    /// Si el score cambió, actualiza el texto.
    /// </summary>
    private void CheckScore()
    {
        if (score != Game.Get().GetScore())
        {
            DrawScoreText();
            score = Game.Get().GetScore();
        }
    }
    /// <summary>
    /// Si murió un enemigo, actualiza el texto
    /// </summary>
    private void CheckEnemyKill()
    {
        if (enemiesK != Game.Get().GetEnemiesKilled())
        {
            DrawEnemiesKilled();
            enemiesK = Game.Get().GetEnemiesKilled();
        }
    }
}
