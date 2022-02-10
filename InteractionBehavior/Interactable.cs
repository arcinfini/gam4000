/*

READ ME:

This interface is the basic framework of all interactable items in the
environment. The interacter behavior will check for a class with this
interface and call the Interact() method to activate the interaction.

The player is passed to the method to allow catching components of the
player to be altered.

*/

public interface Interactable {

	// Called when the player hits E on it
	// Contains to 
	void Interact(GameObject player);
}