using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
	private Camera camera;
	// Use this for initialization
	void Start () {
		if (!isLocalPlayer) {
			return;
		}
		camera = Camera.main;
		var lookAtCamera = camera.GetComponent(typeof(LookAtCamera)) as LookAtCamera;

		if (lookAtCamera != null) {
			lookAtCamera.target = this.gameObject;
		}


	}
	
	void Update()
	{
		if (!isLocalPlayer)
		{
			return;
		}

		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);


		//camera.transform.position = transform.position;
		//camera.transform.RotateAround(transform.position, new Vector3(-3.0f,0,0), 0.0f);

	}

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.blue;
	}
}
