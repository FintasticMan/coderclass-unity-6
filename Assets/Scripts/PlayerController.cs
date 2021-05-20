using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;

	public float fireRate;
	private float nextFire;
	
	void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));
		}
	}
	
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;
		GetComponent<Rigidbody>().rotation = Quaternion.Euler
		(
			GetComponent<Rigidbody>().velocity.z * (tilt / 2),
			0.0f,
			GetComponent<Rigidbody>().velocity.x * -tilt
		);
		
		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);
	}
}
