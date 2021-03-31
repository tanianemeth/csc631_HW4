using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
	public int UserID { get; set; }
	public string Name { get; set; }

	public int Score { get; set; }
	public Color Color { get; set; }
	public Hero[] Heroes { get; set; }
	public bool IsMouseControlled { get; set; }
    
	private int heroCount = 0;

	public Player(int userID, string name, int score, Color color, bool isMouseControlled)
	{
		UserID = userID;
		Name = name;
		Score = score;
		Color = color;
		Heroes = new Hero[5];
		IsMouseControlled = isMouseControlled;
	}

	public void AddHero(Hero hero)
	{
		Heroes[heroCount++] = hero;
		hero.Owner = this;
	}

	public void AddScore(int score)
	{
		this.Score = score;
	}
}
