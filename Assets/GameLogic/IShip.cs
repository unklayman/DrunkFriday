using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShip : IGameObject
{

	IPlayer Driver { get; set; }

	IPlayer Shooter{ get; set; }

	Camera Camera { get; set; }

	GunController GunController { get; set; }
}
