using UnityEngine;
using System.Collections;

/// <summary>
/// Link to Team Eider's website: https://eldertheduck.wordpress.com/
/// Link to Team Shelduck's website: https://shelduck.wordpress.com/
/// Link to Assessment 3 project version: https://eldertheduck.wordpress.com/assessment-3
/// </summary>

public class HealthPickup : MonoBehaviour {
	
	/// <summary>
	/// Health the player gains for collecting
	/// </summary>
	public int healthOnCollect;
	
	/// <summary>
	/// Gameobject that is instantiated when the player collides
	/// </summary>
	public GameObject pointsEffect;
	
	/// <summary>
	/// Does the object bob?
	/// </summary>
	public bool bob;
	
	/// <summary>
	/// Amount that thet object bobs by (top to bottom): units
	/// </summary>
	public float bobAmount;
	
	/// <summary>
	/// Speed at which the object bobs: units/second
	/// </summary>
	public float bobSpeed;
	
	Vector3 startPosition;
	
	GameObject player;
	
	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		//if we are bobbing, it rotates and bobs the object by the desired amount.
		if (bob) {
			Vector3 wantedPosition = startPosition;
			wantedPosition.y += Mathf.Sin (Time.time * bobSpeed) * bobAmount;
			transform.position = wantedPosition;
			transform.Rotate(new Vector3(0,20*Time.deltaTime,0));
		}
	}
	
	void LateUpdate () {
		if((this.transform.position - player.transform.position).magnitude > 50) {
			Spawner.instance.spawnCollectable();
			Destroy (this.gameObject);
		}
	}
	
	void OnTriggerEnter (Collider c) {
		if(c.transform.tag == "Player") {
			PlayerStates.instance.alterHealth(healthOnCollect);
			Spawner.instance.spawnCollectable();
			Destroy(this.gameObject);
		}
	}
}
