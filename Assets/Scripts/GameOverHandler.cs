using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Link to Team Eider's website: https://eldertheduck.wordpress.com/
/// Link to Team Shelduck's website: https://shelduck.wordpress.com/
/// Link to Assessment 3 project version: https://eldertheduck.wordpress.com/assessment-3
/// </summary>

/// <summary>
/// Manages the game over planel.
/// </summary>

public class GameOverHandler : MonoBehaviour
{

    public Text scoreText;
    public InputField nameInput;

    void OnEnable()
    {
		PlayerStates.inst.alterPoints (PlayerStates.inst.resources);
		PlayerStates.inst.alterResources (-PlayerStates.inst.resources);
		GUIHandler.instance.updateResourceText(0.ToString(), "-"+PlayerStates.inst.resources.ToString());
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        scoreText.text = PlayerStates.instance.points.ToString();
        GUI.FocusControl(nameInput.name);
    }

    public void saveAndExitToMenu()
    {
        HighScoresBackend.instance.addHighScore(nameInput.text, PlayerStates.instance.points);
        HighScoresBackend.instance.saveHighScores();
        Application.LoadLevel("mainmenu");
    }
}