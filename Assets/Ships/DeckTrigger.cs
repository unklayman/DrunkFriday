using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;


public class DeckTrigger : MonoBehaviour {

	public ShipController Ship;

	// Use this for initialization
	void Start () {
		Ship = gameObject.transform.parent.gameObject.GetComponentInParent<ShipController> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider collider) {
		var player = collider.gameObject.GetComponent<ThirdPersonPlayerController> ();
		if (player == null) {
			return;
		}
		player.transform.parent = gameObject.transform;
	}

	void OnTriggerExit(Collider collider) {
		var player = collider.gameObject.GetComponent<ThirdPersonPlayerController> ();

		if (player == null) {
			return;
		}
		player.transform.parent = null;
	}
}
