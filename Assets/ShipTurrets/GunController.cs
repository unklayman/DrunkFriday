using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GunController : NetworkBehaviour {

	public Camera GunCamera;
	public float AngularSpeed = 0.01f;
	private float angleX = 0;
	private float angleY = 0;

	public PlayerController Shooter;
	public ShipController Ship;

	//space positions
	public GameObject CannonBallSpawnPosition;
	public GameObject CannonBallPrefab;

	// Use this for initialization
	void Start () {
		if (GunCamera == null) {
			GunCamera = GetComponentInChildren<Camera> ();
		}
		if (Ship == null) {
			Ship = GetComponentInParent<ShipController> ();
		}
		Physics.IgnoreCollision (Ship.gameObject.GetComponent<Collider> (), GetComponent<Collider> ());
	}

	[Command]
	void CmdFire()
	{
		// Create the Bullet from the Bullet Prefab
		var cb = (GameObject)Instantiate (
			CannonBallPrefab,
			CannonBallSpawnPosition.transform.position,
			Quaternion.Euler(gameObject.transform.rotation.eulerAngles));

		// Add velocity to the bullet
		cb.GetComponent<Rigidbody>().velocity = cb.transform.forward * 100;

		// Spawn the bullet on the Clients
		NetworkServer.Spawn(cb);

		// Destroy the bullet after 2 seconds
		Destroy(cb, 5.0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Ship != null) {
			this.transform.position = Ship.TurretAttachPosition.transform.position;
		}
		if (Shooter == null) {
			return;
		}
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			CmdFire();
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			GameController.PlayerShipInteraction (Shooter,Ship,InteractionType.PlayerReleasesShipGunsControls);
		}
		angleY += Input.GetAxis ("Mouse X") * 2f;
		angleX -= Input.GetAxis ("Mouse Y") * 2f;

		transform.rotation = Quaternion.Slerp (transform.rotation,Quaternion.Euler(angleX,angleY,0), AngularSpeed);
	}
}
