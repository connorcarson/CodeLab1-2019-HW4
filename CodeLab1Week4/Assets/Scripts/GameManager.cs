using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	public float initCubeBlueSpawnDelay = 3;
	public float initCubeRedSpawnDelay = 5.5f;
	public float cubeSpawnRate = 8;

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

			if (health < 0)
			{
				health = 0;
			}
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
				string fullPathToFile = Application.dataPath + MyHighScore_File; //create string for path to MyHighScore file
				File.WriteAllText(fullPathToFile, "The current high score is " + highScore); //write HighScore in file
			}
		}
	}

	public static GameManager instance;
	
	// Use this for initialization
	void Start()
	{	
		if (instance == null) //if there is no other instance of LevelManager already in the scene
		{
			DontDestroyOnLoad(gameObject); //don't destroy this LevelManager
			instance = this; //set the current instance to this LevelManager
		}
		else
		{
			Destroy(gameObject); //otherwise, if there is an instance already in this scene, destroy this LevelManager
		}

		string myHighScoreFileText = File.ReadAllText(Application.dataPath + MyHighScore_File); //get text from myHighScore file
		string[] highScoreSplit = myHighScoreFileText.Split(' '); //divide text by spaces
		HighScore = Int32.Parse(highScoreSplit[5]); //the high score number in file to int and set HighScore to that int

		HeartSpawn(); //spawn first prize at the start of our game
		InvokeRepeating("CubeBlueSpawn", initCubeBlueSpawnDelay, cubeSpawnRate); //spawn CubeBlue according to our init delay in seconds, and then repeat according to our cubeSpawnRate
		InvokeRepeating("CubeRedSpawn", initCubeRedSpawnDelay, cubeSpawnRate); //spawn CubeRed according to our init delay in seconds, and then repeat according to our cubeSpawnRate
	}

	// Update is called once per frame
	void Update()
	{
		CheckForPrize();
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
}
