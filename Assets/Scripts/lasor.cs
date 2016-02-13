using UnityEngine;
using System.Collections;

/// <summary>
/// Link to Team Eider's website: https://eldertheduck.wordpress.com/
/// Link to Team Shelduck's website: https://shelduck.wordpress.com/
/// Link to Assessment 3 project version: https://eldertheduck.wordpress.com/assessment-3
/// </summary>

public class lasor : MonoBehaviour {

	/// <summary>
	/// The fire rate.
	/// </summary>
	public float fireRate;

	/// <summary>
	/// The damage per tick of fireRate
	/// </summary>
	public int damage;

	//used to track the damage rate of the lasor
	float lastTime;

	void OnTriggerEnter (Collider c) {
		if (c.transform.tag == "Enemy" && Time.time >= lastTime) {
			c.gameObject.GetComponent<Enemy> ().decreaseHealth (damage);
			lastTime = Time.time;
		}
	}
}
