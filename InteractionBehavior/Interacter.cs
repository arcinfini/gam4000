/*

READ ME:

This behavior controls the playeres interaction with the surrounding objects
in the environment. This is meant to be a component of the player gameonject

General Setup:
	1. Set the player's camera to the cameraObject gameobject

*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour {

	private GameObject cameraObject;

	public void Update () {
		if (Input.GetKey(KeyCode.E)) {
			Transform cTransform = GetComponent<Camera>().transform;
			bool didHit = Physics.Raycast(
				cTransform.position, 
				cTransform.forward,
				out RaycastHit hit
			);

			if (didHit) {
				// Check if target has an interactable script
				// Call the method
				Interactable interactable;
				bool found = hit.collider.gameObject.TryGetComponent(out interactable);
				if (found) interactable.Interact();
			}
		}
	}
}