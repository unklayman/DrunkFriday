using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile {

	private int damage = 50;

	#region implemented abstract members of Projectile

	protected override int GetDamageAmount ()
	{
		return damage;
	}

	#endregion


}
