using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenGameEnds : MonoBehaviour {

	private EndGame EndScreen;
	// Use this for initialization
	void Start () {

		EndScreen = GameObject.Find ("EndScreen").GetComponent<EndGame> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (EndScreen.gameIsOver)
		{
			Destroy (this.gameObject);
		}
	}
}
