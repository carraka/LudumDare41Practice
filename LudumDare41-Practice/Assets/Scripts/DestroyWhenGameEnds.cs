using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenGameEnds : MonoBehaviour {

	private GameManager GameManager;
	// Use this for initialization
	void Start () {

		GameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.gameOver)
		{
			Destroy (this.gameObject);
		}
	}
}
