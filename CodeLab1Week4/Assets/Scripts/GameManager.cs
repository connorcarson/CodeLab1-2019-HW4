using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public float initCubeSpawnDelay = 3;
	public float cubeSpawnRate = 8;
	

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
		
		HeartSpawn(); //spawn first prize at the start of our game
		InvokeRepeating("CubeSpawn", initCubeSpawnDelay, cubeSpawnRate); //spawn CubeRed and CubeBlue according to our init delay in seconds, and then repeat according to our cubeSpawnRate
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

	void CubeSpawn() //function for spawning our cube prizes
	{
		GameObject newCube1 = Instantiate(Resources.Load<GameObject>("Prefabs/CubeBlue"));
		newCube1.transform.position = new Vector2(Random.Range(-10, 10), Random.Range(-4, 4));
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
