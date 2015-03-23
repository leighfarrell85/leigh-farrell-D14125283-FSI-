using UnityEngine;
using System.Collections;

public enum AIStates{Patrol, Chase, Flee, Attack};

public class RobotAi : MonoBehaviour 
{
	public AIStates aiStates = AIStates.Patrol;
	NavMeshAgent myNMA;
	public Transform target;
	
	public float speed = 10f;
	
	public GameObject explosion;
	
	//public AudioClip explosionSound;
	
	

	// Use this for initialization
	void Start () 
	{
		myNMA = GetComponent<NavMeshAgent>();	
	}
	
	// Update is called once per frame
	void Update ()
	{
		myNMA.SetDestination(target.position);
		
		switch(aiStates)
		{
		case AIStates.Chase:
			Chase();
			break;
			
		case AIStates.Flee:
			Flee();
			break;
			
		case AIStates.Patrol:
			Patrol();
			break;
		case AIStates.Attack:
			Attack();
			break;
	}
}
	
	void Chase()
	{
		gameObject.renderer.material.color = Color.red;
		
		if(Vector3.Distance(transform.position, target.position) <= 10)
		{
			aiStates = AIStates.Attack;
		}
			
	}
	
	void Flee()
	{
		gameObject.renderer.material.color = new Color(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f),Random.Range(0.1f, 1f), 1f);
		
		myNMA.SetDestination(-target.position);
		
		
		if(Vector3.Distance(transform.position, target.position) >= 40)
		{
			aiStates = AIStates.Patrol;
		}
	}
	
	void Patrol()
	{
		gameObject.renderer.material.color = Color.yellow;
		
		if(Vector3.Distance(transform.position, target.position) <= 20)
		{
			aiStates = AIStates.Attack;
		}
	}
	
	void Attack()
	{
		gameObject.renderer.material.color = Color.blue;
		
		if(Vector3.Distance(transform.position, target.position) <= 5)
		{
			aiStates = AIStates.Patrol;
		}
		
		
		
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Bullet")
		{
			Instantiate(explosion, transform.position, transform.rotation);
			audio.Play();
			aiStates = AIStates.Flee;
			Destroy(gameObject, 5);
		}
	}
	
	
	
	
	
	
	
	
	
	
	
	
}
