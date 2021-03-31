﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Player[] Players = new Player[2];
	public GameObject HeroPrefab;

	private Hero[,] gameBoard = new Hero[3,3];

	private int currentPlayer = 1;
	private bool canInteract = false;
	private bool choosingInteraction = false;

	private bool useNetwork;
	private NetworkManager networkManager;

	void Start()
	{
		DontDestroyOnLoad(gameObject);
		networkManager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();
		MessageQueue msgQueue = networkManager.GetComponent<MessageQueue>();
		msgQueue.AddCallback(Constants.SMSG_MOVE, OnResponseMove);
		msgQueue.AddCallback(Constants.SMSG_INTERACT, OnResponseInteract);
		msgQueue.AddCallback(Constants.SMSG_WIN, OnResponseWin);
		msgQueue.AddCallback(Constants.SMSG_RESTART, OnResponseRestart);
	}

	public Player GetCurrentPlayer()
	{
		return Players[currentPlayer - 1];
	}

	public void Init(Player player1, Player player2)
	{
		Players[0] = player1;
		Players[1] = player2;
		currentPlayer = 1;
		useNetwork = (!player1.IsMouseControlled || !player2.IsMouseControlled);
	}

	public void CreateHeroes()
	{
		for (int i = 0; i < 5; i++)
		{
			GameObject heroObj1 = Instantiate(HeroPrefab, new Vector3(0, 0, (float)i), Quaternion.identity);
			heroObj1.GetComponentInChildren<Renderer>().material.color = Players[0].Color;
			Hero hero1 = heroObj1.GetComponent<Hero>();
			hero1.Index = i;
			Players[0].AddHero(hero1);
			gameBoard[0, i] = hero1;
			GameObject heroObj2 = Instantiate(HeroPrefab, new Vector3(5, 0, (float)i), Quaternion.identity);
			heroObj2.GetComponentInChildren<Renderer>().material.color = Players[1].Color;
			Hero hero2 = heroObj2.GetComponent<Hero>();
			hero2.Index = i;
			gameBoard[5, i] = hero2;
			Players[1].AddHero(hero2);
		}
	}

	public bool CanInteract()
	{
		//return canInteract;
		return true;
	}

	public void StartInteraction()
	{
		if (canInteract)
		{
			choosingInteraction = true;
		}
	}

	public void EndInteraction(Hero hero)
	{
		EndTurn();
	}

	public void EndInteractedWith(Hero hero)
	{
		// Do nothing
	}

	public void EndMove(Hero hero)
	{
		bool heroCanInteract = false;
		int[] deltaX = { 1, 0, -1, 0 };
		int[] deltaY = { 0, 1, 0, -1 };
		for (int i = 0; i < 4; ++i)
		{
			int x = hero.x + deltaX[i];
			int y = hero.y + deltaY[i];
			if (x >= 0 && x < 6 && y >= 0 && y < 5)
			{
				if (gameBoard[x, y] && gameBoard[x, y].Owner != hero.Owner)
				{
					heroCanInteract = true;
					break;
				}
			}
		}
		if (hero.Owner.IsMouseControlled)
		{
			canInteract = heroCanInteract;
		}

		if (!heroCanInteract)
		{
			EndTurn();
		}
	}

	public void EndTurn()
	{
		ObjectSelector.SetSelectedObject(null);
		canInteract = false;
		currentPlayer = 3 - currentPlayer;
	}
	public bool checkForDraw()
	{
		for(int x =0; x < 3; x++)
		{
			for(int y = 0; y < 3; y ++){
				if(gameBoard[x,y] == null){
					return false;

				}

			}
		}
		return true;

	}
	public bool checkForWin()
	{
		for(int i = 0; i < 3; i ++)
		{
			//rows
			if(gameBoard[i, 0] != null && gameBoard[i, 1] != null && gameBoard[i,2] != null){
				if(gameBoard[i, 0].Owner == gameBoard[i, 1].Owner && gameBoard[i,1].Owner == gameBoard[i, 2].Owner){
					return true;
				}
			}
			//columns
			if(gameBoard[0, i] != null && gameBoard[1, i] != null && gameBoard[2,i] != null){
				if(gameBoard[0, i].Owner == gameBoard[1, i].Owner && gameBoard[1,i].Owner == gameBoard[2, i].Owner){
					return true;
				}
			}
		}
		//diagonals
		if(gameBoard[0, 0] != null && gameBoard[1, 1] != null && gameBoard[2,2] != null){
			if(gameBoard[0, 0].Owner == gameBoard[1, 1].Owner && gameBoard[1,1].Owner == gameBoard[2, 2].Owner){
				return true;
			}
		}
		if(gameBoard[0, 2] != null && gameBoard[1, 1] != null && gameBoard[2,0] != null){
			if(gameBoard[0, 2].Owner == gameBoard[1, 1].Owner && gameBoard[1,1].Owner == gameBoard[2, 0].Owner){
				return true;
			}
		}
		return false;
	}

	public void restart()
	{

		if(checkForWin() || checkForDraw()){
			for(int x =0; x < 3; x++)
			{
				for(int y = 0; y < 3; y ++){
					if(gameBoard[x,y] != null){
						Destroy(gameBoard[x,y].gameObject);
						gameBoard[x,y] = null;
					}

				}
			}

		}


	}
	public void ProcessClick(GameObject hitObject)
	{
		if(checkForWin()){
			return;
		}
		if (hitObject.tag == "Tile")
		{
			if(! useNetwork || GetCurrentPlayer().UserID == Constants.USER_ID)
			{
				int x = (int)hitObject.transform.position.x;
				int y = (int)hitObject.transform.position.z;
				if(gameBoard[x, y] == null){
					GameObject heroObj = Instantiate(HeroPrefab, new Vector3(x, 0, y), Quaternion.identity);
					heroObj.GetComponentInChildren<Renderer>().material.color = GetCurrentPlayer().Color;
					Hero hero = heroObj.GetComponent<Hero>();
					//hero1.Index = i;
					GetCurrentPlayer().AddHero(hero);
					gameBoard[x, y] = hero;
					if (useNetwork)
					{
						networkManager.SendMoveRequest(0, x, y);
					}
					//checkForWin();
					if(checkForWin()){
						print("You won");
						canInteract = true;
					}
					else{
						if(checkForDraw())
						{
							print("Draw");
							canInteract = true;
						}
					}
					EndTurn();

				}

			}


			// if (ObjectSelector.SelectedObject)
			// {
			// 	Hero hero = ObjectSelector.SelectedObject.GetComponentInParent<Hero>();
			// 	if (hero)
			// 	{
			// 		int x = (int)hitObject.transform.position.x;
			// 		int y = (int)hitObject.transform.position.z;
			// 		if (gameBoard[x, y] == null)
			// 		{
			// 			if (hero.CanMoveTo(x, y))
			// 			{
			// 				if (useNetwork)
			// 				{
			// 					networkManager.SendMoveRequest(hero.Index, x, y);
			// 				}
			// 				gameBoard[hero.x, hero.y] = null;
			// 				hero.Move(x, y);
			// 				gameBoard[x, y] = hero;
			// 			}
			// 		}
			// 	}
			// }
		}
		else
		{
			Hero hero = hitObject.GetComponentInParent<Hero>();
			if (hero)
			{
				if (choosingInteraction)
				{
					Hero selectedHero = ObjectSelector.SelectedObject?.GetComponentInParent<Hero>();
					if (selectedHero)
					{
						if (AreNeighbors(hero, selectedHero) && hero.Owner != selectedHero.Owner)
						{
							if (useNetwork)
							{
								networkManager.SendInteractRequest(selectedHero.Index, hero.Index);
							}
							selectedHero.Interact(hero);
							choosingInteraction = false;
						}
					}
				}
				else if (hero.gameObject == ObjectSelector.SelectedObject)
				{
					ObjectSelector.SetSelectedObject(null);
				}
				else if (hero.Owner.IsMouseControlled && hero.Owner == Players[currentPlayer - 1])
				{
					ObjectSelector.SetSelectedObject(hitObject);
				}
			}
		}
	}

	public bool HighlightEnabled(GameObject gameObject)
	{
		if (gameObject.tag == "Tile")
		{
			Hero hero = ObjectSelector.SelectedObject?.GetComponentInParent<Hero>();
			if (hero)
			{
				int x = (int)gameObject.transform.position.x;
				int y = (int)gameObject.transform.position.z;
				return (gameBoard[x, y] == null);
			}
		}
		else if (choosingInteraction)
		{
			Hero hero = gameObject.GetComponentInParent<Hero>();
			Hero selectedHero = ObjectSelector.SelectedObject?.GetComponentInParent<Hero>();
			if (hero && selectedHero)
			{
				return AreNeighbors(hero, selectedHero) && hero.Owner != selectedHero.Owner;
			}
			else
			{
				return false;
			}
		}
		else
		{
			Hero hero = gameObject.GetComponentInParent<Hero>();
			if (hero)
			{
				return (hero.Owner.IsMouseControlled && hero.Owner == Players[currentPlayer - 1]);
			}
		}
		return true;
	}

	private bool AreNeighbors(Hero hero1, Hero hero2)
	{
		return (Math.Abs(hero1.x - hero2.x) + Math.Abs(hero1.y - hero2.y) == 1);
	}
	public void OnRestartClick()
	{
		if(useNetwork)
		{
			networkManager.SendRestartRequest();
		}
		restart();
	}

	public void OnResponseMove(ExtendedEventArgs eventArgs)
	{
		ResponseMoveEventArgs args = eventArgs as ResponseMoveEventArgs;
		if (args.user_id == Constants.OP_ID)
		{
			int x = args.x;
			int y = args.y;
			if(gameBoard[x, y] == null){
				GameObject heroObj = Instantiate(HeroPrefab, new Vector3(x, 0, y), Quaternion.identity);
				heroObj.GetComponentInChildren<Renderer>().material.color = GetCurrentPlayer().Color;
				Hero hero = heroObj.GetComponent<Hero>();
				GetCurrentPlayer().AddHero(hero);
				gameBoard[x, y] = hero;
				if(checkForWin()){
					print("Someone Won");
					canInteract = true;
				}
				else{
					if(checkForDraw())
					{
						print("Draw");
						canInteract = true;
					}
				}
				EndTurn();
}
			// int pieceIndex = args.piece_idx;
			// int x = args.x;
			// int y = args.y;
			// Hero hero = Players[args.user_id - 1].Heroes[pieceIndex];
			// gameBoard[hero.x, hero.y] = null;
			// hero.Move(x, y);
			// gameBoard[x, y] = hero;
		}
		else if (args.user_id == Constants.USER_ID)
		{
			// Ignore
		}
		else
		{
			Debug.Log("ERROR: Invalid user_id in ResponseReady: " + args.user_id);
		}
	}
	public void OnResponseWin(ExtendedEventArgs eventArgs)
	{
		ResponseWinEventArgs args = eventArgs as ResponseWinEventArgs;
	}
	public void OnResponseRestart(ExtendedEventArgs eventArgs)
	{
		ResponseRestartEventArgs args = eventArgs as ResponseRestartEventArgs;
		restart();
	}

	public void OnResponseInteract(ExtendedEventArgs eventArgs)
	{
		ResponseInteractEventArgs args = eventArgs as ResponseInteractEventArgs;
		if (args.user_id == Constants.OP_ID)
		{
			int pieceIndex = args.piece_idx;
			int targetIndex = args.target_idx;
			Hero hero = Players[args.user_id - 1].Heroes[pieceIndex];
			Hero target = Players[Constants.USER_ID - 1].Heroes[targetIndex];
			hero.Interact(target);
		}
		else if (args.user_id == Constants.USER_ID)
		{
			// Ignore
		}
		else
		{
			Debug.Log("ERROR: Invalid user_id in ResponseReady: " + args.user_id);
		}
	}
}
