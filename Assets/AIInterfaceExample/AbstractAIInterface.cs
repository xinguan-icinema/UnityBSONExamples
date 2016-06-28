using UnityEngine;
using System.Collections;

namespace AIInterfaceExample {

/// <summary>
/// This interface represents the sort of information an AI might need as input (number of players,
/// player positions, current player actions/gestures)
///
/// This abstraction means that e.g you could implement a local C# AI for testing using this 
/// interface without changing anything else
/// </summary>
public abstract class AbstractAIInterface : MonoBehaviour 
{
	abstract public void SetGameManager(GameManager gm);
	abstract public void UpdatePlayerCount(int count);
	
	abstract public void UpdatePlayerPosition(int id, Vector3 pos);
	
	// stuff like waving etc that could be represented as a simple string
	abstract public void UpdatePlayerAction(int id, string action);
}
}