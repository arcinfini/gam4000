/*

READ ME:

Attach this script to a gameobject in world that is meant to behave
as a collectable key

Set the number field to the code that should match with what door it
has the ability to open.

*/


public class Key : MonoBehaviour, Item, Interactable {
    [Tooltip("The corresponding number id of the door this key will open")]
    public int number {private set; get;}

    public Key(int number) {
        this.number = number;
    }

    public void Interactable(GameObject player) {
        Pocket pocket = player.GetComponent<Pocket>();

        pocket.GiveKey(this);
        gameObject.SetActive(false);
    }
}