using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseScoreEventArgs : ExtendedEventArgs
{
	public int user_id { get; set; } // The user_id of whom who sent the request
	public int user_score { get; set; } // score belongs to player with id user_id


	public ResponseScoreEventArgs()
	{
		event_id = Constants.SMSG_SCORE;
	}
}

public class ResponseScore : NetworkResponse
{
	private int user_id;
	private int user_score;


	public ResponseScore()
	{
	}

	public override void parse()
	{
		user_id = DataReader.ReadInt(dataStream);
		user_score = DataReader.ReadInt(dataStream);
	}

	public override ExtendedEventArgs process()
	{
		ResponseScoreEventArgs args = new ResponseScoreEventArgs
		{
			user_id = user_id,
		    user_score = user_score
		};

		return args;
	}
}
