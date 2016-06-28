using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AIInterfaceExample {

/// <summary>
/// A simple game manager that handles the actual logic of the game.
/// It doesn't know or care about how the AbstractAIInterface is implemented
/// </summary>
public class GameManager : MonoBehaviour 
{
	public List<Player> players = new List<Player>();
	public AbstractAIInterface aiInterface;
	public float aiUpdatePeriod = 0.5f;
	public GameObject aiCharacterPrefab;
	
	List<AICharacterScript> aiCharacters = new List<AICharacterScript>();

	void Awake() 
	{
		for (int i = 0; i < players.Count; ++i) 
		{
			this.players[i].AssignPlayerId(i);
			
			// Creates an AI 'character' for each player that exists
			GameObject aiObject = Instantiate<GameObject>(this.aiCharacterPrefab);
			aiObject.transform.SetParent(this.transform);
			aiObject.name = "AI " + i;
			AICharacterScript aiScript = aiObject.GetComponent<AICharacterScript>();
		
			this.aiCharacters.Add(aiScript);
		}
		this.aiInterface.SetGameManager(this);
	}
	
	void Start() 
	{
		this.aiInterface.UpdatePlayerCount(this.players.Count);
		StartCoroutine(PeriodicAIUpdate());
	}
	
	public void AIMoveToCommand(int id, float x, float y)
	{
		this.aiCharacters[id].MoveTo(x, y);
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
				
				if (p.IsWaving) {
					this.aiInterface.UpdatePlayerAction(p.PlayerId, "waving");
				}
			}			
			yield return new WaitForSeconds(aiUpdatePeriod);
		}
	}
}
}
