using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallController : Projectile {

	private int damage = 500;

	#region implemented abstract members of Projectile

	protected override int GetDamageAmount ()
	{
		return damage;
	}

	#endregion
}
