/*

READ ME:

Attach this script to the player gameobject

Nothing else should be touched, this is mainly backend stuff.

*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket {
    
    private static ArrayList keys = new ArrayList();

    public static bool HasKey(int keyNumber) {
        foreach (int key in keys) {
            if (key == keyNumber) { return true; }
        }
        return false;
    }

    public static void GiveKey(Key key) {
        keys.Add(key.number);
    }
}