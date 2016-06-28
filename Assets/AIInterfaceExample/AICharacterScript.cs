using UnityEngine;
using System.Collections;

namespace AIInterfaceExample {
	
/// <summary>
/// A simple AI script that moves the object towards whatever the last MoveTo command was
/// </summary>
public class AICharacterScript : MonoBehaviour 
{
	public float moveSpeed = 2f;
	
	Vector3 moveToTarget = new Vector3();
	
	public void MoveTo(float x, float y)
	{
		this.moveToTarget.Set(x, 0f, y);
	}
	
	void Update()
	{
		float step = this.moveSpeed * Time.deltaTime;
		transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, 
														this.moveToTarget, 
														step);
		
	}
}

}