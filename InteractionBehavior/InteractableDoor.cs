public class InteractableDoor : Interactable {
	private bool state = false;

	public void Interact() {
		if (state == false) OpenDoor();
		else CloseDoor();
	}

	private void OpenDoor() {
		
	}

	private void CloseDoor() {

	}
}