using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

[RequireComponent (typeof(ThirdPersonCharacter))]
public class ThirdPersonPlayerController : NetworkBehaviour, IPlayer
{
	#region private fileds
	private ThirdPersonCharacter character;
	private Vector3 moveDirection;
	private bool isJumping;
	#endregion

	#region IPlayer implementaiton
	public IShip Ship { get; set; }
	public Camera Camera { get; set; }
	#endregion

	private void Start ()
	{
		character = GetComponent<ThirdPersonCharacter> ();
		Camera = GetComponentInChildren<Camera> ();
		if (Camera == null) {
			throw new MissingComponentException ("Has no required child camera");
		}
	}

	private void Update ()
	{
		if (!isJumping) {
			isJumping = CrossPlatformInputManager.GetButtonDown ("Jump");
		}
		//process vertical axis camera look
		float mv = Input.GetAxis ("Mouse Y");
		Camera.transform.rotation = Quaternion.Euler(Camera.transform.rotation.eulerAngles.x - (mv * 2f), Camera.transform.rotation.eulerAngles.y, Camera.transform.rotation.eulerAngles.z);
	}

	private void FixedUpdate ()
	{
		// read inputs
		float h = CrossPlatformInputManager.GetAxis ("Mouse X");
		float v = CrossPlatformInputManager.GetAxis ("Vertical");

		bool crouch = Input.GetKey (KeyCode.LeftControl);
		//backward moving is not implemented yet
		if (v < 0)
			v = 0;
		//process interaction
		Interact (Input.GetKey (KeyCode.E));
		// pass all parameters to the character control script
		moveDirection = v * gameObject.transform.forward + h * gameObject.transform.right;
		character.Move (moveDirection, crouch, isJumping);
		isJumping = false;
	}

	void Interact (bool isInteractionCalled)
	{
		var lm = LayerMask.GetMask (FridayGameLayers.INTERACTABLE);
		RaycastHit hit;
		if (Physics.Raycast (transform.position, gameObject.transform.TransformDirection (Vector3.forward), out hit, 1.5f, lm)) {
			
			if (isInteractionCalled) {
				var target = hit.collider.gameObject;
				if (target.name.Equals ("ShipControl")) {
					GameController.PlayerShipInteraction (this, target.GetComponentInParent<ShipController> (), InteractionType.PlayerTakesShipControl);
				}
				if (target.name.Equals ("GunControl")) {
					GameController.PlayerShipInteraction (this, target.GetComponentInParent<ShipController> (), InteractionType.PlayerTakesShipGunsControl);
				}
			}
		}
	}
}
