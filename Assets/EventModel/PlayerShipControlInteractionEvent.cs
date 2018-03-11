using System;
using UnityEngine;


public class PlayerShipControlInteractionEvent : EventBase
{
	private GameObject player;
	private GameObject ship;
	public PlayerShipControlInteractionEvent (GameObject player,GameObject ship)
	{
		this.player = player;
		this.ship = ship;
	}

	public GameObject Ship {get {return ship;}}
	public GameObject Player {get {return player;}}

}