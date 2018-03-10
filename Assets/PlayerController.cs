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
	public bool isBusy = false;

	void Start(){
		
	}

	void Update()
	{
		if (!isLocalPlayer && !isBusy)
		{
			return;
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
