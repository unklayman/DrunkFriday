using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;


public class DeckTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider) {
		var player = collider.gameObject.GetComponent<ThirdPersonUserControl> ();

		if (player == null) {
			return;
		}

		var ship = GetComponentInParent<ShipController> ();
		player.transform.parent = ship.gameObject.transform;
	}

	void OnTriggerExit(Collider collider) {
		var player = collider.gameObject.GetComponent<ThirdPersonUserControl> ();

		if (player == null) {
			return;
		}

		var ship = GetComponentInParent<ShipController> ();
		player.transform.parent = null;
	}
}
