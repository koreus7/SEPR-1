using UnityEngine;
using System.Collections;

public class ApplyForce : MonoBehaviour {


	public Rigidbody rb;

	// Use this for initialization
	void Start () {
		Rigidbody rb = GetComponent<Rigidbody>();

		rb.AddForce (transform.forward * -200f, ForceMode.Impulse);

	

	}
	
	// Update is called once per frame
	void FixedUpdate () {
	

	}
}
