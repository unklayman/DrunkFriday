using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController {

	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	public float sensitivityX = 2F;

	private MovementController(){}
	private static MovementController instance = new MovementController();

	private MonoBehaviour target;

	public static MovementController GetInstance(){
		return instance;
	}

	public 	void SetTarget(MonoBehaviour target){
		if (target == null || target == this.target) {
			return;
		}
		this.target = target;
	}

	void updateShip ()
	{
		throw new System.NotImplementedException ();
	}

	void updatePlayer ()
	{
		/*var t = target as PlayerController;

		t.collider.transform.Rotate (0, Input.GetAxis ("Mouse X") *  sensitivityX, 0);
		var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

		if (t.collider.isGrounded) {
			moveDirection = t.transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			if (Input.GetButton ("Jump")) {
				moveDirection.y = jumpSpeed;
			}

		}
		moveDirection.y -= gravity * Time.deltaTime;
		t.collider.Move(moveDirection * Time.deltaTime);*/
	}

	public void Update ()
	{
		switch (target.tag) {
		case "Player":
			updatePlayer ();
			break;
		case "Ship":
			updateShip ();
			break;
		}
	}
}
