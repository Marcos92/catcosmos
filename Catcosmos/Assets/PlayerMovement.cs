using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	GravitySource planet;
	Rigidbody body;

	public float movementSpeed;
	Vector3 moveInput;

	public delegate void MoveEvent();
    public static event MoveEvent OnMove;
	
	void Awake () 
	{
		planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravitySource>();
		body = GetComponent<Rigidbody> ();

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

		if(moveInput != Vector3.zero)
		{
			OnMove();
		}
	}
}
