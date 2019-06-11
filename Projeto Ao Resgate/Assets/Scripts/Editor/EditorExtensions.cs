//Maded by Pedro M Marangon
using UnityEngine;
using UnityEditor;

public class EditorExtensions {

	[MenuItem("Edit/Delet All PlayerPrefs")]
    private static void DeletePlayerPrefs(){
        PlayerPrefs.DeleteAll();
    }
    
}
