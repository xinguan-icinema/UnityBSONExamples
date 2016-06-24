using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Kernys.Bson;
using SimpleNetwork;

/// <summary>
/// Simple testing script that just sends whatever is in testSendString over UDP
/// Can send either raw UDP or wrapped in a BSON object { "testValue": "This is a test string" }
/// </summary>
public class BSONTestSender : MonoBehaviour 
{
	public string ip = "127.0.0.1";
	public int port = 9898;
	public int listenPort = 9899;
	public bool testSendRawUdp;
	public string testSendString = "";
	public bool sendNow;
	
	UDPSender udpSender;
	BSONInterface.BSONSender bsonSender;
	
	UDPListener udpListener;
	
	void OnEnable()
	{
		udpSender = new UDPSender(ip, port);
		bsonSender = new BSONInterface.BSONSender(udpSender);
		
		this.udpListener = new UDPListener(this.listenPort);
	}
	
	void OnDisable()
	{
		this.udpSender = null;
		this.bsonSender = null;
		this.udpListener.Stop();
		this.udpListener = null;
	}
	
	void Update()
	{
		// Sends the testSendString string whenever the sendNow checkbox is pressed
		if (this.sendNow) 
		{
			this.sendNow = false;
			
			if (this.testSendRawUdp) {
				// Send raw UDP straight from the udpSender
				this.udpSender.SendBytes(Encoding.UTF8.GetBytes(this.testSendString));
			} else {
				// Wrap in a simple BSON object and send via the bsonSender
				Kernys.Bson.BSONObject b = new Kernys.Bson.BSONObject();
				b.Add("testValue", new Kernys.Bson.BSONValue(this.testSendString));
				this.bsonSender.Send(b);
			}
		}
		
		byte[] received = this.udpListener.PopMessage();
		if (received != null) {
			Kernys.Bson.BSONObject inBson = Kernys.Bson.SimpleBSON.Load(received);
			Debug.LogFormat("Received from remote:\n {0}", BsonObjectToString(inBson));
		}
	}
	
	string BsonObjectToString(BSONObject b)
	{
		string s = "";
		foreach (string key in b.Keys)
		{
			BSONValue v = b[key];
			string valStr;
			if (v.valueType == BSONValue.ValueType.Object) {
				valStr = BsonObjectToString((BSONObject)v);
			} else {
				valStr = v.stringValue;
			}
			s += string.Format("{0}: {1}\n", key, valStr);
		}
		return s;
	}

}
