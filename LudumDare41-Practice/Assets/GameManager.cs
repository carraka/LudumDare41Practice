using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	//Resources
	public int wood = 0;
	public int stone = 0;
	public int carrots = 10;
	public int broccoli = 10; 

	public int unpicked = 0;

	//Gameplay booleans
	public bool woodProduction = true;
	public bool stoneProduction = true;

	//Timers
	public float baseTimeWood = 30f;
	public float baseTimeStone = 30f;
	public float timeWood = 30f;
	public float timeStone = 30f;
	public float timeToGrow = 5f;
	private float growTimer = 0f;
	private float woodTimer = 0f;
	private float stoneTimer = 0f;

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
		timeWood = timeWood / broccoli;
		timeStone = timeStone / carrots;

		HideChoiceBox ();

	}
	
	// Update is called once per frame
	void Update () {

		UpdateStatText ();

		if (carrots + broccoli == 0) {
			//End the Game
		}

		GatherResources ();

		//Every X seconds, grow a new vegetable
		growTimer += Time.deltaTime;
		if (growTimer > timeToGrow) {
			growTimer = 0;
			GrowVegetable ();
		}

	}

	void GatherResources(){
		woodTimer += Time.deltaTime;
		stoneTimer += Time.deltaTime;

		if (woodTimer > timeWood && woodProduction) {
			woodTimer = 0;
			wood++;
		}

		if (stoneTimer > timeStone && stoneProduction) {
			stoneTimer = 0;
			stone++;
		}
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
		
		if (vegetable == "broc") {
			broccoli++;
			timeWood = baseTimeWood / broccoli;
		}

		if (vegetable == "carrot") {
			carrots++;
			timeStone = baseTimeStone / carrots;
		}	

		unpicked--;

		//play sfx
		AudioSource audio = gameObject.AddComponent < AudioSource > ();
		audio.PlayOneShot ((AudioClip)Resources.Load ("veg_pull"));

		//hide choice box and buttons when all vegetables are picked
		if (unpicked <= 0)
		{
			HideChoiceBox();
		}

	}//PickVegetable

	public void EatVegetable()
	{
		if (carrots == 0 && broccoli > 0)
			broccoli--;
		else if (broccoli == 0 && carrots > 0)
			carrots--;
		else 
		{
			int num = Random.Range (0, 1);
			if (num == 0)
				broccoli--;
			else
				carrots--;
		}
	}//EatVegetable()

}
