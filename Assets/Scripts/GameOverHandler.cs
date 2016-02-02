using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// GameOverHandler.
/// 
/// Manages the game over planel.
/// </summary>
public class GameOverHandler : MonoBehaviour
{

    public Text scoreText;
    public InputField nameInput;

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

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