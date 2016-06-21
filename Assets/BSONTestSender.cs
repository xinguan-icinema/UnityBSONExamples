using UnityEngine;
using System.Collections;
using System.Text;

using SimpleNetwork;

/// <summary>
/// Simple testing script that just sends whatever is in testSendString over UDP
/// Can send either raw UDP or wrapped in a BSON object { "testValue": "This is a test string" }
/// </summary>
public class BSONTestSender : MonoBehaviour 
{
	public string ip = "127.0.0.1";
	public int port = 9898;
	public bool testSendRawUdp;
	public string testSendString = "";
	public bool sendNow;
	
	UDPSender udpSender;
	BSONInterface.BSONSender bsonSender;
	
	void Awake()
	{
		udpSender = new UDPSender(ip, port);
		bsonSender = new BSONInterface.BSONSender(udpSender);
	}
	
	void Update()
	{
		if (this.sendNow) 
		{
			this.sendNow = false;
			if (this.testSendRawUdp) {
				this.udpSender.SendBytes(Encoding.UTF8.GetBytes(this.testSendString));
			} else {
				Kernys.Bson.BSONObject b = new Kernys.Bson.BSONObject();
				b.Add("testValue", new Kernys.Bson.BSONValue(this.testSendString));
				this.bsonSender.SendUncompressed(b);
			}
		}
	}
}
