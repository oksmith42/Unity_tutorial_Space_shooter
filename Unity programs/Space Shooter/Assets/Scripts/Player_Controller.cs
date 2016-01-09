using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}


public class Player_Controller : MonoBehaviour
 {
	private Rigidbody rb;
	public float speed;
	public float tilt;
	
	public Boundary boundary;
	
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	
	private float nextFire;
	
	void Update()
	{
		//Instantiating the shot
		if(Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			//GameObject clone
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);// as GameObject;
		}
	}
	
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		
		rb = GetComponent<Rigidbody>();
		
		rb.velocity = (new Vector3(moveHorizontal,0.0f, moveVertical)) * speed; //making out physics movements tantamount to our inputs
		
		rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),0.0f ,Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));
	
		rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -(tilt));
	}
}
