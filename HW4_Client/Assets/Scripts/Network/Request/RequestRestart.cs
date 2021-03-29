using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestRestart : NetworkRequest
{
	public RequestRestart()
	{
		request_id = Constants.CMSG_RESTART;
	}

	public void send()
	{
		packet = new GamePacket(request_id);

	}
}
