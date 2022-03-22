/*

READ ME:

This behavior controls the playeres interaction with the surrounding objects
in the environment. This is meant to be a component of the player gameonject

*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour {

    public GameObject message;
    public float interactDistance = 1f;

    private Interactable[] items;
    private Camera cameraObject;

    public void Awake() {
        items = FindObjectsOfType(typeof(Interactable)) as Interactable[];
        cameraObject = Camera.main;
    }

    public void Update () {
        if (!WithinInteractableDistance()) return;

        // Check if target has an interactable script
        // Call the method
        if (CastToCamera(out RaycastHit hit)) {
            Interactable interactable;
            bool found = hit.collider.gameObject.TryGetComponent(out interactable);

            message.SetActive(found);

            if (found && Input.GetKeyDown(KeyCode.E)) {
                interactable.Interact(gameObject);
            }
        } else {
            message.SetActive(false);
        }
    }

    // Checks all interactable items to determine if one is within range
    public bool WithinInteractableDistance() {
        foreach (Interactable item in items) {
            float distance = Vector3.Distance(item.transform.position, transform.position);

            if (distance <= interactDistance) {
                return true;
            }
        }

        return false;
    }

    // Sends a raycast from the cameras position directly outward
    public bool CastToCamera(out RaycastHit hit) {
        Transform cTransform = cameraObject.transform;
        bool didHit = Physics.Raycast(
            cTransform.position,
            cTransform.forward,
            out hit,
            interactDistance
        );

        return hit;
    }
}