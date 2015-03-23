using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]

public class DamageEntity : MonoBehaviour {

	public float damage = 15;
	public bool dammageOnEnter = false;
	public bool dammageOnExit = false;
	public bool dammageOnStay = true;
	
	// Use this for initialization
	void Start () 
	{
		collider.isTrigger = true;
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(dammageOnEnter)
		{
			if(other.GetComponent<BaseEntity>() != null)
			{
			other.GetComponent<BaseEntity>().TakeDammage(damage);
			}
			
			if(other.gameObject.tag == "Player")
			{
				other.GetComponent<BaseEntity>().TakeDammage(damage);
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(dammageOnExit)
		{
			if(other.GetComponent<BaseEntity>() != null)
			{
				other.GetComponent<BaseEntity>().TakeDammage(damage);
			}
		}
	}
	
	void OnTriggerStay(Collider other)
	{
		if(dammageOnStay)
		{
			if(other.GetComponent<BaseEntity>() != null)
			{
				other.GetComponent<BaseEntity>().TakeDammage(damage * Time.deltaTime);
			}
		}
	}
}



















