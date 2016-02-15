using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Link to Team Eider's website: https://eldertheduck.wordpress.com/
/// Link to Team Shelduck's website: https://shelduck.wordpress.com/
/// Link to Assessment 3 project version: https://eldertheduck.wordpress.com/assessment-3
/// </summary>

public class PlayerStates : MonoBehaviour {

	//singleton. Access with PlayerStates.instance. ...
	public static PlayerStates inst;
	public static PlayerStates instance {
		get
        {
			if (inst == null) {
				inst =  FindObjectOfType(typeof (PlayerStates)) as PlayerStates;
			}
			return inst;
		}
	}


	public enum State {
		Flying, Walking, Swimming, Falling
	}
		

	public enum PowerUpState
	{
		/// <summary>
		/// The player does not have a powerup.
		/// </summary>
		None,

		/// <summary>
		/// The player is invincible and moves faster.
		/// </summary>
		Invincible,

		/// <summary>
		/// The player gets bigger and can jump on enemies to kill them.
		/// </summary>
		Shroomed
	}

	public int health = 100;

	/// <summary>
	/// Current state of the player.
	/// </summary>
	public State currentState;

    public PlayerController playerController;


    /// <summary>
    /// Current powerup state of the player.
    /// </summary>
    public PowerUpState currentPowerupState;


    /// <summary>
    /// Current points the player has.
    /// </summary>
    public int points;

	/// <summary>
	/// Current resources the player has.
	/// </summary>
	public int resources;

	/// <summary>
	/// Current energy the player has.
	/// </summary>
	public int energy = 50;

	/// <summary>
	/// Energy increaes rate of the player when grounded.
	/// </summary>
	public float energyIncreaseRate;

	/// <summary>
	/// Energy drain rate when flying.
	/// </summary>
	public float energyFlyingDecreaseRate;

	public Text statusText;
	bool shownLazerText = false;
	bool shownBreadText = false;

	//private variable for handling gradual energy increase.
	float lastIncreaseTime;

	int geeseKilled = 0;
	int rabbitsKilled = 0;
	int breadCollected = 0;

	public float timeHealthAbove90;

	bool ended = false;


	// Use this for initialization
	void Start () {
		//must be set to start the energy handling.
		lastIncreaseTime = energyIncreaseRate;

		//ensures GUI is in sync with energy.
		GUIHandler.instance.updateEnergyBar (energy);
		GUIHandler.instance.updateResourceText (resources.ToString (), "+"+resources.ToString ());
		timeHealthAbove90 = 0;
	}

	bool HasHealthMission() {
		bool retVal = false;
		foreach (Mission m in MissionManager.inst.missions) {
			if (m.missionTag == "highhealth") {
				retVal = true;
			}
		}
		return retVal;
	}

	// Update is called once per frame
	void Update () {
		gradualEnergyChange ();
		GUIHandler.instance.updateHealthBar(health);
		if (PlayerStates.inst.health >= 90 && HasHealthMission()) {
			timeHealthAbove90 += Time.deltaTime;
			if (timeHealthAbove90 >= 30) {
				string[] thisTag = new string[1];
				thisTag [0] = "highhealth";
				MissionManager.inst.addProgress (thisTag, 1);
			}
		}
		if (Time.timeSinceLevelLoad > MissionManager.inst.gameplayLength && !ended) {
			points += resources;
			ended = true;
			GUIHandler.instance.updatePointsText (points.ToString(), "+"+resources.ToString ());
			GUIHandler.instance.updateResourceText("0",(-resources).ToString () );
			resources = 0;
			//uncomment following line to go to main menu at the end of the game
			//Application.LoadLevel("mainmenu");
		}
	}

	/// <summary>
	/// Alters the points.
	/// </summary>
	/// <param name="amount">Amount to add.</param>
	/// <param name="mission">If set to <c>true</c> GUIUpdate's accodringly.</param>
	/// <param name="updateGUI">If set to <c>true</c> update GUI.</param>
	public void alterPoints(int amount, bool mission = false, bool updateGUI = true) {
		points += amount;
		if (updateGUI) {
			if(!mission) {
				GUIHandler.instance.updatePointsText (points.ToString (), "+" + amount.ToString ());
			} else {
				GUIHandler.instance.updatePointsText (points.ToString(), "+" + amount.ToString(), true);
			}
		}
	}

	/// <summary>
	/// Alters the players energy. Energy = Energy + amuount
	/// </summary>
	/// <param name="amount">Amount to add.</param>
	public void alterEnergy (int amount) {
		//energy must be between 0 and 100.
		energy = Mathf.Clamp (energy + amount, 0, 100);
	}


	public void alterHealth(int amount) {
		
        //health must be between 0 and 100
		if (currentPowerupState != PowerUpState.Invincible)
        {
            health = Mathf.Clamp(health + amount, 0, 100);
            GUIHandler.instance.updateHealthBar(health);
            if (health == 0)
            {
                GUIHandler.instance.updateGameOver();
            }
		} else if(amount >= 0) {
			health = Mathf.Clamp(health + amount, 0, 100);
			GUIHandler.instance.updateHealthBar(health);
			if (health == 0)
			{
				GUIHandler.instance.updateGameOver();
			}
		}

	}

	public void alterResources(int amount) {
		resources += amount;
		if (resources >= 50 && !shownBreadText) {
			shownBreadText = true;
			CancelInvoke("ResetStatusText");
			statusText.text = "You can buy bread powerup";
			statusText.gameObject.SetActive(true);
			Invoke ("ResetStatusText", 5);
		}
		if (resources >= 60 && !shownLazerText) {
			shownLazerText = true;
			CancelInvoke("ResetStatusText");
			statusText.text = "You can buy lazer powerup";
			statusText.gameObject.SetActive(true);
			Invoke ("ResetStatusText", 5);
		}
	}

	void ResetStatusText() {
		statusText.text = "";
		statusText.gameObject.SetActive (false);
	}

	/// <summary>
	/// Sets the player state.
	/// </summary>
	/// <param name="st">State to change to (PlayerStates.State....)</param>
	public void setState(State st) {
		currentState = st;
		lastIncreaseTime = Time.time;
	}


    /// <summary>
    /// Collect a powerup state.
    /// None cannot be collected.
    /// </summary>
    /// <param name="powerup"></param>
    public void collectPowerup(PowerUpState powerup)
    {
        if (powerup != PowerUpState.None && powerup != currentPowerupState){
            currentPowerupState = powerup;
            playerController.onPowerupStateChanged(currentPowerupState);
        }
    }

    public void cancelPowerups()
    {
        currentPowerupState = PowerUpState.None;

        playerController.onPowerupStateChanged(currentPowerupState);    
    }

	/// <summary>
	/// Handles player energy, decreases when flying and increases when grounded.
	/// </summary>
	void gradualEnergyChange () {
		//inside each if statement is an if(Time.time >= lastincreasetime).
		//This will wait until the time passes a certain point, perform some actions
		//and then move this point forwards by some amount, giving the effect of 
		//gradual / slowed increase.
		if (currentState != State.Flying) {
			if (Time.time >= lastIncreaseTime) {
				lastIncreaseTime = Time.time + energyIncreaseRate;
				alterEnergy (1);
				GUIHandler.instance.updateEnergyBar (energy);
			}
		} else {
			//else assume we are swimming or walking, thus resting.
			if(Time.time >= lastIncreaseTime) {
				lastIncreaseTime = Time.time + energyFlyingDecreaseRate;
				alterEnergy(-1);
				GUIHandler.instance.updateEnergyBar(energy);
			}
		}
	}

}
