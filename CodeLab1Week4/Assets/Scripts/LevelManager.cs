using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Canvas canvas;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;

    private Animator anim;

    public float timeLeft = 30;
    private int wholeTime;
    public float restartTimer;
    public float restartDelay = 8;

    public float TimeLeft
    {
        get { return timeLeft; }
        set
        {
            timeLeft = value;
            if (timeLeft > 30)
            {
                timeLeft = 30;
            }

            if (timeLeft < 0)
            {
                timeLeft = 0;
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        anim = canvas.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        LevelTimer();
        ScoreDisplay();
        HealthDisplay();
        
        if (timeLeft <= 0)
        {
            LevelLoader();
        }

        if (GameManager.instance.Health <= 0)
        {
            GameOver();
        }
    }
    
    void LevelTimer()
    {
        TimeLeft -= Time.deltaTime; //Countdown one second, every second
        wholeTime = (int) TimeLeft; //Convert float to int, round time in seconds up to whole number
        timerText.text = "" + wholeTime; //display Time
    }
    
    void ScoreDisplay()
    {
        scoreText.text = "Score: " + GameManager.instance.Score;
    }

    void HealthDisplay()
    {
        healthText.text = "Health: " + GameManager.instance.Health;
    }

    void LevelLoader()
    {
        anim.SetTrigger("Level1Over");
        restartTimer += Time.deltaTime; //count up in seconds
        if (restartTimer >= restartDelay) //if restart timer is equal to our restart delay
        {
            SceneManager.LoadScene(1); //load next level
        }
    }
    
    public void GameOver()
    {
        anim.SetTrigger("GameOver"); //start Game Over animation
        
        restartTimer += Time.deltaTime; //count up in seconds
        if (restartTimer >= restartDelay) //if restart timer is equal to our restart delay
        {
            SceneManager.LoadScene(0); //load next level
        }
    }
}
