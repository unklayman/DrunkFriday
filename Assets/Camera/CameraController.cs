using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;         //Private variable to store the offset distance between the player and camera

	private Vector3 startPosition;


	// Use this for initialization
	void Start () 
	{
		//Calculate and store the offset value by getting the distance between the player's position and camera's position.
		offset = new Vector3(2.0f,  0.0f, 0.0f);

		//transform.position = player.transform.position + offset;
	}

	void Update() {
		this.transform.rotation = new Quaternion(transform.rotation.x, player.transform.rotation.y, player.transform.rotation.z, transform.rotation.w);

		var zoomVector = new Vector3 ();
		var zoomScale = 2.0f;

		if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
		{
			zoomVector.z += zoomScale;
			//orthographicSize++;
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
		{
			zoomVector.z -= zoomScale;
		}

		transform.position += zoomVector;
		//Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, orthographicSizeMin, orthographicSizeMax );
	}

	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
		// Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
		//transform.position = player.transform.position + offset;
	}
}
