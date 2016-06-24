using UnityEngine;
using System.Collections;

namespace AIInterfaceExample {

public class Player : MonoBehaviour 
{
	Vector3 prevPos = new Vector3();
	int playerId;
	
	public bool isWaving = false;
	
	public int PlayerId {
		get { return playerId; }
	}
	
	public void AssignPlayerId(int id) {
		this.playerId = id;
	}
	
	void Update()
	{
		this.prevPos = this.transform.localPosition;
	}
}

}
