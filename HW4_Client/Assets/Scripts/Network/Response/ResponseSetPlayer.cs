using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseSetPlayerEventArgs : ExtendedEventArgs
{
	public int player_id { get; set; } // The player_id of who won

	public ResponseSetPlayerEventArgs()
	{
		event_id = Constants.SMSG_SETPLAYER;
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
		ResponseSetPlayerEventArgs args = new ResponseSetPlayerEventArgs
		{
			player_id = player_id
		};

		return args;
	}
}
