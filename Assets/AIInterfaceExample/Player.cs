using UnityEngine;
using System.Collections;

namespace AIInterfaceExample {

/// <summary>
/// A simple Player script that keeps track of it's own ID, and with an example of how
/// it could interface with a GameManager about whether the player is waving or not
/// </summary>
public class Player : MonoBehaviour 
{
	int playerId;
	
	public bool isWaving = false; 
	
	// You would replace with with actual code from your gesture recognition algorithms
	public bool IsWaving {
		get { return isWaving; }
	}
	
	public int PlayerId {
		get { return playerId; }
	}
	
	public void AssignPlayerId(int id) {
		this.playerId = id;
	}

}

}
