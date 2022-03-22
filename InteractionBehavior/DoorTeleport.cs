/*

READ ME: 

Transitions to the next scene

*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTeleport : Interactable {
    public int doorCode;

    public void Interact(GameObject player) {
        Debug.Log("interacted with door");
        Debug.Log(Pocket.HasKey(doorCode));

        if (Pocket.HasKey(doorCode)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
