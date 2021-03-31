using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenu : MonoBehaviour
{
	private GameManager gameManager;
	private Button interactButton;
	private TMPro.TextMeshProUGUI turnIndicator;
	private TMPro.TextMeshProUGUI gameOver;

	// Start is called before the first frame update
	void Start()
    {
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		interactButton = GameObject.Find("Interact Button").GetComponent<Button>();
		turnIndicator = GameObject.Find("Turn Indicator").GetComponent<TMPro.TextMeshProUGUI>();
		gameOver = GameObject.Find("Win Message").GetComponent<TMPro.TextMeshProUGUI>();
	}

	public void OnInteractClick()
	{
		gameManager.OnRestartClick();

	}

	void Update()
	{
		interactButton.interactable = gameManager.CanInteract();
		if(gameManager.checkForWin()){
			turnIndicator.text = "";
			gameOver.text = gameManager.GetCurrentPlayer().Name + " " + "lost the game";

		}
		else{
			turnIndicator.text = gameManager.GetCurrentPlayer().Name + "'s turn";
			gameOver.text = "";
		}
		// if(gameManager.checkForWin())
		// {
		// 	gameOver.text = gameManager.GetCurrentPlayer().Name + " " + "lost the game";
		// }
		if(gameManager.checkForDraw()){
			turnIndicator.text = "";
			gameOver.text = "It's a Tie";
		}
	}
}
