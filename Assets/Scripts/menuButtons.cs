using UnityEngine;
using System.Collections;

/// <summary>
/// Link to website: https://shelduck.wordpress.com/
/// Link to executables: https://shelduck.wordpress.com/downloads/
/// </summary>

public class MenuButtons : MonoBehaviour {

    public GameObject highScoresPanel;

	void Start() {		
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
	public void pressPlay () {
		Application.LoadLevel (1);
	}

    public void pressHighScores() {
        highScoresPanel.GetComponent<HighScoresGUI>().openPanel();
    }

}
