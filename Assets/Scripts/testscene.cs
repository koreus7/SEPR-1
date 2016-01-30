using UnityEngine;
using System.Collections;

/// <summary>
/// Link to website: https://shelduck.wordpress.com/
/// Link to executables: https://shelduck.wordpress.com/downloads/
/// </summary>

public class testscene : MonoBehaviour {

	void Update () {
		if (PlayerStates.instance.health > 0) {
			Debug.Log ("Passed: Player has health, game still running");
		} else {
			if(Time.timeScale >= 0.5) {
				Debug.Log ("Failed: Health dropped below zero, Game still running at full speed, game not ended.");
			} else {
				Debug.Log ("Passed: Health dropped below zero, game time-scale altered, end of game detected.");
			}
		}

		if (PlayerStates.instance.resources == 0) {
			Debug.Log ("Shooting should not work.");
		} else {
			Debug.Log ("Player should be able to shoot");
		}
	}

}
