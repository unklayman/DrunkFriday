using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

	public GameObject Driver {get;set;}
	private float maxSpeed = 50f;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Move ();
	}

	void Move ()
	{
		if (Driver == null) {
			return;
		}

		if (Input.GetKeyDown (KeyCode.W)) {
			rb.AddForce (transform.forward * 20f);
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			rb.AddForce (transform.forward * 0.05f);
		}


		if(rb.velocity.magnitude > maxSpeed)
		{
			rb.velocity = rb.velocity.normalized * maxSpeed;
		}
	}


}
