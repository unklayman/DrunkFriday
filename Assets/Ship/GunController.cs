using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	public Camera GunCamera;
	public float AngularSpeed = 0.01f;
	private float angleX = 0;
	private float angleY = 0;

	public PlayerController Shooter;
	public ShipController Ship;

	// Use this for initialization
	void Start () {
		GunCamera = GetComponentInChildren<Camera> ();
		Ship = GetComponentInParent<ShipController> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Shooter == null) {
			return;
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			GameController.PlayerShipInteraction (Shooter,Ship,InteractionType.PlayerReleasesShipGunsControls);
		}
		angleY += Input.GetAxis ("Mouse X") * 2f;
		angleX -= Input.GetAxis ("Mouse Y") * 2f;

		transform.rotation = Quaternion.Slerp (transform.rotation,Quaternion.Euler(angleX,angleY,0), AngularSpeed);
	}
}
