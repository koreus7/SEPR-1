using UnityEngine;
using System.Collections;

public class JumpWeapon : MonoBehaviour {

	/// <summary>
	/// Script that implements effects
	/// that jumping on enemies has.
	/// </summary>

	/// <summary>
	/// Link to Team Eider's website: https://eldertheduck.wordpress.com/
	/// Link to Team Shelduck's website: https://shelduck.wordpress.com/
	/// Link to Assessment 3 project version: https://eldertheduck.wordpress.com/assessment-3
	/// </summary>

	public Rigidbody playerRigidBody;
    public PlayerController playerController;

	void OnTriggerEnter (Collider c) {
		if (c.transform.tag == "Enemy")
		{
			Enemy e = c.gameObject.GetComponent<Enemy> ();
			e.decreaseHealth (e.health);
            playerController.jump(10);
		}
	}
}
