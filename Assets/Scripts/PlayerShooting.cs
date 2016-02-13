using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Link to Team Eider's website: https://eldertheduck.wordpress.com/
/// Link to Team Shelduck's website: https://shelduck.wordpress.com/
/// Link to Assessment 3 project version: https://eldertheduck.wordpress.com/assessment-3
/// </summary>

public class PlayerShooting : MonoBehaviour {

	/// <summary>
	/// The projectile.
	/// </summary>
	public GameObject projectile;

	/// <summary>
	/// The projectile speed.
	/// </summary>
	public float projectileSpeed;

	/// <summary>
	/// The fire rate.
	/// </summary>
	public float fireRate;

	/// <summary>
	/// The lasor prefab, if this weapon is enabled.
	/// </summary>
	public GameObject lasor;

	//used to track if the player is shooting too fast.
	float lastfireTime = 0;

	float lazorCostTime;
	public float lazorCostInterval;

	//active bread shooter
	public bool breadShoot = false;

	//active lazer shooter
	public bool lazerShoot = false;


	// Use this for initialization
	void Start () {
		lasor.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		//When we shoot and havent recently shot, instantiate a projectile, give it forwards velocity, and update the GUI and the PlayerStates resources.
		if (Input.GetButton ("Fire1") && Time.time >= lastfireTime + fireRate) {
			if(PlayerStates.instance.resources >= 0) {
				GameObject g = (GameObject)Instantiate(projectile, transform.position + transform.forward, transform.rotation);
				g.GetComponent<Rigidbody>().velocity = transform.forward*projectileSpeed;
				lastfireTime = Time.time;
				PlayerStates.instance.alterResources(-1);
				GUIHandler.instance.updateResourceText(PlayerStates.instance.resources.ToString(), "-1", true);
			}
		}

		//Bread shooter. NEw assessment 3. Shoots array of projectiles when you click E.
		if (Input.GetKey (KeyCode.E) && Time.time >= lastfireTime + fireRate) {
			if(PlayerStates.instance.resources >= 1) {
				if (breadShoot) {
					for(int i = 0; i < 10; i++) {
						GameObject g = (GameObject)Instantiate(projectile, transform.position + transform.forward*(10-i), transform.rotation);
						g.GetComponent<Rigidbody>().velocity = transform.forward*projectileSpeed+(transform.right*(5-i));
						lastfireTime = Time.time;
					}
					PlayerStates.instance.alterResources(-2);
					GUIHandler.instance.updateResourceText(PlayerStates.instance.resources.ToString(), "-2", true);
				}
			}
		}

		//lazer weapon. (Updated assesment 3)Enables and disables the lasor object on the player.
		if (Input.GetKeyDown (KeyCode.Q)) {
			if (lazerShoot) {
				lasor.SetActive (true);
				lazorCostTime = Time.time;
			}
		}
		if (Input.GetKey (KeyCode.Q)) {
			if (lazerShoot){
				if(Time.time >= lazorCostTime) {
					PlayerStates.instance.alterResources(-1);
					GUIHandler.instance.updateResourceText(PlayerStates.instance.resources.ToString(), "-1", true);
					lazorCostTime = Time.time + lazorCostInterval;
				}
			}
		} else if (Input.GetKeyUp (KeyCode.Q)) {
			lasor.SetActive(false);
		}
	}
}
