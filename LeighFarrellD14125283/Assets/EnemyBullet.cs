using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour 
{
	public float speed = 20.0f;  //speed of the bullet
	private float deathTime = 5f; 
	
	// Use this for initialization
	void Start () 
	{
		
		//Vector3 newVelocity = Vector3.forward;
		//newVelocity.z = speed;
		//rigidbody.velocity = newVelocity;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(0, 0, speed);
		Destroy(gameObject,deathTime);	
	}
}
