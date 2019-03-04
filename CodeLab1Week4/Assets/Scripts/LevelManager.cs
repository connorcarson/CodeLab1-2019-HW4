using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private List<string> difficultyLevel = new List<string>() {"Easy", "Normal", "Difficult"};
    public Dropdown dropdown;
    
    //public GameObject playButton;
    //public GameObject dropDownMenu;
    
    public Canvas canvas;
    
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI highScoreText;

    private Animator anim;

    public int sceneIndex;
    public float timeLeft = 30;
    public float timeMax = 30;
    private int wholeTime;
    public float restartTimer;
    public float restartDelay = 8;

    
    public float TimeLeft
    {
        get { return timeLeft; }
        set
        {
            timeLeft = value;
            if (timeLeft > timeMax)
            {
                timeLeft = timeMax;
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
        //playButton.SetActive(false);
        //dropDownMenu.SetActive(false);
        
        dropdown.AddOptions(difficultyLevel);
        
        PlayerPrefs.SetInt("difficultyIndex", 2);
        anim = canvas.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        LevelTimer();
        ScoreDisplay();
        HealthDisplay();
        HighScoreDisplay();
        
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

    void HighScoreDisplay()
    {
        highScoreText.text = "High Score: " + GameManager.instance.HighScore;
    }

    void LevelLoader()
    {
        anim.SetTrigger("Level1Over");
        restartTimer += Time.deltaTime; //count up in seconds
        if (restartTimer >= restartDelay) //if restart timer is equal to our restart delay
        {
            SceneManager.LoadScene(sceneIndex); //load next level
        }
    }
    
    public void GameOver()
    {
        anim.SetTrigger("GameOver"); //start Game Over animation
        
        restartTimer += Time.deltaTime; //count up in seconds
        if (restartTimer >= restartDelay) //if restart timer is equal to our restart delay
        {
            GameManager.instance.Score = 0; //reset score
            GameManager.instance.Health = 100;
            SceneManager.LoadScene(0); //load first level
        }
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0;
        //playButton.SetActive(true);
        //dropDownMenu.SetActive(true);
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        //playButton.SetActive(false);
        //dropDownMenu.SetActive(false);
    }
    
    public void DifficultySetting(int difficultyIndex)
    {
        if (difficultyIndex == 0)
        {
            timeLeft = 90;
            timeMax = 90;
            GameManager.instance.initCubeRedSpawnDelay = 5;
            GameManager.instance.initCubeBlueSpawnDelay = 8;
            GameManager.instance.cubeSpawnRate = 10;
            SceneManager.LoadScene(0);
        }
        else if (difficultyIndex == 1)
        {
            timeLeft = 60;
            timeMax = 60;
            GameManager.instance.initCubeRedSpawnDelay = 3.5f;
            GameManager.instance.initCubeBlueSpawnDelay = 6f;
            GameManager.instance.cubeSpawnRate = 8;
            SceneManager.LoadScene(0);
        } 
        else if (difficultyIndex == 2)
        {
            timeLeft = 30;
            timeMax = 30;
            GameManager.instance.initCubeRedSpawnDelay = 1.5f;
            GameManager.instance.initCubeBlueSpawnDelay = 3;
            GameManager.instance.cubeSpawnRate = 5;
            SceneManager.LoadScene(0);
        }
    }
}
