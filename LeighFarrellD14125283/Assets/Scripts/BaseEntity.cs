using UnityEngine;
using System.Collections;

public class BaseEntity : MonoBehaviour 
{

	[SerializeField]
	protected BaseWeapon currentWeapon = null;
	
	[SerializeField]
	protected AudioClip[] hurtSoundEffects;
	
	[SerializeField]
	protected Texture2D healthBarTexture;
	
	protected float currentHealth  = 100;
	
	public float CurrentHealth 
	{
	get {
		return currentHealth;
		}
	}
	
	protected float maximumHealth = 100;
	
	public float MaximumHealth
	{
	get {
		return maximumHealth;
		}
	}
	
	void Start()
	{
		currentHealth = maximumHealth;
	}
	
	void Update()
	{
		if(currentWeapon != null)
		{
			if(Input.GetButtonDown("Fire1"))
			{
				currentWeapon.Fire();
			}
		}
	}
	
	public void TakeDammage(float damageAmount)
	{
		currentHealth -= damageAmount;
		
		if(currentHealth <= 0.0f)
		{
			Debug.Log("you died");
			gameObject.SetActive(false);
			return;
		}
		
		if(!audio.isPlaying && hurtSoundEffects.Length > 1)
		{
			int n = Random.Range(1, hurtSoundEffects.Length);
			audio.clip = hurtSoundEffects[n];
			audio.Play();
			
			hurtSoundEffects[n] = hurtSoundEffects[0];
			hurtSoundEffects[0] = audio.clip;
		}
	
	}
	
	void OnGUI()
	{
		if(healthBarTexture != null)
		{
			GUI.color = Color.Lerp(Color.red, Color.green, currentHealth / maximumHealth);
			GUI.DrawTexture(new Rect(10, 10, 200 * currentHealth / maximumHealth, 10), healthBarTexture);
		}
	}
}










