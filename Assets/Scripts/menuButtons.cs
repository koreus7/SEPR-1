using UnityEngine;
using System.Collections;

/// <summary>
/// Link to Team Eider's website: https://eldertheduck.wordpress.com/
/// Link to Team Shelduck's website: https://shelduck.wordpress.com/
/// Link to Assessment 3 project version: https://eldertheduck.wordpress.com/assessment-3
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
