using System;
using UnityEngine;


public class PlayerShipControlInteractionEvent : EventBase
{
	public GameObject ShipPart {get {return shipPart;}}
	public GameObject Player {get {return player;}}

	private GameObject player;
	private GameObject shipPart;

	public PlayerShipControlInteractionEvent (GameObject player,GameObject shipPart)
	{
		this.player = player;
		this.shipPart = shipPart;
	}
}