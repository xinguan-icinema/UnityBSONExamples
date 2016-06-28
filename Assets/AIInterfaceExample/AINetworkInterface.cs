using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Kernys.Bson;
using SimpleNetwork;

namespace AIInterfaceExample {

/// <summary>
/// AI interface implementation that assumes the logic of the AI is being handled
/// on a standalone server
/// </summary>
public class AINetworkInterface : AbstractAIInterface 
{
	public string AIAddress = "localhost";
	public int AIRemotePort = 9898;
	
	UDPSender udpSender;
	BSONInterface.BSONSender bsonSender;
	
	UDPListener udpListener;
	
	BSONArray updatesThisFrame = new BSONArray();
	
	GameManager gameManager;

	void OnEnable()
	{
		this.udpSender = new UDPSender(this.AIAddress, this.AIRemotePort);
		bsonSender = new BSONInterface.BSONSender(udpSender);
		
		this.udpListener = new UDPListener(9899);
	}
	
	void OnDisable()
	{
		this.udpListener.Stop();
	}
	
	public override void SetGameManager(GameManager gm) {
		this.gameManager = gm;
	}
	
	public override void UpdatePlayerCount (int count)
	{
		BSONObject obj = new BSONObject();
		obj.Add("type", new BSONValue("player count"));
		
		BSONObject data = new BSONObject();
		data.Add("count", new BSONValue(count));
			
		obj.Add("data", data);
		
		AddBsonUpdate(obj);
	}
		
	public override void UpdatePlayerPosition (int id, Vector3 pos)
	{
		BSONObject obj = new BSONObject();
		obj.Add("type", new BSONValue("player pos"));
		
		BSONObject data = new BSONObject();
		data.Add("id", new BSONValue(id));
		data.Add("x", new BSONValue(pos.x));
		data.Add("y", new BSONValue(pos.y));
		data.Add("z", new BSONValue(pos.z));
		
		obj.Add("data", data);
		
		AddBsonUpdate(obj);
	}
	
	public override void UpdatePlayerAction (int id, string action)
	{
		BSONObject obj = new BSONObject();
		obj.Add("type", new BSONValue("player action"));
		
		BSONObject data = new BSONObject();
		data.Add("id", new BSONValue(id));
		data.Add("action", new BSONValue(action));
		
		obj.Add("data", data);
		
		AddBsonUpdate(obj);
	}
	
	void AddBsonUpdate(BSONObject obj) 
	{
		this.updatesThisFrame.Add(obj);
	}
	
	void Update()
	{
		ReceiveUpdatesFromAI();
	}
	
	void LateUpdate()
	{
		// We want to wait until everything else has updated before 
		// sending the latest updates to the AI
		SendUpdatesToAI();
	}	

	void ReceiveUpdatesFromAI()
	{
		// Make sure we consume all the messages in the UDPListener queue,
		// otherwise it could start to fill up if we are receiving more than
		// 1 per frame. 
		bool hasMessages = true;
		while (hasMessages)
		{
			byte[] data = this.udpListener.PopMessage();
			
			if (data != null)
			{
				BSONObject obj = SimpleBSON.Load(data);
				try 
				{
					BSONValue commandType = obj["type"];
					
					switch (commandType.stringValue) 
					{
						// There is only 1 possible command in this example
						case "moveTo":
							int id = obj["id"].int32Value;
							float x = (float)obj["x"].doubleValue;
							float y = (float)obj["y"].doubleValue;
							
							// Send the update to the GameManager
							this.gameManager.AIMoveToCommand(id, x, y);
							break;
						
					}
				} catch (System.Exception e) {
					Debug.LogError(e);
				}
			} else {
				// No more messages this frame, exit the loop
				hasMessages = false;
			}
		}
	}
	
	void SendUpdatesToAI()
	{
		// Don't send an update if there is nothing to update
		if (this.updatesThisFrame.Count > 0)
		{
			BSONObject b = new BSONObject();
			b.Add("world", this.updatesThisFrame);
			this.bsonSender.Send(b);
			this.updatesThisFrame.Clear();
		}	
	}	
}
}
