using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum States{Patrol, Chase, Flee, LookAround, Attack};

public class EnemyAI_Behaviours : MonoBehaviour 
{

	
	public States aiStates = States.Patrol;
	public Transform tTarget;
	public Vector3 vTarget; 
	public Transform guard;
	
	public Transform[] wayPoints;
	public float speed;
	public int curWayPoint;
	public bool doPatrol = true;
	public Vector3 Target;
	public Vector3 moveDirection;
	public Vector3 Velocity;
	
	public float abortDistance = 10f;
	
	float lookTime = 0f;
	float fleaTime = 3f;
	
	float checkoutTime = 15f;
	private float lookCounter = 0f;
	
	NavMeshAgent myNMA;
	
	//public Transform navMeshTarget; 
	Animator anim;
	
	Ray ray;
	RaycastHit target;
	
	public GameObject bullet;
	public Transform firingPin;
	public float delayTime = 8f;
	
	private float counter = 0f;
	
	// Use this for initialization
	void Start () 
	{
		myNMA = GetComponent<NavMeshAgent>();
		
		anim = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(aiStates)
		{
		case States.Chase:
			Chase();
			break;
		
		case States.Flee:
			Flee();
			break;
		
		case States.LookAround:
			LookAround();
			break;
		
		case States.Patrol:
			Patrol();
			break;
		case States.Attack:
			Attack();
			break;
		}
		//myNMA.SetDestination(navMeshTarget.position);
		
		rigidbody.velocity = Velocity;
		transform.LookAt(Target);
		
		myNMA.SetDestination(Target);
		
		//lookCounter += Time.deltaTime;
		
		//if(lookCounter >= checkoutTime)
		//{
			//aiStates = States.LookAround;
			//lookCounter = 0f;
		//}
		
		
		
	}
	
	void Patrol()
	{
		Collider[] hitColliders = Physics.OverlapSphere (transform.position, 20);
		
		for (int i = 0; i < hitColliders.Length; i++)
		{
			if (hitColliders[i].gameObject.tag == "Player")
			{
				aiStates = States.Chase;
			}
		}
		
		
		
		
		myNMA.speed = 3;
		doPatrol = true;
		anim.SetBool("Patrol", true);
		anim.SetBool("LookAround", false);
		anim.SetBool("Attack", false);
		
		if(curWayPoint < wayPoints.Length)
		{
			Target = wayPoints[curWayPoint].position;
			moveDirection = Target - transform.position;
			Velocity = rigidbody.velocity;
			myNMA.SetDestination(Target);
			
			
			if(moveDirection.magnitude < 1)
			{
				curWayPoint++;
			}
			else
			{
				Velocity = moveDirection.normalized * speed;
			}
		}
		else
		{
			if(doPatrol)
			{
			anim.SetBool("Patrol", true);
			curWayPoint = 0;
			}
			else
			{
				Velocity = Vector3.zero;
			}
		}
		lookCounter += Time.deltaTime;
		
		if(lookCounter >= checkoutTime)
		{
			aiStates = States.LookAround;
			lookCounter = 0f;
		}
		Debug.Log(lookCounter);
		
		
		
		
	}
	
	void Chase()
	{
		myNMA.speed = 5;
		doPatrol = false;
		anim.SetBool("Patrol", false);
		anim.SetBool("LookAround", false);
		anim.SetBool("Attack", true);
		
		//Vector3 lookAtPlayer = new Vector3(tTarget.position.x, this.transform.position.y, tTarget.position.z);
		//guard.LookAt(lookAtPlayer);
		
		Target = tTarget.position;
	 	myNMA.SetDestination(tTarget.position);
		
		
	
			if(Vector3.Distance(transform.position, tTarget.position) >= 25)
			{
				aiStates = States.Patrol;
			}
			else if(Vector3.Distance(transform.position, tTarget.position) <= 15)
			{
				aiStates = States.Attack;
			}	
		
		
		if(transform.position != tTarget.position)
		{
			vTarget = tTarget.position;
			moveDirection = vTarget - transform.position;
			Velocity = rigidbody.velocity;
			
			if(moveDirection.magnitude > 1)
			{
				Velocity = moveDirection.normalized * speed;
			}			
			else
			{
				Velocity  = Vector3.zero;
			}
		}
		
		
	}
	
	void Flee()
	{
		myNMA.speed = 10;
		doPatrol = false;
		fleaTime += Time.deltaTime;
		
		if(transform.position != tTarget.position)
		{
			vTarget = tTarget.position;
			moveDirection = vTarget + transform.position;
			Velocity = rigidbody.velocity;
			myNMA.SetDestination(Target);
			
			if(moveDirection.magnitude > 1)
			{
				Velocity = moveDirection.normalized * speed;
			}
				if(fleaTime >= 10)
				{
					aiStates = States.Patrol;
				}			
		}
	}
	
	void LookAround()
	{
		myNMA.speed = 0;
		doPatrol = false;
		
		anim.SetBool("Patrol", false);
		anim.SetBool("Attack", false);
		lookTime += Time.deltaTime;
		
		Velocity = Vector3.zero;
		//moveDirection = Vector3.zero;
		
		anim.SetBool("LookAround", true);
		
		if(lookTime >= 9f)
		{
			aiStates = States.Patrol;
			//lookCounter = 0f;
		}
	}
	
	void Attack()
	{
		anim.SetBool("Attack", true);
		anim.SetBool("Patrol", false);
		anim.SetBool("LookAround", false);
		
	
		Target = tTarget.position;
		myNMA.SetDestination(tTarget.position);
		
		counter +=  Time.deltaTime;
		
		if(counter > delayTime)
		{
			Instantiate(bullet, firingPin.position, firingPin.rotation);
			audio.Play();
			counter = 0f;
		}
		
		if(Vector3.Distance(transform.position, tTarget.position) >= 25)
		{
			aiStates = States.Patrol;
		}	
		
	}
	
	
	
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Bullet")
		{
			aiStates = States.Flee;
			Destroy(gameObject);
		}
		Debug.Log("HIT!!");
		
		//if(col.gameObject.tag == "LookAroundCollider")
		//{
		//aiStates = States.LookAround;
		//}
	}	
	
	
	
	
	
}
