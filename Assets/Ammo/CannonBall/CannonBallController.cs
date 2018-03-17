using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallController : MonoBehaviour {

	public float Damage = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision){
		if (collision.rigidbody != null && collision.rigidbody.gameObject.tag == "Ship") {
			GameController.Damage (collision.rigidbody.gameObject, 1);
		}
		Destroy(gameObject);
	}
}
