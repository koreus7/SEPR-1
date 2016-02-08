using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using NUnit.Framework;

public class Test {

	public PlayerStates state = new PlayerStates ();

    [Test]
    public void EditorTest()
    {
        //Arrange
        var gameObject = new GameObject();

        //Act
        //Try to rename the GameObject
        var newGameObjectName = "My game object";
        gameObject.name = newGameObjectName;

        //Assert
        //The object has a new name
        Assert.AreEqual(newGameObjectName, gameObject.name);
    }

	[Test]
	public void PlayerEnergy()
	{
		// Tests if the playerEnergy is always between 0 and 100
		PlayerStates state = new PlayerStates ();
		int playerEnergy = state.energy;
		Assert.GreaterOrEqual(playerEnergy, 0);
		Assert.LessOrEqual(playerEnergy, 100);
	}

	[Test]
	public void PlayerStartingHealth()
	{
		// Tests if the Player starts with full health, i.e. 100 points
		int initialHealth = state.health;
		Assert.AreEqual (initialHealth, 100);
	}

	[Test]
	public void PersistentScore()
	{
		// Tests if the game saves The player's score between rounds
		
		SceneManager.LoadScene ("East");
	}
}
