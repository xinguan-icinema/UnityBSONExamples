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

	/*
	* Turns BSONObj into an array of bytes ready to transport.
	* -------------------------------------------------
	* | a |   b   |         c                         |
	* -------------------------------------------------
	* c => compressed data
	* b => length of uncompressed data
	* a => 1 byte header
	*
	*/	
	public void SendUncompressed(Kernys.Bson.BSONObject bsonObj)
	{
		byte[] raw = Kernys.Bson.SimpleBSON.Dump(bsonObj);
		
		List<byte> b = intToByteString(raw.Length);
		b.AddRange(raw);
		
		List<byte> a = new List<byte>();
		a.Add(2);
		a.AddRange(b);
		
		messageSender.SendBytes(a.ToArray());
	}
	
	static private List<byte> intToByteString(int paramInt)
	{
		byte[] array = System.BitConverter.GetBytes(paramInt);
		
		// Our BSON interface expects big endian
		if (System.BitConverter.IsLittleEndian)
			System.Array.Reverse(array);
		
		return new List<byte>(array);
	}	
}

}