using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using SimpleNetwork;

namespace BSONInterface {

public class BSONSender {
	
	private SenderInterface messageSender;
	
	public BSONSender(SenderInterface sender) 
	{
		this.messageSender = sender;
	}

	public void Send(Kernys.Bson.BSONObject bsonObj)
	{
		byte[] raw = Kernys.Bson.SimpleBSON.Dump(bsonObj);
		messageSender.SendBytes(raw);
	}	
}

}