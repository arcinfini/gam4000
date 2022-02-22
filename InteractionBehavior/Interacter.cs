/*

READ ME:

This behavior controls the playeres interaction with the surrounding objects
in the environment. This is meant to be a component of the player gameonject

*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour {

	private Camera cameraObject;

	public void Awake() {
		cameraObject = Camera.main;
	}

	public void Update () {
		if (Input.GetKey(KeyCode.E)) {
			Transform cTransform = GetComponent<Camera>().transform;
			bool didHit = Physics.Raycast(
				cTransform.position, 
				cTransform.forward,
				out RaycastHit hit
			);

			// Check if target has an interactable script
			// Call the method
			if (didHit) {
				Interactable interactable;
				bool found = hit.collider.gameObject.TryGetComponent(out interactable);
				if (found) interactable.Interact(gameObject);
			}
		}
	}
}