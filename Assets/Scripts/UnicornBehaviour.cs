using UnityEngine;
using System.Collections;

public class UnicornBehaviour : MonoBehaviour {

	/// <summary>
	/// Link to Team Eider's website: https://eldertheduck.wordpress.com/
	/// Link to Team Shelduck's website: https://shelduck.wordpress.com/
	/// Link to Assessment 3 project version: https://eldertheduck.wordpress.com/assessment-3
	/// </summary>

	Vector3 playerOrigionalPos;

	public Vector3 playerMoveToPosition;

	public GameObject player;

	public float DistanceToTrigger;

	private bool eventTriggered = false;

	public Camera mainCamera;
	public Camera gotoCamera;
	public GameObject unicornUI;
	public EnemyGoose eGoose;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		eventTriggered = false;
	}


	void Update () {
		//Debug.Log (transform.position + (transform.forward * 2 * Time.deltaTime));
		if (Input.GetKey (KeyCode.H)) {
			EndEvent();
		}
	}

	void FixedUpdate () {
		if (DistanceToPlayer() <= DistanceToTrigger && !eventTriggered) {
			StartEvent();
		}
	}

	/// <summary>
	/// Distance to player. (same as in AI.cs)
	/// </summary>
	float DistanceToPlayer () {
		return Mathf.Abs ((player.transform.position - this.transform.position).magnitude);
	}

	void StartEvent() {
		Time.timeScale = 0;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		//gotoCamera.gameObject.SetActive (true);
		unicornUI.SetActive (true);
		eventTriggered = true;
	}

	void EndEvent() {
		Time.timeScale = 1;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		//gotoCamera.gameObject.SetActive (false);
		unicornUI.SetActive (false);
	}

	public void KeepUnicorn() {
		Debug.Log ("Unicorn is kept");
		eGoose.health = 1500;
		eGoose.pointsForKill = 900;
		EndEvent ();
	}

	public void KillUnicorn () {
		Debug.Log ("RIP");
		transform.tag = "Enemy";
		EndEvent ();
	}

}
