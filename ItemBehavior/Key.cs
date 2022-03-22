/*

READ ME:

Attach this script to a gameobject in world that is meant to behave
as a collectable key

Set the number field to the code that should match with what door it
has the ability to open.

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable, Item {
    [Tooltip("The corresponding number id of the door this key will open")]
    public int number;

    public void Interact(GameObject player) {
        Pocket.GiveKey(this);
        gameObject.SetActive(false);
    }
}