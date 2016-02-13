using UnityEngine;
using System.Collections;

/// <summary>
/// Link to Team Eider's website: https://eldertheduck.wordpress.com/
/// Link to Team Shelduck's website: https://shelduck.wordpress.com/
/// Link to Assessment 3 project version: https://eldertheduck.wordpress.com/assessment-3
/// </summary>

public class Location : MonoBehaviour {

	public string[] missionTag;
	bool visited = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider c) {
		if (c.tag == "Player") {
			if(!visited) {
				MissionManager.instance.addProgress(missionTag, 1);
				visited = true;
			}
		}
	}
}
