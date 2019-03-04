using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Rigidbody rb;

	public KeyCode upKey;
	public KeyCode downKey;
	public KeyCode rightKey;
	public KeyCode leftKey;
	
	public float forceMultiplier = 1;
	public int score = 0;
	
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector2 newForce = new Vector2(0, 0); //the force we will add to our player
		
		if (Input.GetKey(upKey))
		{
			//Debug.Log("Pressed W");
			newForce.y += forceMultiplier; //calculate force up
		}

		if (Input.GetKey(downKey)) //calculate force down
		{
			newForce.y -= forceMultiplier;
		}

		if (Input.GetKey(rightKey)) //calculate force right
		{
			newForce.x += forceMultiplier;
		}
		
		if (Input.GetKey(leftKey)) //calculate force left
		{
			newForce.x -= forceMultiplier;
		}
		
		rb.AddForce(newForce); //apply total force
	}
}
