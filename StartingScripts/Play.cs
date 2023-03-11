using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
	public bool Starting = true;

	void Start()
	{
		Debug.Log("LoadSceneB");
	}
	//when the user unclicks on the button
	void OnMouseUp()
	{
		//can only do once to avoid errors
		if (Starting)
		{
			//loads the second scene
			Debug.Log("sceneBuildIndex to load: " + 1);
			SceneManager.LoadScene(1);
			Starting = false;
		}
	}
}