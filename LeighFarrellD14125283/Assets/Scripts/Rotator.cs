using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour 
{

	public Vector2 rotationRange = new Vector2(70, 70);
	public float rotationSpeed = 10f;
	public float dampingTime = 0.2f;
	public bool relative = true;
	
	private Vector3 targetAngles;
	private Vector3 followAngles;
	private Vector3 followVelocity;
	private Quaternion originalRotation;
	
	
	// Use this for initialization
	void Start () 
	{
		originalRotation = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float horizontalInput;
		float verticalInput;
		
		// detecting relative motion 
		if(relative)
		{
			horizontalInput = Input.GetAxis("Mouse X");
			verticalInput = Input.GetAxis("Mouse Y");
		
		
		if(targetAngles.y > 180)
		{
			targetAngles.y -= 360;
			followAngles.y -= 360;
		}
		else if(targetAngles.y < -180)
		{
			targetAngles.y += 360;
			followAngles.y += 360;
		}
		
		
		if(targetAngles.x > 180)
		{
			targetAngles.x -= 360;
			followAngles.x -= 360;
		}
		else if(targetAngles.x < -180)
		{
			targetAngles.x += 360;
			followAngles.x += 360;
		}
		
			// setting target angles
			targetAngles.y += horizontalInput * rotationSpeed;
			targetAngles.x += verticalInput * rotationSpeed;
			
			// clamping it to the range
			targetAngles.y = Mathf.Clamp (targetAngles.y, rotationRange.y * - 0.5f, rotationRange.y * 0.5f);
			targetAngles.x = Mathf.Clamp (targetAngles.x, rotationRange.x * - 0.5f, rotationRange.x * 0.5f);		
		}
		else
		{
			// exact position
			horizontalInput = Input.mousePosition.x;
			verticalInput = Input.mousePosition.y;
			
			targetAngles.y = Mathf.Lerp(rotationRange.y * - 0.5f, rotationRange.y * 0.5f, horizontalInput / Screen.width);
			targetAngles.x = Mathf.Lerp(rotationRange.x * - 0.5f, rotationRange.x * 0.5f, verticalInput / Screen.height); 
		}
		
		followAngles = Vector3.SmoothDamp(followAngles, targetAngles, ref followVelocity, dampingTime);
		
		transform.localRotation = originalRotation * Quaternion.Euler(-followAngles.x, followAngles.y, 0);
	}
	
	
	
	
	
	
	
	
	
	
	
	
}
