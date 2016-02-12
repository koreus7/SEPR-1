using UnityEngine;
using System.Collections;

/// <summary>
/// Testing statics.
/// 
/// Gives the unit tests access to gameobjects in the scene.
/// </summary>
public class TestingStatics : MonoBehaviour 
{

	public GameObject player;

	public static TestingStatics inst;
	public static TestingStatics instance {
		get
		{
			if (inst == null) {
				inst =  FindObjectOfType(typeof (TestingStatics)) as TestingStatics;
			}
			return inst;
		}
	}


	public PlayerStates GetPlayerStates()
	{
		return gameObject.GetComponent<PlayerStates> ();
	}

	public PlayerController GetPlayerController()
	{
		return player.GetComponent<PlayerController> ();
	}


    //Wait for all the scripts we want to test to be initialised.
	public void WaitForInit()
	{
		while (IsInit()) {}
		return;
	}

	//Check if all script references we need are not null.
	public bool IsInit()
	{
		if (GetPlayerStates () == null)
		{
			return false;
		}

		if (GetPlayerController () == null)
		{
			return false;
		} 

		return true;
	}

	// Use this for initialization
	void Start () {
		
	
	}
		
	
	// Update is called once per frame
	void Update () {
	
	}
}
