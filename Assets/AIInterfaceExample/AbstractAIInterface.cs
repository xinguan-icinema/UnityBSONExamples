using UnityEngine;
using System.Collections;

namespace AIInterfaceExample {

/// <summary>
/// Functions that an AI needs on the Unity side
/// e.g you could implement a local C# AI for testing using this interface
/// without changing anything else
/// </summary>
public abstract class AbstractAIInterface : MonoBehaviour 
{
	abstract public void UpdatePlayerPosition(int id, Vector3 pos);
	
	// stuff like waving etc that could be represented as a simple string
	abstract public void UpdatePlayerAction(int id, string action);
}
}