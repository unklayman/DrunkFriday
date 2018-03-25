﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer : IGameObject
{

	IShip Ship { get; set; }

	Camera Camera { get; set; }
}
