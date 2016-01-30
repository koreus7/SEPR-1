using UnityEngine;
using System.Collections;

public class JumpWeapon : MonoBehaviour {

	public Rigidbody playerRigidBody;


	void OnTriggerEnter (Collider c) {
		Debug.Log ("FUCK" + c.gameObject.name);
		if (c.transform.tag == "Enemy")
		{
			Enemy e = c.gameObject.GetComponent<Enemy> ();
			e.decreaseHealth (e.health);
		}
	}
}
