using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	void OnEnable(){
		EventManager.StartListening(EventType.PLAYER_REQUEST_SHIP_CONTROL,PlayerRequestShipControlHandler);
		EventManager.StartListening(EventType.PLAYER_LEAVES_SHIP_CONTROL,PlayerLeavesShipControl);
	}

	void OnDisable(){
		EventManager.StopListening(EventType.PLAYER_REQUEST_SHIP_CONTROL,PlayerRequestShipControlHandler);
		EventManager.StopListening(EventType.PLAYER_LEAVES_SHIP_CONTROL,PlayerLeavesShipControl);
	}

	private void PlayerRequestShipControlHandler(EventBase e){
		PlayerShipControlInteractionEvent ev = e as PlayerShipControlInteractionEvent;
		if (e == null) {
			return;
		}

		ShipController shipController = ev.Ship.GetComponent<ShipController> ();
		PlayerController playerController = ev.Player.GetComponent<PlayerController> ();
		if (shipController.Driver != null) {
			return;
		}
		shipController.Driver = ev.Player;
		playerController.Ship = ev.Ship;

		MainCameraController.GetInstance ().SetTargetFor<ShipCamera> (ev.Ship);
	}

	private void PlayerLeavesShipControl(EventBase e){
		PlayerShipControlInteractionEvent ev = e as PlayerShipControlInteractionEvent;
		if (e == null) {
			return;
		}

		ShipController shipController = ev.Ship.GetComponent<ShipController> ();
		PlayerController playerController = ev.Player.GetComponent<PlayerController> ();
		if (shipController.Driver == null) {
			return;
		}
		 
		ev.Player.transform.parent = null;

		shipController.Driver = null;
		playerController.Ship = null;
		MainCameraController.GetInstance ().SetTargetFor<PlayerCamera> (ev.Player);
	}
}
