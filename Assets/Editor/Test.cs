using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class Test 
{

	/// <summary>
	/// Script containing all unit tests
	/// </summary>

	PlayerStates playerStates = new PlayerStates ();

	[Test]
	public void PlayerStartsWith100Health()
	{
		PlayerStates playerStates = new PlayerStates ();
		Assert.AreEqual (100, playerStates.health);
	}

	[Test]
	public void PlayerEnergy()
	{
		int playerEnergy = playerStates.energy;
		Assert.GreaterOrEqual(playerEnergy, 0);
		Assert.LessOrEqual(playerEnergy, 100);
	}

	[Test]
	public void AlterHealth()
	{
		int originalHealth = playerStates.health;
		playerStates.alterHealth (-10);
		int newHealth = playerStates.health;
		Assert.Less(newHealth, originalHealth);
		playerStates.alterHealth (10);
		Assert.AreEqual (playerStates.health, originalHealth);
	}

	[Test]
	public void AlterResources()
	{
		PlayerStates states = new PlayerStates ();
		int originalResources = states.resources;
		states.alterResources (-10);
		int newResources = states.resources;
		Assert.Less (newResources, originalResources);
		states.alterResources (10);
		Assert.AreEqual (states.resources, originalResources);

	}



}