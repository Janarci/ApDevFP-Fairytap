using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseMenu : MonoBehaviour
{
	public Text playerName;
	public Text playerScore;
	public Text playerMsg;

    private void Update()
    {
		playerName.text = FindObjectOfType<WebHandlerScript>().currentPlayerName;
		playerScore.text = "Score Obtained: " + FindObjectOfType<WebHandlerScript>().currentPlayerScore.ToString();
		//if defet
		//sad msg

		//if win
		//unlock level msg
	}

    public void onRestartPress()
	{
		SceneManager.LoadSceneAsync("EzMap");
		FindObjectOfType<BGMhandler>().setCurrentSound("Idle");
	}
	public void onMainMenu()
	{
		SceneManager.LoadSceneAsync("MainMenu");
		FindObjectOfType<BGMhandler>().setCurrentSound("Idle");
	}
	public void onLeaderBoardPress()
	{
		playerName.text = FindObjectOfType<WebHandlerScript>().currentPlayerName;
		playerScore.text = "Score Obtained: " + FindObjectOfType<WebHandlerScript>().currentPlayerScore.ToString();
		FindObjectOfType<WebHandlerScript>().isOnAfterGameMenu = true;
		Application.Quit();
		Debug.Log("WorkinButon");
	}

}
