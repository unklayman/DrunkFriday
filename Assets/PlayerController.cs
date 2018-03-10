using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	public float sensitivityX = 2F;
	public GameObject bulletPrefab;
	public Transform bulletSpawn;

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

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			CmdFire();
		}

		CharacterController controller = GetComponent<CharacterController>();

		controller.transform.Rotate (0, Input.GetAxis ("Mouse X") *  sensitivityX, 0);
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



	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.blue;
		//MovementController.GetInstance().SetTarget (this.gameObject);
		MainCameraController.GetInstance ().SetTarget (this.gameObject);

	}
}