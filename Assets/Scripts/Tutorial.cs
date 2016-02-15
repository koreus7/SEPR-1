using UnityEngine;
using System.Collections;


public class Tutorial : MonoBehaviour {

	// panel explaining the game at the start
	public GameObject tutorialPanel;
	//removes problem with pause menu clashing
	public static bool started = false;



	// Use this for initialization
	// opens tutorial panel to explain game and pauses game
	void Start () {
		Time.timeScale = 0;
		tutorialPanel.SetActive (true);
		Cursor.lockState = CursorLockMode.None;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Unpauses game, removes tutorial Panel, to enter play mode
	public void PlayButton() {
		tutorialPanel.SetActive (false);
		Time.timeScale = 1;
		Cursor.lockState = CursorLockMode.Locked;
		started = true;

	}







}
