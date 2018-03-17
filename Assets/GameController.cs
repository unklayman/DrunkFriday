using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	
	public static void PlayerShipInteraction(PlayerController player,ShipController ship,InteractionType type){
		if (type.Equals (InteractionType.PlayerTakesShipControl)) {
			ship.Driver = player;
			player.Ship = ship;
			player.PlayerCamera.enabled = false;
			ship.ShipCamera.enabled = true;
		}
		if (type.Equals (InteractionType.PlayerReleasesShipControl)) {
			ship.Driver = null;
			player.Ship = null;
			player.PlayerCamera.enabled = true;
			ship.ShipCamera.enabled = false;
		}
		if (type.Equals (InteractionType.PlayerTakesShipGunsControl)) {
			ship.Shooter = player;
			ship.GunController.Shooter = player; //todo to ship controller logic
			player.Ship = ship;
			player.PlayerCamera.enabled = false;
			ship.GunController.GunCamera.enabled = true;
		}
		if (type.Equals (InteractionType.PlayerReleasesShipGunsControls)) {
			ship.Shooter = null;
			ship.GunController.Shooter = null; //todo to ship controller logic
			player.Ship = null;
			player.PlayerCamera.enabled = true;
			ship.GunController.GunCamera.enabled = false;
		}

	}
}
