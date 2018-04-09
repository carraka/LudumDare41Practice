using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public bool gameOver = false;
	public GameObject EndScreen;


	public int wood = 0;
	public int stone = 0;
	public int carrots = 10;
	public int broccoli = 10; 

    public int unpicked = 0;


    //Build costs
    public int costTowerWood = 1;
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
    private Image eggplantIcon;

    //InfoBox
    private Image infoBox;
    private Text buildCostText;
    private Image buildWoodIcon;
    private Text buildWoodText;
    private Image buildStoneIcon;
    private Text buildStoneText;
    private Image buildEggplantIcon;
    private Text buildEggplantText;
    private Text infoText;

    // Use this for initialization
    void Start() {
        timeWood = timeWood / broccoli;
        timeStone = timeStone / carrots;

        HideChoiceBox();
        HideInfoBox();

    }

    void Awake() {
        //Text
        statsText = GameObject.Find("Stattext").GetComponent<Text>();
        broccoliText = GameObject.Find("BroccoliText").GetComponent<Text>();
        carrotText = GameObject.Find("CarrotText").GetComponent<Text>();
        woodText = GameObject.Find("WoodText").GetComponent<Text>();
        stoneText = GameObject.Find("StoneText").GetComponent<Text>();

        //ChoiceBox
        choiceBox = GameObject.Find("ChoiceBox").GetComponent<Image>();
        chooseGrowthText = GameObject.Find("ChooseGrowthText").GetComponent<Text>();
        unpickedText = GameObject.Find("UnpickedText").GetComponent<Text>();
        broccoliButton = GameObject.Find("BroccoliButton").GetComponent<Button>();
        carrotButton = GameObject.Find("CarrotButton").GetComponent<Button>();
        wallButton = GameObject.Find("BuildWallButton").GetComponent<Button>();
        towerButton = GameObject.Find("BuildTowerButton").GetComponent<Button>();
        broccoliButtonSprite = GameObject.Find("BroccoliButton").GetComponent<Image>();
        carrotButtonSprite = GameObject.Find("CarrotButton").GetComponent<Image>();
        wallButtonSprite = GameObject.Find("BuildWallButton").GetComponent<Image>();
        towerButtonSprite = GameObject.Find("BuildTowerButton").GetComponent<Image>();
        eggplantIcon = GameObject.Find("EggplantIcon").GetComponent<Image>();

        //Infobox
        infoBox = GameObject.Find("InfoBox").GetComponent<Image>();
        buildCostText = GameObject.Find("BuildCostText").GetComponent<Text>();
        buildWoodIcon = GameObject.Find("BuildWoodIcon").GetComponent<Image>();
        buildWoodText = GameObject.Find("BuildWoodText").GetComponent<Text>();
        buildStoneIcon = GameObject.Find("BuildStoneIcon").GetComponent<Image>();
        buildStoneText = GameObject.Find("BuildStoneText").GetComponent<Text>();
        buildEggplantIcon = GameObject.Find("BuildEggplantIcon").GetComponent<Image>();
        buildEggplantText = GameObject.Find("BuildEggplantText").GetComponent<Text>();
        infoText = GameObject.Find("InfoText").GetComponent<Text>();

		//End Game
		//EndScreen = GameObject.Find ("EndScreen").GetComponent<EndGame> ();

    }


    // Update is called once per frame
    void Update() {

        UpdateStatText();

        if (carrots <= 0 && broccoli <= 0) {
			gameOver = true;
			EndScreen.SetActive(true);
			EndScreen.GetComponent<EndGame>().PlayEnding (true);

        }

		else if (gameOver)
		{
			EndScreen.SetActive(true);

			EndScreen.GetComponent<EndGame>().PlayEnding (false);
		}

        GatherResources();

        //Every X seconds, grow a new vegetable
        growTimer += Time.deltaTime;
        if (growTimer > timeToGrow) {
            growTimer = 0;
            GrowVegetable();
        }

    }

    void GatherResources() {
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

    void GrowVegetable() {
        ShowChoiceBox();
        unpicked++;

    }

    void UpdateStatText() {
        statsText.text = "Time to Grow: " + (timeToGrow - (int)growTimer);
        broccoliText.text = broccoli.ToString();
        carrotText.text = carrots.ToString();
        woodText.text = wood.ToString();
        stoneText.text = stone.ToString();
        unpickedText.text = "Unpicked: " + unpicked.ToString();
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
        eggplantIcon.enabled = true;

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
        eggplantIcon.enabled = false;

        unpickedText.enabled = false;

    }

    public void UpdateInfoBox(string updateType)
    {
        infoBox.enabled = true;
        buildCostText.enabled = false;
        buildWoodIcon.enabled = false;
        buildWoodText.enabled = false;
        buildStoneIcon.enabled = false;
        buildStoneText.enabled = false;
        buildEggplantIcon.enabled = false;
        buildEggplantText.enabled = false;
        infoText.enabled = false;

        if (updateType == "Build")
        {
            infoText.enabled = true;
            infoText.GetComponent<Text>().text = "Select placement to build";
        }

        if (updateType == "Not Enough")
        {
            infoText.enabled = true;
            infoText.GetComponent<Text>().text = "Not enough material to build this structure";
        }

        if (updateType == "Broccoli")
        {
            infoText.enabled = true;
            infoText.GetComponent<Text>().text = "Harvest more wood!";
        }

        if (updateType == "Carrot")
        {
            infoText.enabled = true;
            infoText.GetComponent<Text>().text = "Mine more stone!";
        }

        if (updateType == "Cost Wall" || updateType == "Cost Tower")
        {
            buildCostText.enabled = true;
            buildWoodIcon.enabled = true;
            buildWoodText.enabled = true;
            buildStoneIcon.enabled = true;
            buildStoneText.enabled = true;
            buildEggplantIcon.enabled = true;
            buildEggplantText.enabled = true;

            if (updateType == "Cost Wall")
            {
                buildWoodText.GetComponent<Text>().text = costWallWood.ToString();
                buildStoneText.GetComponent<Text>().text = costWallStone.ToString();
            }

            if (updateType == "Cost Tower")
            {
                buildWoodText.GetComponent<Text>().text = costTowerWood.ToString();
                buildStoneText.GetComponent<Text>().text = costTowerStone.ToString();
            }
        }
    }



    public void HideInfoBox()
    {
        if (GameObject.Find("GameManager").GetComponent<PlaceTower>().buildCommand != PlaceTower.towerPlacementMode.off)
            return;  // do not hide dialogue box after selecting to build

        infoBox.enabled = false;
        buildCostText.enabled = false;
        buildWoodIcon.enabled = false;
        buildWoodText.enabled = false;
        buildStoneIcon.enabled = false;
        buildStoneText.enabled = false;
        buildEggplantIcon.enabled = false;
        buildEggplantText.enabled = false;
        infoText.enabled = false;
        
    }


	public void PickVegetable(string vegetable)
	{
		AudioSource audio = gameObject.AddComponent < AudioSource > ();

		if (vegetable == "broc") {
			broccoli++;
			timeWood = baseTimeWood / broccoli;
            woodProduction = true;
			audio.PlayOneShot ((AudioClip)Resources.Load ("veg_pull"));

		}

		else if (vegetable == "carrot") {
			carrots++;
			timeStone = baseTimeStone / carrots;
            stoneProduction = true;
			audio.PlayOneShot ((AudioClip)Resources.Load ("veg_pull"));

		}
		else if (vegetable == "build")
		{
			audio.PlayOneShot ((AudioClip)Resources.Load ("Audio/SoundFX/ldp2_building_ready_alt"));
		}

		unpicked--;

		//play sfx

		//hide choice box and buttons when all vegetables are picked
		if (unpicked <= 0)
		{
			HideChoiceBox();
		}

	}//PickVegetable

	public void EatVegetable()
	{

		AudioSource audio = gameObject.AddComponent < AudioSource > ();
		//audio.PlayOneShot ((AudioClip)Resources.Load ("Audio/SoundFX/ldp2_screams", 1f));

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
