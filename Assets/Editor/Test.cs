using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class Test {

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
		// Test if the playerEnergy is always between 0 and 100
		PlayerStates state = new PlayerStates ();
		int playerEnergy = state.energy;
		Assert.GreaterOrEqual(playerEnergy, 0);
		Assert.LessOrEqual(playerEnergy, 100);
	}
}
