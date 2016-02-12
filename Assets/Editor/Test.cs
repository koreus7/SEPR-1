using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class Test 
{

	PlayerStates playerStates { 
		get { 
			return TestingStatics.instance.GetPlayerStates(); 
		} 
	}

	PlayerController playerController { 
		get {
			return TestingStatics.instance.GetPlayerController ();
		}
	}
		

	[Test]
	public void PlayerStartsWith100Health()
	{
		TestingStatics.instance.WaitForInit ();
		Assert.AreEqual (100, playerStates.health);
	}

	[Test]
	public void PlayerEnergy()
	{
		TestingStatics.instance.WaitForInit ();
		// Test if the playerEnergy is always between 0 and 100
		int playerEnergy = playerStates.energy;
		Assert.GreaterOrEqual(playerEnergy, 0);
		Assert.LessOrEqual(playerEnergy, 100);
	}

	[Test]
	public void DecreaseHealth()
	{
		TestingStatics.instance.WaitForInit ();
		// Test if the player's health decreases.
		int originalHealth = playerStates.health;
		playerStates.alterHealth (-10);
		int newHealth = playerStates.health;
		Assert.Less(newHealth, originalHealth);

		playerStates.health = 100;
	}


}