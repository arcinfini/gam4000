
/*

READ ME:

Attach to a door object to enable its destruction on unlock

Set the doorcode to the respective key code that should unlock
the door

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable {
    // public Transform point1;
    // public Transform point2;
    public int doorCode;

    public void Interact(GameObject player) {
        if (Pocket.HasKey(doorCode)) {
            SetActive(false);
        }
    }
}
