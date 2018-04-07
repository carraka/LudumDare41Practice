using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	//Resources
	public int wood = 0;
	public int stone = 0;
	public int carrots = 1;
	public int broccoli = 1; 

	public int unpicked = 0;

	//Timers
	public float timeBeforeGather = 3.0f;
	public float timeToGrow = 5f;
	private float growTimer = 0f;
	private float gatherTimer = 0f;

	//Text
	public Text statsText;
	public Text broccoliText;
	public Text carrotText;
	public Text woodText;
	public Text stoneText;

	//ChoiceBox
	public Image choiceBox;
	public Text chooseGrowthText;
	public Text unpickedText;
	public Button broccoliButton;
	public Button carrotButton;
	public Image broccoliButtonSprite;
	public Image carrotButtonSprite;

	// Use this for initialization
	void Start () {

		HideChoiceBox ();

	}
	
	// Update is called once per frame
	void Update () {

		UpdateStatText ();

		if (carrots + broccoli == 0) {
			//End the Game
		}

		//Every X seconds, gather resources
		gatherTimer += Time.deltaTime;
		if (gatherTimer > timeBeforeGather) {
			gatherTimer = 0;
			GatherResources ();
		}

		//Every X seconds, grow a new vegetable
		growTimer += Time.deltaTime;
		if (growTimer > timeToGrow) {
			growTimer = 0;
			GrowVegetable ();
		}

	}

	void GatherResources(){
		wood += broccoli;
		stone += carrots;
	}

	void GrowVegetable(){
		ShowChoiceBox ();
		unpicked++;

	}

	void UpdateStatText(){
		statsText.text = "Time to Grow: " + (timeToGrow - (int)growTimer);
		broccoliText.text = broccoli.ToString();
		carrotText.text = carrots.ToString();
		woodText.text = wood.ToString();
		stoneText.text = stone.ToString();
		unpickedText.text = "Unpicked: " + unpicked.ToString ();
	}

	void ShowChoiceBox()
	{
		choiceBox.enabled = true;
		chooseGrowthText.enabled = true;
		broccoliButton.enabled = true;
		carrotButton.enabled = true;
		broccoliButtonSprite.enabled = true;
		carrotButtonSprite.enabled = true;
		unpickedText.enabled = true;

	}

	void HideChoiceBox()
	{
		choiceBox.enabled = false;
		chooseGrowthText.enabled = false;
		broccoliButton.enabled = false;
		carrotButton.enabled = false;
		broccoliButtonSprite.enabled = false;
		carrotButtonSprite.enabled = false;
		unpickedText.enabled = false;
	}

	public void PickVegetable(string vegetable)
	{
		
		if (vegetable == "broc")
			broccoli++;
		if (vegetable == "carrot")
			carrots++;

		unpicked--;
		if (unpicked <= 0)
		{
			HideChoiceBox();
		}

	}

}
