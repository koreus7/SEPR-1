using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Link to Team Eider's website: https://eldertheduck.wordpress.com/
/// Link to Team Shelduck's website: https://shelduck.wordpress.com/
/// Link to Assessment 3 project version: https://eldertheduck.wordpress.com/assessment-3
/// </summary>

public class UpscrollingText : MonoBehaviour {

	/// <summary>
	/// Text to show
	/// </summary>
	public string text;

	/// <summary>
	/// Destroys after this time (seconds)
	/// </summary>
	public float lifetime;

	/// <summary>
	/// Color of the text
	/// </summary>
	public Color textColor = new Color (255, 255, 255, 255);

	/// <summary>
	/// Text initial velocity on the UI.
	/// </summary>
	public Vector3 movementVector = new Vector3 (0, 150, 0);

	//private variabeles for assigning things.
	Text textComponent;
	Color faded;

	public UpscrollingText(string text) {
		this.text = text;
	}

	// Use this for initialization
	void Start () {
		//parents to canvas so its drawn, then assignes all of the initial values.
		this.transform.SetParent (GameObject.FindGameObjectWithTag ("Canvas").transform);
		textComponent = this.GetComponent<Text> ();
		textComponent.text = text;
		textComponent.color = textColor;
		faded = textColor;
		faded.a = 0;
		//invokes a call to the kill function, which destroys this object
		Invoke ("kill", lifetime);
	}
	
	// Update is called once per frame
	void Update () {
		//mvoes updards and interpolates the color constantly (1/curr - wanted as time)
		transform.Translate (movementVector * Time.deltaTime);
		textComponent.color = Color.Lerp (textComponent.color, faded, 1 / (textComponent.color.a - faded.a) * Time.deltaTime);
	}

	void kill() {
		Destroy (this.gameObject);
	}
}
