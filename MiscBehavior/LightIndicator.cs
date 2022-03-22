
/*

READ ME:

Attach to the Light object and assign the door gameobject that
is meant to be represented by the light

*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIndicator : MonoBehaviour {
    public GameObject door;
    public Door doorscript;

    public void Start() {
        doorscript = door.GetComponent<Door>();
    }

    public void Update() {
        // Red when locked
        // Gree when unlocked
        bool hasKey = Pocket.HasKey(doorscript.doorCode);

        if (hasKey && door.activeInHierarchy) {
            // Set Yellow
        } else if (hasKey && !door.activeInHierarchy) {
            // Set Green
        } else {
            // Set Red
        }
    }
}