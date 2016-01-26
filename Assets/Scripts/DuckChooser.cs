﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DuckChooser : MonoBehaviour {

	//singleton. Access with PlayerStates.instance. ...
	public static DuckChooser inst;
	public static DuckChooser instance {
		get {
			if (inst == null) {
				inst =  FindObjectOfType(typeof (DuckChooser)) as DuckChooser;
			}
			return inst;
		}
	}

	/// <summary>
	/// The index of the currently selected duck
	/// This effects the colors.
	/// </summary>
	public int duckColorIndex;

	public Color[] duckBeakColors;
	public Color[] duckBodyColors;
	public Color[] duckBodyAccentColors;
	public Color[] duckHeadColors;
	public string[] duckNames;

	public Material duckBeakMat;
	public Material duckBodyMat;
	public Material duckBodyAccentMat;
	public Material duckHeadMat;

	public Text duckName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void updateMaterials() {
		duckBeakMat.color = duckBeakColors [duckColorIndex];
		duckBodyMat.color = duckBodyColors [duckColorIndex];
		duckBodyAccentMat.color = duckBodyAccentColors [duckColorIndex];
		duckHeadMat.color = duckHeadColors [duckColorIndex];
		duckName.text = duckNames [duckColorIndex];
	}

	public void increaseIndex () {
		duckColorIndex++;
		if (duckColorIndex >= duckHeadColors.Length -1) {
			duckColorIndex = duckHeadColors.Length -1 ;
		}
		updateMaterials ();
	}

	public void decreaseIndex() {
		duckColorIndex--;
		if (duckColorIndex < 0) {
			duckColorIndex = 0;
		}
		updateMaterials ();
	}
}
