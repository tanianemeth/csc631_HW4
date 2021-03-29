using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestWin : NetworkRequest
{
	public RequestWin()
	{
		request_id = Constants.CMSG_WIN;
	}

	public void send()
	{
		packet = new GamePacket(request_id);

	}
}
