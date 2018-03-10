using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		var player = other.gameObject.GetComponent<PlayerController> ();
		if (player == null) { 
			return;
		}

		var lookAtCamera = Camera.main.GetComponent(typeof(LookAtCamera)) as LookAtCamera;
		if (lookAtCamera != null) {
			lookAtCamera.SetTarget(this.gameObject);
		}
	}
}
