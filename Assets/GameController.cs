using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour {
	
	public static void PlayerShipInteraction(IPlayer player, IShip ship,InteractionType type){
		if (type.Equals (InteractionType.PlayerTakesShipControl)) {
			ship.Driver = player;
			player.Ship = ship;
			player.Camera.enabled = false;
			ship.Camera.enabled = true;
		}
		if (type.Equals (InteractionType.PlayerReleasesShipControl)) {
			ship.Driver = null;
			player.Ship = null;
			player.Camera.enabled = true;
			ship.Camera.enabled = false;
		}
		if (type.Equals (InteractionType.PlayerTakesShipGunsControl)) {
			ship.Shooter = player;
			ship.GunController.Shooter = player; //todo to ship controller logic
			player.Ship = ship;
			player.Camera.enabled = false;
			ship.GunController.GunCamera.enabled = true;
		}
		if (type.Equals (InteractionType.PlayerReleasesShipGunsControls)) {
			ship.Shooter = null;
			ship.GunController.Shooter = null; //todo to ship controller logic
			player.Ship = null;
			player.Camera.enabled = true;
			ship.GunController.GunCamera.enabled = false;
		}
	}
}
