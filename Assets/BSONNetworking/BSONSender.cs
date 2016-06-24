using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using SimpleNetwork;

namespace BSONInterface {

/// <summary>
/// Doesn't really do much right now so maybe not necessary, but at some point
/// it might need to do things like split up the bsonObj if it's too big etc 
/// </summary>
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