using UnityEngine;
using System.Collections;

public class BaseWeapon : MonoBehaviour 
{
	[SerializeField]
	protected AudioClip shotFiredAudio = null;
	
	[SerializeField]
	protected ParticleSystem muzzleFlash = null;
	
	[SerializeField]
	protected Texture2D crossHairTecture = null;
	
	
	public GameObject enemy; 
	public GameObject bullet;
	public Transform firingPin;

	// Use this for initialization
	void Start () 
	{
		if(audio == null)
		{
			gameObject.AddComponent<AudioSource>();
		}
		
		audio.clip = shotFiredAudio;
	}
	
	// Update is called once per frame
	void Update () 
	{
		RaycastHit hitInfo;
		
		if(Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f)), out hitInfo, 100))
		{
			transform.forward = (hitInfo.point - transform.position).normalized;
		}
		else
		{
			Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 100));
			transform.forward = (point - transform.position).normalized;
		}
		
		
			
	}
	
	public void Fire()
	{
		audio.Play();
		muzzleFlash.Play();
		
		RaycastHit hitInfo;
		
		Instantiate(bullet, firingPin.position, firingPin.rotation);
		
		if(Physics.Raycast(muzzleFlash.transform.position, transform.forward, out hitInfo))
		{
			if(hitInfo.collider.GetComponent<BaseEntity>() != null)
			{
				hitInfo.rigidbody.AddForceAtPosition(transform.forward * 500, hitInfo.point);
				Debug.Log ("Hit Hit Hit");
			}
			else
			{
			Debug.Log("hello");
			}
		}
		//if(Physics.Raycast(muzzleFlash.transform.position, transform.forward, out hitInfo))
		//{
			//if(hitInfo.collider.gameObject.tag == "Enemy")
			//{
				
				//Destroy(enemy);
				//Debug.Log ("Hit");
			//}
		//}
	}
	
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(Screen.width * 0.5f - 25, Screen.height * 0.5f -25, 50, 50), crossHairTecture);
	}
}








