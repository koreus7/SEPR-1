using UnityEngine;
using System.Collections;

public class JumpWeapon : MonoBehaviour {

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
