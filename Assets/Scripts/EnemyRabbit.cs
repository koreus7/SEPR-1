using UnityEngine;
using System.Collections;

/// <summary>
/// Link to Team Eider's website: https://eldertheduck.wordpress.com/
/// Link to Team Shelduck's website: https://shelduck.wordpress.com/
/// Link to Assessment 3 project version: https://eldertheduck.wordpress.com/assessment-3
/// </summary>

public class EnemyRabbit : Enemy {

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		rigid = this.GetComponent<Rigidbody> ();
		maxDistanceFromPlayer = Spawner.instance.enemySpawnRadius;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == State.Chasing) {
			anim.Play ("chasing");
		} else {
			anim.Play("walking");
		}
	}
}
