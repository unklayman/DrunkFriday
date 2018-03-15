using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    public GameObject Driver {get;set;}
    private Rigidbody rb;

	//Axis movement
    public float MaxSpeed = 3f;
	[Range(-1,1)]
	public float Thrust;
	public float ThrustDelta = 0.1f;
	public float Acceleration = 20f;
	public float Deceleration = 1f;
	public float StopThreshold = 0.5f;

	//Angular movement
	private float pitchSensitivity = 10;
	public float AngularSpeed = 0.01f;
	public float Pitch = 0;


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
			Thrust += ThrustDelta;
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			Thrust -= ThrustDelta;
		}
		if (Input.GetKeyDown (KeyCode.A)) {
			Pitch -= pitchSensitivity;
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			Pitch += pitchSensitivity;
		}

        rb.AddForce (transform.forward * Thrust * Acceleration , ForceMode.Acceleration);

        if(rb.velocity.magnitude > MaxSpeed)
        {
            rb.velocity = rb.velocity.normalized * MaxSpeed;
        }

		var desiredRotation = Quaternion.Euler(
			MainCameraController.GetInstance().GetCameraRotation().eulerAngles.x - 30,
			MainCameraController.GetInstance().GetCameraRotation().eulerAngles.y,
			Pitch);
        
		transform.rotation = Quaternion.Slerp (transform.rotation,desiredRotation, AngularSpeed);
		Decelerate ();
    }

    void Decelerate ()
    {
        DecelerateMovement ();
    }

    void DecelerateMovement ()
    {
		if (rb.velocity.magnitude > StopThreshold) {
			rb.AddForce (Vector3.one * -Deceleration, ForceMode.Acceleration);
		} else if (rb.velocity.magnitude < -StopThreshold) {
			rb.AddForce (Vector3.one * Deceleration, ForceMode.Acceleration);
        } else {
            rb.velocity = Vector3.zero;
        }
    }
}

