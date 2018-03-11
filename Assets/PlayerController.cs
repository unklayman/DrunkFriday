using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	public float sensitivity = 2F;
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	private float maxDistance = 1.5f;

	private CharacterController controller ;

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

	void Update()
	{
		if (!isLocalPlayer)
		{
			return;
		}
		Move ();
	}

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.blue;
		MainCameraController.GetInstance().SetTarget (this.gameObject);
		controller = GetComponent<CharacterController> ();
	}

	void FixedUpdate()
	{
		
		if (Ship == null) {
			var lm = LayerMask.GetMask("Interactable");
			RaycastHit hit;
			if (Physics.Raycast (transform.position, MainCameraController.GetInstance().GetCameraViewVector(), out hit, maxDistance, lm) && Input.GetKeyDown (KeyCode.E)) {
				EventManager.TriggerEvent(EventType.PLAYER_REQUEST_SHIP_CONTROL, new PlayerShipControlInteractionEvent(gameObject, hit.collider.transform.parent.gameObject));// control is a child of a ship prefab
			}
		}
	}

	private void Move(){
		if (Ship != null) { //means he is captain
			if(Input.GetKeyDown(KeyCode.Escape)){
				EventManager.TriggerEvent (EventType.PLAYER_LEAVES_SHIP_CONTROL, new PlayerShipControlInteractionEvent (gameObject,Ship));
			}
		} else {
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				CmdFire();
			}
			controller.transform.Rotate (0, Input.GetAxis ("Mouse X") *  sensitivity, 0);
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

	public GameObject Ship {get;set;}
}