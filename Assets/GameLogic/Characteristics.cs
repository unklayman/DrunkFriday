using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characteristics : MonoBehaviour , IDamageable {

	private int maxHealth = 100;
	private int health = 100;

	public int MaxHealth {get;set;}
	public int Health {
		get
		{ 
			return health;
		}
		set
		{ 
			if (value > maxHealth) {
				health = maxHealth;
				return;
			}
			if (value <= 0) {
				Destroy (gameObject);
			}
			health = value;
		}
	}

	#region IDamageable implementation

	public void DoDamage (int amount)
	{
		Health -= amount;
	}

	#endregion
}
