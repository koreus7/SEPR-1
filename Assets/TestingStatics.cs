using UnityEngine;
using System.Collections;

public class TestingStatics : MonoBehaviour {

	public GameObject breadPrefab;

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


	// Use this for initialization
	void Start () {
		
	
	}
		
	
	// Update is called once per frame
	void Update () {
	
	}
}
