using UnityEngine;
using System.Collections;

/// <summary>
/// Link to Team Eider's website: https://eldertheduck.wordpress.com/
/// Link to Team Shelduck's website: https://shelduck.wordpress.com/
/// Link to Assessment 3 project version: https://eldertheduck.wordpress.com/assessment-3
/// </summary>

public class Mission : MonoBehaviour {

	public bool complete = false;

	public string missionDescription;

	public string missionText;

	public string missionTag;

	public int progress;

	public int completeProgress;

	public int pointsForComplete;

	public void checkProgress () {
		if(progress >= completeProgress && complete == false) {
			complete = true;
			PlayerStates.instance.alterPoints(pointsForComplete, true);
		}
	}

}
