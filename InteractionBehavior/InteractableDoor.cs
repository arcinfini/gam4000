/*

READ ME:

This behavior is meant to control opening and closing doors. The general
behavior pathway is as following:

E (CLOSED) --> If locked then unlock and open door
E (OPEN) --> Close door

*/


public class InteractableDoor : Interactable {
	[Tooltip("The corresponding key number that will unlock the door")]
	public int KeyNumber { get; }
	private bool state { get; private set;}= false;

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