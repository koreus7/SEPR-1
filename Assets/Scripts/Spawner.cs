using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Link to Team Eider's website: https://eldertheduck.wordpress.com/
/// Link to Team Shelduck's website: https://shelduck.wordpress.com/
/// Link to Assessment 3 project version: https://eldertheduck.wordpress.com/assessment-3
/// </summary>

public class Spawner : MonoBehaviour {

	public static Spawner inst;

	public static Spawner instance {
		get {
			if (inst == null) {
				inst = FindObjectOfType (typeof(Spawner)) as Spawner;
			}
			return inst;
		}
	}

	/// <summary>
	/// Show debug info in scene view
	/// </summary>
	public bool debugMode = true;

	/// <summary>
	/// radius about the player that the enemies can spawn in
	/// </summary>
	public int enemySpawnRadius = 50;

	/// <summary>
	/// Time between enemy limit increasing by one.
	/// </summary>
	public float enemySpawnIncreaseRate = 10;

	/// <summary>
	/// maximum amount of enemies possible to have spawned at once
	/// </summary>
	public float maxEnemies = 15;

	//used to count up from for the gradual difficulty increase
	float initialMaxEnemies;

	/// <summary>
	/// Possible spawn points of the enemies
	/// </summary>
	//public List<Transform> possibleEnemySpawns = new List<Transform> ();

	/// <summary>
	/// Possible enimies that can be spawned
	/// </summary>
	public List<GameObject> enemiesToSpawn = new List<GameObject> ();

	/// <summary>
	/// effect that is instantiated when an enemy is spawned
	/// </summary>
	public GameObject enemySpawnEffect;


    /// <summary>
    /// The collectables to be spawned.
    /// </summary>
    public List<GameObject> collectablePrefabs = new List<GameObject>();


    /// <summary>
    /// The collectable components of the collectables to be spawned
    /// </summary>
	private List<Collectable> collectables = new List<Collectable>();


    public float collSpawnRadius;

	float collScanHeight;
	int currentActiveCollectables;
	public int maxCollectables;
	public float collectableSpawnHeight;

	//current amount of enemies on the scene
	int currentActiveEnemies = 0;

	//player reference.
	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
        GetCollectableComponents();
		initialMaxEnemies = maxEnemies;
	}

	// Update is called once per frame
	void Update () {
		//ensures there is always maxEnemies and collectables in the scene.
		while (currentActiveEnemies < maxEnemies) {
			spawnEnemy();
		}
		while (currentActiveCollectables < maxCollectables) {
			spawnCollectable();
		}

		//gradually increase the difficulty of the game at a rate of one enemy per ten seconds. +30 by the end of the game
		maxEnemies = initialMaxEnemies + (Mathf.FloorToInt (Time.time / enemySpawnIncreaseRate));

	}

	/// <summary>
	/// Spawns a random enemy close to the player.
	/// </summary>
	public void spawnEnemy () {
		//uses localEnemySpawn() to get a coord to spawn the enemy at.
		Vector3 spawn = radiusAboutPlayer (enemySpawnRadius, 1, 50);
		int randomIndexEnemy = Random.Range (0, enemiesToSpawn.Count);
		Instantiate (enemiesToSpawn [randomIndexEnemy], spawn, Quaternion.identity);
		Instantiate (enemySpawnEffect, spawn, Quaternion.identity);
		currentActiveEnemies++;
	}

	/// <summary>
	/// Point near the ground in a radius about the player.
	/// </summary>
	/// <returns>Random point thats near the player.</returns>
	/// <param name="distance">Max distance from player.</param>
	/// <param name="height">Height above ground to place point.</param>
	/// <param name="scanHeight">Height to start scanning down from for raytracing.</param>
	public Vector3 radiusAboutPlayer (float distance, float height, float scanHeight) {
		//get a box above the player, height of scanHeight
		Vector3 tempSpawn = new Vector3(Random.Range (player.transform.position.x-distance,player.transform.position.x+distance),
		                                scanHeight,
		                                Random.Range (player.transform.position.z-distance,player.transform.position.z+distance)
		                                );
		RaycastHit hit;
		if (Physics.Raycast (tempSpawn, Vector3.down, out hit)) {
			//dont spawn enemies on water
			if(hit.transform.tag != "Water") {
				tempSpawn = hit.point;
				tempSpawn.y += height;
				return tempSpawn;
			} else {
				//recurse if we hit water. 
				return radiusAboutPlayer(distance,height,scanHeight);
			}
		}
		//safety case with randomness to stop stacking.
		return player.transform.position + new Vector3(Random.Range(0,10), Random.Range(0,2),0);
	}

	/// <summary>
	/// Spawns a random collectable.
	/// </summary>
	public void spawnCollectable () {
		Instantiate(pickRandomCollectable(), radiusAboutPlayer(collSpawnRadius,0.3f,50), Quaternion.identity);
		currentActiveCollectables++;
	}


    /// <summary>
    /// Chooses a random collectable from the list weighted by the
    /// abundance property of each collectable.
    /// </summary>
    public GameObject pickRandomCollectable() {

        float totalAbundance = 0.0f;

        foreach (Collectable c in collectables)
        {
            totalAbundance += c.abundace;
        }


        float random = Random.Range(0, totalAbundance);
        float count = 0;

        //Default return value.
        GameObject chosenCollectable = collectables[0].gameObject;


        foreach (Collectable c in collectables)
        {
            if (count + c.abundace >= random)
            {
                chosenCollectable = c.gameObject;
                break;
            }
            count += c.abundace;
        }


        return chosenCollectable;
    }

    public void GetCollectableComponents()
    {
        collectables.Clear();

        foreach (GameObject g in collectablePrefabs)
        {
            collectables.Add(g.GetComponent<Collectable>());
        }
    }

	/// <summary>
	/// Changes the spawn rates.
	/// </summary>
	/// <param name="newMaxEnemies">New max enemies.</param>
	/// <param name="newSpawnRadius">New spawn radius.</param>
	public void changeSpawnRates (int newMaxEnemies, int newSpawnRadius = 0) {
		Spawner.instance.maxEnemies = newMaxEnemies;
		if (newSpawnRadius != 0) {
			Spawner.instance.enemySpawnRadius = newSpawnRadius;
		}
	}

	/// <summary>
	/// Should be called whenever an enemy is killed.
	/// ensures that there is aways max enemies in the scene.
	/// </summary>
	public void enemyKilled () {
		spawnEnemy ();
		currentActiveEnemies -= 1;
	}
}