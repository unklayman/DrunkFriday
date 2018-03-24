using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeckTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider) {
		var player = collider.gameObject.GetComponent<PlayerController> ();

		if (player == null) {
			return;
		}

		var ship = GetComponentInParent<ShipController> ();

		GameController.PlayerShipInteraction (player, ship, InteractionType.PlayerStayOnShip);
	}

	void OnTriggerExit(Collider collider) {
		var player = collider.gameObject.GetComponent<PlayerController> ();

		if (player == null) {
			return;
		}

		var ship = GetComponentInParent<ShipController> ();

		GameController.PlayerShipInteraction (player, ship, InteractionType.PlayerLeaveAShip);
	}
}
