public class Interacter : MonoBehaviour {
	[SerializedField]
	private GameObject camera;

	public void Update () {
		if (Input.GetKey(KeyCode.E)) {
			Transform cTransform = camera.transform;
			bool didHit = Physics.Raycast(
				cTransform.position, 
				cTransform.forward,
				out RaycastHit hit
			);

			if (didHit) {
				// Check if target has an interactable script
				// Call the method
				bool found = hit.collider.gameObject.TryGetComponent(
					typeof(Interactable), out Interactable interactable
				);
				if (found) interactable.Interact();
			}
		}
	}
}