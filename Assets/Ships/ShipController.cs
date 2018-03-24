﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShipController : NetworkBehaviour
{

	public PlayerController Driver { get; set; }

	public PlayerController Shooter { get; set; }

	public GunController GunController { get; set; }

	public Camera ShipCamera;
	private Rigidbody rb;

	//Axis movement
	public float MaxSpeed = 3f;
	[Range (-1, 1)]
	public float Thrust;
	public float ThrustDelta = 0.1f;
	public float Acceleration = 20f;
	public float Deceleration = 2f;
	public float StopThreshold = 0.1f;

	//Angular movement
	private float pitchSensitivity = 10;
	private float angleX = 0;
	private float angleY = 0;
	public float AngularSpeed = 0.01f;
	public float Pitch = 0;

	private float angularThreshold = 0.01f;

	// Drift parameters
	private float userPitch = 0.0f;
	private float driftPitch = 30f;

	// Debug properties
	public float angularVelocityY;
	public float currentSpeed;
	public float localForward;
	public float localRight;
	public float localPitch;
	public float InputX;

	//prefabs
	public GameObject GunPrefab;

	//space anchors
	public GameObject TurretAttachPosition;


	// Use this for initialization
	void Start ()
	{
		rb = gameObject.GetComponent<Rigidbody> ();
		ShipCamera = GetComponentInChildren<Camera> ();

		if (GunPrefab != null) {
			var gun = (GameObject)Instantiate (
				          GunPrefab,
				          TurretAttachPosition.transform.position,
				          Quaternion.Euler (0, 0, 0));
			GunController = gun.GetComponentInChildren<GunController> ();
			GunController.Ship = this;
		}

		// save default rotation
		angleX = this.transform.rotation.eulerAngles.x;
		angleY = this.transform.rotation.eulerAngles.y;
	}
    
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (rb.velocity.magnitude > MaxSpeed) {
			rb.velocity = rb.velocity.normalized * MaxSpeed;
		}
		Move ();
	}

	void Move ()
	{
		if (Driver == null) {
			return;
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			GameController.PlayerShipInteraction (Driver, this, InteractionType.PlayerReleasesShipControl);
		}
		if (Input.GetKeyDown (KeyCode.W)) {
			Thrust += ThrustDelta;
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			Thrust -= ThrustDelta;
		}
		if (Input.GetKeyDown (KeyCode.A)) {
			userPitch -= pitchSensitivity;
			//Pitch -= pitchSensitivity;
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			userPitch += pitchSensitivity;
			//Pitch += pitchSensitivity;
		}

		if (Mathf.Abs (Thrust) > StopThreshold) {
			rb.AddForce (transform.forward * Thrust * Acceleration, ForceMode.Acceleration);
		} else {
			rb.AddForce (rb.velocity * -Deceleration, ForceMode.Acceleration);
		}

		angleY += Input.GetAxis ("Mouse X") * 2f;
		angleX -= Input.GetAxis ("Mouse Y") * 2f;
		InputX = Input.GetAxis ("Mouse X");

		currentSpeed = rb.velocity.magnitude;


		// z - forward
		// x - right
		// y - top

		var relativeVelocity = transform.InverseTransformDirection (rb.velocity);

		// debug info
		localForward = relativeVelocity.z;
		localRight = relativeVelocity.x;




		if (Mathf.Abs(relativeVelocity.z) > 0.001f) {
			var changedDriftPitch = (relativeVelocity.x / relativeVelocity.z) * driftPitch;

			Pitch = userPitch + changedDriftPitch;

			localPitch = changedDriftPitch;
		}

		var ratioSpeed = Mathf.Abs (currentSpeed / MaxSpeed);

		if (ratioSpeed > angularThreshold) {
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (angleX, angleY, Pitch), AngularSpeed);
		}
	}

	void OnDestroy ()
	{
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

