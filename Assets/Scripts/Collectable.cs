using UnityEngine;
using System.Collections;

/// <summary>
/// Link to website: https://shelduck.wordpress.com/
/// Link to executables: https://shelduck.wordpress.com/downloads/
/// </summary>

public class Collectable : MonoBehaviour {

	/// <summary>
	/// points the player gains for collecting
	/// </summary>
	public int pointsOnCollect;

	/// <summary>
	/// Resources the player gains for collecting.
	/// </summary>
	public int resourceOnCollect;

	/// <summary>
	/// Energy gained on collection
	/// </summary>
	public int energyOnCollect;


    /// <summary>
    /// Health gained on collection.
    /// </summary>
    public int healthOnCollect;

	/// <summary>
	/// Gameobject that is instantiated when the player collides
	/// </summary>
	public GameObject pointsEffect;


    /// <summary>
    /// The powerup to give the player when collected.
    /// </summary>
    public PlayerStates.PowerUpState powerup;

	/// <summary>
	/// Does the object bob?
	/// </summary>
	public bool bob;

    /// <summary>
    /// How likley it is to spawn.
    /// </summary>
    public float abundace = 0.5f;

	/// <summary>
	/// Amount that thet object bobs by (top to bottom): units
	/// </summary>
	public float bobAmount;

	/// <summary>
	/// Speed at which the object bobs: units/second
	/// </summary>
	public float bobSpeed;

	Transform pointsText;

	Vector3 startPosition;

	GameObject player;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
		//if we are bobbing, it rotates and bobs the object by the desired amount.
		if (bob) {
			Vector3 wantedPosition = startPosition;
			wantedPosition.y += Mathf.Sin (Time.time * bobSpeed) * bobAmount;
			transform.position = wantedPosition;
			transform.Rotate(new Vector3(0,20*Time.deltaTime,0));
		}
	}

	void LateUpdate () {
		//when out of range, spawn a new collectable
		if((this.transform.position - player.transform.position).magnitude > 50) {
			Spawner.instance.spawnCollectable();
			Destroy (this.gameObject);
		}
	}

	/// <summary>
	/// When we collide with a player, update the relevent player stats and destroy itself.
	/// </summary>
	/// <param name="c">The thing we collided with</param>
	void OnTriggerEnter (Collider c) {
		if(c.transform.tag == "Player") {
			PlayerStates.instance.alterHealth(healthOnCollect);
			PlayerStates.instance.alterPoints(pointsOnCollect);
			PlayerStates.instance.collectPowerup(powerup);
			GUIHandler.instance.updatePointsText(PlayerStates.instance.points.ToString(), "+"+pointsOnCollect.ToString());
			if(resourceOnCollect != 0) {
				PlayerStates.instance.alterResources(resourceOnCollect);
				GUIHandler.instance.updateResourceText(PlayerStates.instance.resources.ToString(), "+"+resourceOnCollect.ToString());
			}
			PlayerStates.instance.alterEnergy(energyOnCollect);
			GUIHandler.instance.updateEnergyBar(PlayerStates.instance.energy);
			Spawner.instance.spawnCollectable();
			Destroy(this.gameObject);
		}
	}
}
