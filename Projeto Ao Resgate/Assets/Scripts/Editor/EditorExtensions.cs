//Maded by Pedro M Marangon
using UnityEngine;
using UnityEditor;

public class EditorExtensions : EditorWindow {

	[MenuItem("Edit/Delet All PlayerPrefs")]
	private static void DeletePlayerPrefs() {
		PlayerPrefs.DeleteAll();
	}

}
