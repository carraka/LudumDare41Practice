using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMarker : MonoBehaviour {

	private Renderer rend;
	// Use this for initialization
	void Start () {
		rend.enabled = false;
	}

	void Awake(){
		rend = this.GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
