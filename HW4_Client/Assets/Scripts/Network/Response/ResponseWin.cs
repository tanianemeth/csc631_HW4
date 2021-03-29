using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseWinEventArgs : ExtendedEventArgs
{
	public int player_id { get; set; } // The player_id of who won

	public ResponseWinEventArgs()
	{
		event_id = Constants.SMSG_WIN;
	}
}

public class ResponseSetPlayer : NetworkResponse
{
	private int player_id;

	public ResponseSetPlayer()
	{
	}

	public override void parse()
	{
		player_id = DataReader.ReadInt(dataStream);
	}

	public override ExtendedEventArgs process()
	{
		ResponseWinEventArgs args = new ResponseWinEventArgs
		{
			player_id = player_id
		};

		return args;
	}
}
