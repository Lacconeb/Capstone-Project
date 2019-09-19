using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SelectedCharacter
{
    private static int character;
    private static string name;

    public static int Character {
        get {
            return character;
        } set {
            character = value;
        }
    }

    public static string Name {
        get {
            return name;
        } set {
            name = value;
        }
    }    
}
