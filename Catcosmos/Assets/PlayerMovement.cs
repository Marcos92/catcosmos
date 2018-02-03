using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	GravitySource planet;
	Rigidbody body;

	public float movementSpeed;
	Vector3 moveInput;
	
	void Awake () 
	{
		planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravitySource>();
		body = GetComponent<Rigidbody> ();

		// Disable rigidbody gravity and rotation as this is simulated in GravityAttractor script
		body.useGravity = false;
		body.constraints = RigidbodyConstraints.FreezeRotation;
	}
	
	void FixedUpdate () 
	{
		planet.Attract(body);

		Vector3 movement = transform.TransformDirection(moveInput * movementSpeed) * Time.fixedDeltaTime;
		body.MovePosition(body.position + movement);
	}

	void Update()
	{
		moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();
	}
}
