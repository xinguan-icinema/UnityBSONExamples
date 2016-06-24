using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AIInterfaceExample {

/// <summary>
/// A simple game manager that passes information between the AI and the world
/// </summary>
public class GameManager : MonoBehaviour 
{
	public List<Player> players = new List<Player>();
	public AbstractAIInterface aiInterface;
	public float aiUpdatePeriod = 0.5f;

	void Awake() 
	{
		for (int i = 0; i < players.Count; ++i) {
			players[i].AssignPlayerId(i);
		}
	}
	
	void Start() {
		StartCoroutine(PeriodicAIUpdate());
	}
	
	/// <summary>
	/// We probably don't need to send an update to the AI every single frame
	/// </summary>
	IEnumerator PeriodicAIUpdate()
	{
		while (true)
		{
			foreach (Player p in this.players) {
				this.aiInterface.UpdatePlayerPosition(p.PlayerId, p.transform.localPosition);
				
				if (p.isWaving) {
					this.aiInterface.UpdatePlayerAction(p.PlayerId, "waving");
				}
			}			
			yield return new WaitForSeconds(aiUpdatePeriod);
		}
	}
}
}
