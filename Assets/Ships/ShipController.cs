using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShipController : NetworkBehaviour {

	public PlayerController Driver {get;set;}
	public PlayerController Shooter {get;set;}
	public GunController GunController { get; set;}

	public Camera ShipCamera;
	private Rigidbody rb;

	//Axis movement
    public float MaxSpeed = 3f;
	[Range(-1,1)]
	public float Thrust;
	public float ThrustDelta = 0.1f;
	public float Acceleration = 20f;
	public float Deceleration = 2f;
	public float StopThreshold = 0.5f;

	//Angular movement
	private float pitchSensitivity = 10;
	private float angleX = 0;
	private float angleY = 0;
	public float AngularSpeed = 0.01f;
	public float Pitch = 0;

	//prefabs
	public GameObject GunPrefab;

	//space anchors
	public GameObject TurretAttachPosition;


    // Use this for initialization
    void Awake () {
        rb = gameObject.GetComponent<Rigidbody> ();
		ShipCamera = GetComponentInChildren<Camera>();

		if (GunPrefab != null) {
			var gun = (GameObject)Instantiate (
				GunPrefab,
				TurretAttachPosition.transform.position,
				Quaternion.Euler(0,0,0));
			GunController = gun.GetComponentInChildren<GunController> ();
			GunController.Ship = this;
		}
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
		if(Input.GetKeyDown(KeyCode.Escape)){
			GameController.PlayerShipInteraction (Driver,this,InteractionType.PlayerReleasesShipControl);
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

		angleY += Input.GetAxis ("Mouse X") * 2f;
		angleX -= Input.GetAxis ("Mouse Y") * 2f;
        
		transform.rotation = Quaternion.Slerp (transform.rotation,Quaternion.Euler(angleX,angleY,Pitch), AngularSpeed);
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

	void OnDestroy(){
		if (Shooter != null) {
			Destroy (Shooter.gameObject);
		}
		if (Driver != null) {
			Destroy (Driver.gameObject);
		}
		if (GunController != null) {
			Destroy (GunController.gameObject);
		}
		Destroy (gameObject);
	}
}

