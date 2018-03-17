using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {

	void OnCollisionEnter(Collision collision){
		if (collision.rigidbody != null ) {
			var damageable = collision.rigidbody.gameObject.GetComponent<IDamageable> ();
			if (damageable != null) {
				damageable.DoDamage (GetDamageAmount());
			}
		}
		Destroy(gameObject);
	}

	protected abstract int GetDamageAmount ();
}
