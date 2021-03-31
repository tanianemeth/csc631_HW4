using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenu : MonoBehaviour
{
	private GameManager gameManager;
	private Button interactButton;
	private TMPro.TextMeshProUGUI turnIndicator;

	private TMPro.TextMeshProUGUI playerOneScore;

	private TMPro.TextMeshProUGUI playerTwoScore;
	// Start is called before the first frame update
	void Start()
    {
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		interactButton = GameObject.Find("Interact Button").GetComponent<Button>();
		turnIndicator = GameObject.Find("Turn Indicator").GetComponent<TMPro.TextMeshProUGUI>();
		playerOneScore = GameObject.Find("PlayerOneScore").GetComponent<TMPro.TextMeshProUGUI>();
		playerTwoScore = GameObject.Find("PlayerTwoScore").GetComponent<TMPro.TextMeshProUGUI>();
		playerOneScore.text = "Player One   " + gameManager.Players[0].Score.ToString();
		playerTwoScore.text = "Player Two   " + gameManager.Players[1].Score.ToString();
	}

	public void OnInteractClick()
	{
		gameManager.restart();
	}

	void Update()
	{
		interactButton.interactable = gameManager.CanInteract();
		turnIndicator.text = gameManager.GetCurrentPlayer().Name + "'s turn";
	}
}
