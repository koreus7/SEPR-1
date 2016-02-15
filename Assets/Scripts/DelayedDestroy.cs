using UnityEngine;
using System.Collections;

/// <summary>
/// Link to Team Eider's website: https://eldertheduck.wordpress.com/
/// Link to Team Shelduck's website: https://shelduck.wordpress.com/
/// Link to Assessment 3 project version: https://eldertheduck.wordpress.com/assessment-3
/// </summary>

// Destroys the game object after the destroyDelay period is over.
public class DelayedDestroy : MonoBehaviour {

	public float destroyDelay;

	// Use this for initialization
	void Start () {
		Invoke ("destroySelf", destroyDelay);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void destroySelf() {
		Destroy (this.gameObject);
	}
}
