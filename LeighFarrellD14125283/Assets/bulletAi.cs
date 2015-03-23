using UnityEngine;
using System.Collections;



public class bulletAi : MonoBehaviour 
{
	public float speed = 50.0f;  //speed of the bullet
	private float deathTime = 0.01f; // the time it takes the bullet to die

	// Use this for initialization
	void Start () 
	{
		Vector2 newVelocity = Vector2.right; // vector2 variable giving direction for the bullet 
		newVelocity.x = speed;  // assigning speed to the direction
		rigidbody2D.velocity = newVelocity; // assigning rigidbody and velocity to the variable
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Vector3 newPosition = transform.position;
		//newPosition.y += speed * Time.deltaTime;
		//transform.position = newPosition;
	}
	
	void OnCollisionEnter2D(Collision2D other) // collision funtion checking for collision
	{
		if(other.gameObject.tag == "Enemy") // checking to see if the game objects tag is enemy
		{
			Destroy(gameObject,deathTime); // if true then it will destoy the bullet after the death time
		}
	}
}

