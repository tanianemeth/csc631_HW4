using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestSetPlayer : NetworkRequest
{
	public RequestSetPlayer()
	{
		request_id = Constants.CMSG_SETPLAYER;
	}

	public void send(int player_id)
	{
		packet = new GamePacket(request_id);
    packet.addInt32(player_id);

	}
}
