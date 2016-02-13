using UnityEngine;
using System.Collections;

/// <summary>
/// Link to Team Eider's website: https://eldertheduck.wordpress.com/
/// Link to Team Shelduck's website: https://shelduck.wordpress.com/
/// Link to Assessment 3 project version: https://eldertheduck.wordpress.com/assessment-3
/// </summary>

public class EnemyGoose : Enemy {

	/// <summary>
	/// Gameobject that holds the wings
	/// </summary>
	public GameObject wings;

	

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		rigid = this.GetComponent<Rigidbody> ();
		if (wings != null) {
			wings.SetActive (false);
		}
		if (Spawner.instance != null) {
			maxDistanceFromPlayer = Spawner.instance.enemySpawnRadius;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (state == State.Chasing) {
			//dont re-order these two methods. We must start the animation before activating the wings
			//otherwise it fails to play.
			anim.Play("chasing");
			if(wings != null) {
				wings.SetActive (true);
			}
		} else {
			anim.Play("walking");
			if(wings != null) {
				wings.SetActive (false);
			}
		}
	}

}
