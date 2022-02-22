/*

READ ME:

This behavior is meant to control opening and closing doors. The general
behavior pathway is as following:

E (CLOSED) --> If locked then unlock and open door
E (OPEN) --> Close door

Clearly so far I don't actually have the door models so I can't program
any specific behavior with the doors we have currently.

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : Interactable {
	[Tooltip("The corresponding key number that will unlock the door")]
	[SerializeField]
	private int keyNumber;

	public int KeyNumber { get { return keyNumber; } }
	private bool state = false;

	public void Interact(GameObject player) {
		if (state == false) OpenDoor();
		else CloseDoor();
	}

	private void OpenDoor() {
		state = true;
	}

	private void CloseDoor() {
		state = false;
	}
}