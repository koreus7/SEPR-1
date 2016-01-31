using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Link to website: https://shelduck.wordpress.com/
/// Link to executables: https://shelduck.wordpress.com/downloads/
/// </summary>

public class MissionManager : MonoBehaviour {

	//Handles singleton nature of the class
	public static MissionManager inst;
	public static MissionManager instance {
		get {
			if (inst == null) {
				inst = FindObjectOfType (typeof(MissionManager)) as MissionManager;
			}
			return inst;
		}
	}

	/// <summary>
	/// The length of the gameplay in minutes;
	/// </summary>
	public float gameplayLength = 5;

	/// <summary>
	/// All active missions.
	/// </summary>
	public List<Mission> missions = new List<Mission>();

	public List<Mission> pendingMissions = new List<Mission>();

	/// <summary>
	/// Used for internal structure of the class.
	/// </summary>
	public Dictionary<string, Mission> missionsDict = new Dictionary<string,Mission>();

	public int interval = 30;

	float missionTime;
	int missionListPointer = 1;

	// Use this for initialization
	void Start () {
		missionTime = interval;
		//build the mission dictionary
		/*
		foreach (Mission mission in missions) {
			missionsDict[mission.missionTag] = mission;
		}
		*/
		missionsDict [pendingMissions [0].tag] = pendingMissions [0];
		missions.Add (pendingMissions [0]);
		gameplayLength *= 60;
	}
	
	// Update is called once per frame
	void Update () {
		if (missionListPointer < pendingMissions.Count && Time.timeSinceLevelLoad >= missionTime) {
			missionsDict[pendingMissions[missionListPointer].missionTag] = pendingMissions[missionListPointer];
			missions.Add(pendingMissions[missionListPointer]);
			missionListPointer++;
			missionTime += interval;
			GUIHandler.instance.buildMissionTexts ();
		}
	}


	/// <summary>
	/// Progresses the specified mission.
	/// </summary>
	/// <param name="tag">Tag of the mission.</param>
	/// <param name="amount">Amount to progress by.</param>
	/// <param name="updateGUI">If set to <c>true</c> update GUI.</param>
	public void addProgress(string[] tag, int amount, bool updateGUI = true) {
		//update progress, ensure its between 0 and max progress
		foreach (string s in tag) {
			if(missionsDict.ContainsKey(s)) {
				missionsDict [s].progress = Mathf.Clamp (missionsDict[s].progress + amount, 0, missionsDict [s].completeProgress);
				missionsDict [s].checkProgress ();
			}
		}
		//missionsDict [tag].progress = Mathf.Clamp (missionsDict[tag].progress + amount, 0, missionsDict [tag].completeProgress);
		//check if mission has been completed
		//missionsDict [tag].checkProgress ();
		if (updateGUI) {
			GUIHandler.instance.updateMissions();
		}
	}

	public void replaceMission(string tag, Mission replacement, bool updateGUI = true) {
		missionsDict [tag] = replacement;
		if (updateGUI) {
			GUIHandler.instance.updateMissions();
		}
	}

}
