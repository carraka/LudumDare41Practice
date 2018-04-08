using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildWallButton : MonoBehaviour {

	private GameManager GameManager;

	private Button thisButton;

	// Use this for initialization
	void Start () {
		thisButton = this.GetComponent<Button> ();
		thisButton.onClick.AddListener (TaskOnClick);
	}

	void Awake () {
		GameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();

	}
	// Update is called once per frame
	void Update ()
    {
        ColorBlock colorVar = thisButton.colors;
        if (GameManager.wood >= GameManager.costWallWood && GameManager.stone >= GameManager.costWallStone)
            colorVar.highlightedColor = new Color32(51, 255, 221, 255);
        else
            colorVar.highlightedColor = new Color32(255, 0, 0, 255);
        thisButton.colors = colorVar;
    }

	void TaskOnClick()
    {
        if (GameManager.wood >= GameManager.costWallWood && GameManager.stone >= GameManager.costWallStone) // if adequate supplies
        {
            GameManager.GetComponent<PlaceTower>().buildCommand = PlaceTower.towerPlacementMode.wall; //set placement mode to wall
            GameManager.GetComponent<GameManager>().PickVegetable("build"); //subtract 1 veggie from stock
        }
        else
            ; //play buzzer sound effect
    }
}
