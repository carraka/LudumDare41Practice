using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildWallButton : MonoBehaviour {

	private GameManager GameManager;

	private Button thisButton;

	// Use this for initialization
	void Start () {
		Button btn = this.GetComponent<Button> ();
		btn.onClick.AddListener (TaskOnClick);
	}

	void Awake () {
		GameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();

	}
	// Update is called once per frame
	void Update () {

	}

	void TaskOnClick(){
        GameManager.GetComponent<PlaceTower>().buildCommand = PlaceTower.towerPlacementMode.wall;


    }
}
