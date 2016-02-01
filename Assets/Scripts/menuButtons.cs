using UnityEngine;
using System.Collections;

/// <summary>
/// Link to website: https://shelduck.wordpress.com/
/// Link to executables: https://shelduck.wordpress.com/downloads/
/// </summary>

public class menuButtons : MonoBehaviour {
	void Start() {		
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
	public void pressPlay () {
		Application.LoadLevel (1);
	}

}
