using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScript : MonoBehaviour
{
	public bool Ending = false;

	void Awake()
	{
		Debug.Log("EndScene");
	}
	//When the mouse stops clicking
	void OnMouseUp()
	{
		//can only end once
		if (Ending == false)
		{
			//close the game
			Debug.Log("Ending game");
			Application.Quit();
			Ending = true;
		}
	}
}