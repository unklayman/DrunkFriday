﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	public float sensitivityX = 2F;

	private Camera camera;


	void Update()
	{
		if (!isLocalPlayer) {
			return;
		}
		CharacterController controller = GetComponent<CharacterController>();

		controller.transform.Rotate (0, Input.GetAxis ("Mouse X") *  sensitivityX, 0);

		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
//			controller.transform.rotation = new Quaternion (moveDirection.x, moveDirection.y, moveDirection.z, Time.deltaTime * Input.GetAxis ("Mouse X") * sensitivityX);
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
		camera = Camera.main;
		var lookAtCamera = camera.GetComponent(typeof(LookAtCamera)) as LookAtCamera;

		if (lookAtCamera != null) {
			lookAtCamera.target = this.gameObject;
		}
	}
}