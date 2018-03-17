using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public Camera PlayerCamera;

	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	public float sensitivity = 2F;
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	private float maxDistance = 1.5f;

	private CharacterController controller ;

	public float AngleY = 0;
	public float AngleX = 0;

	public ShipController Ship {get;set;}

	[Command]
	void CmdFire()
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate (
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

		// Spawn the bullet on the Clients
		NetworkServer.Spawn(bullet);

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);
	}

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.blue;
		PlayerCamera = GetComponentInChildren<Camera>();
		PlayerCamera.enabled = true;
		controller = GetComponent<CharacterController> ();
	}

	void FixedUpdate()
	{
		if (!isLocalPlayer)
		{
			return;
		}
		Move ();	
		if (Ship == null) {
			var lm = LayerMask.GetMask("Interactable");
			RaycastHit hit;
			if (Physics.Raycast (transform.position, PlayerCamera.transform.TransformDirection(Vector3.forward), out hit, maxDistance, lm) && Input.GetKeyDown (KeyCode.E)) {
				var target = hit.collider.gameObject;
				if (target.name.Equals ("ShipControl")) {
					GameController.PlayerShipInteraction (this, target.GetComponentInParent<ShipController> (), InteractionType.PlayerTakesShipControl);
				}
				if (target.name.Equals ("GunControl")) {
					GameController.PlayerShipInteraction (this, target.GetComponentInParent<ShipController> (), InteractionType.PlayerTakesShipGunsControl);
				}

			}
		}
	}

	private void Move(){
		if (Ship == null) {
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				CmdFire();
			}
			//controller.transform.rotation = Quaternion.Euler (0, MainCameraController.GetInstance().GetCameraRotation().eulerAngles.y,0);
			AngleY += Input.GetAxis ("Mouse X") * 2f;
			AngleX -= Input.GetAxis ("Mouse Y") * 2f;
			transform.rotation = Quaternion.Euler (AngleX, AngleY * 2, 0);
			if (controller.isGrounded) {
				moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= speed;
				if (Input.GetButton ("Jump")) {
					moveDirection.y = jumpSpeed;
				}

			}
			moveDirection.y -= gravity * Time.deltaTime;
			controller.Move(moveDirection * Time.deltaTime);
		}
	}
}