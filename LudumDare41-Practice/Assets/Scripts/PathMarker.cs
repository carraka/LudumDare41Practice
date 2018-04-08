using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathMarker : MonoBehaviour {

	private Image rend;
	// Use this for initialization
	void Start () {
		rend.enabled = false;
	}

	void Awake(){
		rend = this.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
