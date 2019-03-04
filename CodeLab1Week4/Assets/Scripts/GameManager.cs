using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public Button playButton;
	
	public float initCubeBlueSpawnDelay = 3;
	public float initCubeRedSpawnDelay = 5.5f;
	public float cubeSpawnRate = 8;
	
	public float timeLeft = 30;
	public float timeMax = 30;
	private int wholeTime;
	
	public int sceneIndex;
	
	public float restartTimer;
	public float restartDelay = 8;
	
	public TextMeshProUGUI timerText;
	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI healthText;
	public TextMeshProUGUI highScoreText;
	
	private Animator anim;
	
	public Canvas canvas;
	
	private List<string> difficultyLevel = new List<string>() {"Easy", "Normal", "Difficult"};
	public Dropdown dropdown;

	private const string MyHighScore_File = "/MyHighScore.txt";
	
	int health = 100;
	public int Health
	{
		get { return health; }
		set
		{
			health = value;
			if (health > 100)
			{
				health = 100;
			}

			if (health <= 0)
			{
				health = 0;
				GameOver();
			}

			healthText.text = "Health: " + health;
		}
	}

	int score = 0;
	public int Score
	{
		get
		{
			return score;
		}
		set
		{
			score = value;
			scoreText.text = "Score: " + score;
			HighScore = score;
		}
	}

	int highScore = 0;
	public int HighScore
	{
		get
		{
			return highScore;
		}
		set
		{
			if (value > highScore)
			{
				highScore = value;
				highScoreText.text = "High Score: " + highScore;
				string fullPathToFile = Application.dataPath + MyHighScore_File; //create string for path to MyHighScore file
				File.WriteAllText(fullPathToFile, "The current high score is " + highScore); //write HighScore in file
			}
		}
	}
	
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

			if (timeLeft <= 0)
			{
				timeLeft = 0;
				LevelLoader();
			}
		}
	}

	public static GameManager instance;
	
	// Use this for initialization
	void Start()
	{	
		
		//playButton.gameObject.SetActive(false);
		
		if (instance == null) //if there is no other instance of LevelManager already in the scene
		{
			DontDestroyOnLoad(gameObject); //don't destroy this LevelManager
			instance = this; //set the current instance to this LevelManager
		}
		else
		{
			Destroy(gameObject); //otherwise, if there is an instance already in this scene, destroy this LevelManager
		}

		anim = canvas.GetComponent<Animator>();
		
		string myHighScoreFileText = File.ReadAllText(Application.dataPath + MyHighScore_File); //get text from myHighScore file
		string[] highScoreSplit = myHighScoreFileText.Split(' '); //divide text by spaces
		HighScore = Int32.Parse(highScoreSplit[5]); //the high score number in file to int and set HighScore to that int

		HeartSpawn(); //spawn first prize at the start of our game
		InvokeRepeating("CubeBlueSpawn", initCubeBlueSpawnDelay, cubeSpawnRate); //spawn CubeBlue according to our init delay in seconds, and then repeat according to our cubeSpawnRate
		InvokeRepeating("CubeRedSpawn", initCubeRedSpawnDelay, cubeSpawnRate); //spawn CubeRed according to our init delay in seconds, and then repeat according to our cubeSpawnRate
		
		dropdown.AddOptions(difficultyLevel);
	}

	// Update is called once per frame
	void Update()
	{
		LevelTimer();
		CheckForPrize();
	}
	
	void LevelTimer()
	{
		TimeLeft -= Time.deltaTime; //Countdown one second, every second
		wholeTime = (int) TimeLeft; //Convert float to int, round time in seconds up to whole number
		timerText.text = "" + wholeTime; //display Time
	}

	void HeartSpawn() //function for spawning our heart prize
	{
		GameObject newPrize = Instantiate(Resources.Load<GameObject>("Prefabs/Prize")); //loads prefab into game
		newPrize.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-4, 4), 0.78f); //at new, random location
	}

	void CubeBlueSpawn() //function for spawning our blue cube prizes
	{
		GameObject newCube1 = Instantiate(Resources.Load<GameObject>("Prefabs/CubeBlue"));
		newCube1.transform.position = new Vector2(Random.Range(-10, 10), Random.Range(-4, 4));
	}

	void CubeRedSpawn() //function for spawning our red cube prizes
	{
		GameObject newCube2 = Instantiate(Resources.Load<GameObject>("Prefabs/CubeRed"));
		newCube2.transform.position = new Vector2(Random.Range(-10, 10), Random.Range(-4, 4));
	}

	void CheckForPrize() //check if heart prize has been destroyed
	{
		GameObject prizesInScene = GameObject.FindWithTag("Prize"); //find all objects in scene tagged "Prize"
		if (prizesInScene == null) //if there are no objects tagged "Prize" in our scene
		{
			HeartSpawn(); //then spawn a new prize
		}
	}
	
	void LevelLoader()
	{
		anim.SetTrigger("Level1Over");
		restartTimer += Time.deltaTime; //count up in seconds
		if (restartTimer >= restartDelay) //if restart timer is equal to our restart delay
		{
			timeLeft = timeMax;
			SceneManager.LoadScene(sceneIndex); //load next level
		}
	}
    
	public void GameOver()
	{
		anim.SetTrigger("GameOver"); //start Game Over animation
        
		restartTimer += Time.deltaTime; //count up in seconds
		if (restartTimer >= restartDelay) //if restart timer is equal to our restart delay
		{
			Health = 100;
			Score = 0;
			timeLeft = timeMax;
			SceneManager.LoadScene(0); //load first level
		}
	}

	public void Pause()
	{
		Time.timeScale = 0;
		//playButton.gameObject.SetActive(true);
	}

	/*public void Play()
	{
		Time.timeScale = 1;
		playButton.gameObject.SetActive(false);
	}*/
	
	public void DifficultySetting(int difficultyIndex)
    {
        if (difficultyIndex == 0)
        {
	        SceneManager.LoadScene(0);
	        timeLeft = 90;
            timeMax = 90;
            instance.initCubeRedSpawnDelay = 5;
            instance.initCubeBlueSpawnDelay = 8;
            instance.cubeSpawnRate = 10;
        }
        else if (difficultyIndex == 1)
        {
	        SceneManager.LoadScene(0);
	        timeLeft = 60;
            timeMax = 60;
            initCubeRedSpawnDelay = 3.5f;
            initCubeBlueSpawnDelay = 6f;
            cubeSpawnRate = 8;
        } 
        else if (difficultyIndex == 2)
        {
	        SceneManager.LoadScene(0);
	        timeLeft = 30;
            timeMax = 30;
            initCubeRedSpawnDelay = 1.5f;
            initCubeBlueSpawnDelay = 3;
            cubeSpawnRate = 5;
        }
    }
}
