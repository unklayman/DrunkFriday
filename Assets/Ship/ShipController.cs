using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

	public Camera shipCamera;



	// Use this for initialization
	void Start () {
		//this.shipCamera = this.gameObject.GetComponent<Camera> ();
		this.shipCamera.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		var player = other.gameObject.GetComponent<PlayerController> ();
		if (player == null) { 
			return;
		}
		var playerCamera = Camera.main;
		playerCamera.enabled = false;
		shipCamera.enabled = true;
	}
}
