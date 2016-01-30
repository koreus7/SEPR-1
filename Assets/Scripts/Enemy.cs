using UnityEngine;
using System.Collections;

/// <summary>
/// Link to website: https://shelduck.wordpress.com/
/// Link to executables: https://shelduck.wordpress.com/downloads/
/// </summary>

public class Enemy : AI {

	/// <summary>
	/// Points awarded for killing this enemy
	/// </summary>
	public int pointsForKill;

	public int damageToPlayer;

	public float hitRate;

	float lastDamageTime;

	/// <summary>
	/// Mission that killing this enemy progresses' tag.
	/// </summary>
	public string[] missionProgressTags;

	//range to re-spawn the enemy.
	protected int maxDistanceFromPlayer;

	/// <summary>
	/// Despawn this enemy and re-spawn a random new enemy
	/// when the player is out of range.
	/// </summary>
	public bool useEfficiency = true;

	// Use this for initialization
	void Start () {
		lastDamageTime = Time.time;
		player = GameObject.FindGameObjectWithTag ("Player");
		rigid = this.GetComponent<Rigidbody> ();
	}
	
	//once per physics update
	void FixedUpdate () {
		Move ();
		//if enemy goes out of range, then position it somewhere in range and show the spawn effect
		if (Spawner.instance != null && useEfficiency) {
			if ((transform.position - player.transform.position).magnitude > maxDistanceFromPlayer) {
				Vector3 spawnPos = Spawner.instance.radiusAboutPlayer (Spawner.instance.enemySpawnRadius, 1, 50);
				Instantiate (Spawner.instance.enemySpawnEffect, spawnPos, Quaternion.identity);
				this.transform.position = spawnPos;
			}
		}
	}
	
	/// <summary>
	/// Health = Health - change
	/// </summary>
	/// <param name="change">Change.</param>
	public void decreaseHealth(int change) {
		health -= change;
		if (health <= 0) {
			PlayerStates.instance.alterPoints(pointsForKill);
			Spawner.instance.enemyKilled ();
			MissionManager.instance.addProgress(missionProgressTags, 1);
			Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter (Collision c) {
		if (c.transform.tag == "Player" && Time.time >= lastDamageTime) {

			PlayerStates.instance.alterHealth (-damageToPlayer);
			lastDamageTime = Time.time + hitRate;
		}
	}

}
