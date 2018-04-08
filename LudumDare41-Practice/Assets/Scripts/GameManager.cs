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


    //Build costs
    public int costTowerWood = 10;
    public int costTowerStone = 5;

    public int costWallWood = 5;
    public int costWallStone = 10;

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
	private Text statsText;
	private Text broccoliText;
	private Text carrotText;
	private Text woodText;
	private Text stoneText;

	//ChoiceBox
	private Image choiceBox;
	private Text chooseGrowthText;
	private Text unpickedText;
	private Button broccoliButton;
	private Button carrotButton;
	private Button wallButton;
	private Button towerButton;
	private Image broccoliButtonSprite;
	private Image carrotButtonSprite;
	private Image wallButtonSprite;
	private Image towerButtonSprite;

	// Use this for initialization
	void Start () {
		timeWood = timeWood / broccoli;
		timeStone = timeStone / carrots;

		HideChoiceBox ();

	}

	void Awake(){
		//Text
		statsText = GameObject.Find ("Stattext").GetComponent<Text> ();
		broccoliText = GameObject.Find ("BroccoliText").GetComponent<Text> ();
		carrotText = GameObject.Find ("CarrotText").GetComponent<Text> ();
		woodText = GameObject.Find ("WoodText").GetComponent<Text> ();
		stoneText = GameObject.Find ("StoneText").GetComponent<Text> ();

		//ChoiceBox
		choiceBox = GameObject.Find ("ChoiceBox").GetComponent<Image> ();
		chooseGrowthText = GameObject.Find ("ChooseGrowthText").GetComponent<Text> ();
		unpickedText = GameObject.Find ("UnpickedText").GetComponent<Text> ();
		broccoliButton = GameObject.Find ("BroccoliButton").GetComponent<Button> ();
		carrotButton = GameObject.Find ("CarrotButton").GetComponent<Button> ();
		wallButton = GameObject.Find ("BuildWallButton").GetComponent<Button> ();
		towerButton = GameObject.Find ("BuildTowerButton").GetComponent<Button> ();
		broccoliButtonSprite = GameObject.Find ("BroccoliButton").GetComponent<Image> ();
		carrotButtonSprite = GameObject.Find ("CarrotButton").GetComponent<Image> ();
		wallButtonSprite = GameObject.Find ("BuildWallButton").GetComponent<Image> ();
		towerButtonSprite = GameObject.Find ("BuildTowerButton").GetComponent<Image> ();
	
	}

	
	// Update is called once per frame
	void Update () {

		UpdateStatText ();

		if (carrots <=0 && broccoli <= 0) {
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
		broccoliButtonSprite.enabled = true;
		carrotButtonSprite.enabled = true;
		carrotButton.enabled = true;
		towerButtonSprite.enabled = true;
		towerButton.enabled = true;
		wallButtonSprite.enabled = true;
		wallButton.enabled = true;

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
		towerButtonSprite.enabled = false;
		towerButton.enabled = false;
		wallButtonSprite.enabled = false;
		wallButton.enabled = false;

		unpickedText.enabled = false;

	}

	public void PickVegetable(string vegetable)
	{
		
		if (vegetable == "broc") {
			broccoli++;
			timeWood = baseTimeWood / broccoli;
            woodProduction = true;
		}

		if (vegetable == "carrot") {
			carrots++;
			timeStone = baseTimeStone / carrots;
            stoneProduction = true;
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
        /*		if (carrots == 0 && broccoli > 0)
                    broccoli--;
                else if (broccoli == 0 && carrots > 0)
                    carrots--;
                else 
                {
                    int num = Random.Range (0, 2);
                    if (num == 0)
                        broccoli--;
                    else
                        carrots--;
                }*/

        int num = Random.Range(0, broccoli + carrots);
        if (num < broccoli)
            broccoli--;
        else
            carrots--;
        
        if (broccoli <= 0)
            woodProduction = false;
        if (carrots <= 0)
            stoneProduction = false;

	}//EatVegetable()

}
