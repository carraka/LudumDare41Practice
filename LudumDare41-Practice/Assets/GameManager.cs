using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int wood = 0;
	public int stone = 0;
	public int carrots = 1;
	public int broccoli = 1; 

	public float timeBeforeGather = 3.0f;
	public float timeToGrow = 15f;

	private float growTimer = 0f;
	private float gatherTimer = 0f;

	public Text statsText;
	public Text broccoliText;
	public Text carrotText;
	public Text woodText;
	public Text stoneText;


	// Use this for initialization
	void Start () {


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
		//grow it!
	}

	void UpdateStatText(){
		statsText.text = "Time to Grow: " + (timeToGrow - (int)growTimer);
		broccoliText.text = broccoli.ToString();
		carrotText.text = carrots.ToString();
		woodText.text = wood.ToString();
		stoneText.text = stone.ToString();
	}
}
