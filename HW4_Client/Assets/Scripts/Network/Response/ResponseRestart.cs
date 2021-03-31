using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseRestartEventArgs : ExtendedEventArgs
{
	public int player_id { get; set; } // The player_id of who pressed restart?

	public ResponseRestartEventArgs()
	{
		event_id = Constants.SMSG_RESTART;
	}
}

public class ResponseRestart : NetworkResponse
{
	private int player_id;

	public ResponseRestart()
	{
		
	}

	public override void parse()
	{
		player_id = DataReader.ReadInt(dataStream);
	}

	public override ExtendedEventArgs process()
	{
		ResponseRestartEventArgs args = new ResponseRestartEventArgs
		{
			player_id = player_id
		};

		return args;
	}
}
