using UnityEngine;
using System.Collections;

namespace SimpleNetwork {
	
/// <summary>
/// Simple abstraction of message sending 
/// Allows the BSONSender to be given any SenderInterface, so you can replace it later on
/// with something else (e.g if you want to use TCP instead)
/// </summary>
public interface SenderInterface 
{
	void SendBytes(byte[] data);
}
}
