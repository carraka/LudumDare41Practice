using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntSpawnPoint : MonoBehaviour {
	
	[System.Serializable]
	public class Wave
	{
		public GameObject enemyPrefab;
		public int maxEnemies;
		public float spawnInterval;
		public float enemySpeed;
	}

	public Wave[] waves;
	public int timeBetweenWaves = 5;
	public int currentWave = 0;
	float lastSpawnTime;
	public int enemiesSpawned = 0;

	public int timeBeforeFirstWave;
	public bool beginWaves = true;
	public bool spawningComplete = false;



	public float timeToNextSpawn = 1f;
	public float gruntSpeed = 3f;
	//public Transform[] targetList;

	private PlaceTower PlaceTower;
	private GameManager GameManager;

	public Vector2 spawnPointTilePos = new Vector2 (1, -1);

	public Vector2[] tileTargetList = new Vector2[] {
		new Vector2 (1, 7),
		new Vector2 (4, 7),
		new Vector2 (4, 3),
		new Vector2 (7, 3),
		new Vector2 (7, 4),
		new Vector2 (12, 4)
	};

	// Use this for initialization
	void Start () {
		//GetComponent<SpriteRenderer> ().enabled = false;

					
	}

	void Awake (){
		PlaceTower = GameObject.Find ("GameManager").GetComponent<PlaceTower> ();
		GameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		//StartCoroutine (spawnTime ());

	}

	void Update(){

		if (spawningComplete && GameObject.FindGameObjectWithTag("Grunt") == null)
		{
			GameManager.gameOver = true;
		}

		if (!GameManager.gameOver)
		{
			StartCoroutine (StartWaves ());



			if (beginWaves)
			{
				if (currentWave < waves.Length){

					float timeInterval = Time.time - lastSpawnTime;
					float spawnInterval = waves [currentWave].spawnInterval;

					if ((currentWave == 0 && enemiesSpawned == 0)||((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) || enemiesSpawned > 0 && timeInterval > spawnInterval) && enemiesSpawned < waves[currentWave].maxEnemies){
						//GameObject.Find ("Canvas").GetComponent<GameManager> ().currentWaveText.text = "Wave " + (currentWave + 1) + " of " + waves.Length;
						//Music ("attack");
						lastSpawnTime = Time.time;
						StartCoroutine (spawnTime (waves[currentWave].enemySpeed));
						enemiesSpawned++;
					}

					if (enemiesSpawned == waves[currentWave].maxEnemies && GameObject.FindGameObjectWithTag("Grunt") == null)
					{

						//SetMusic ("prepare");
						currentWave++;
						enemiesSpawned  = 0;
						lastSpawnTime = Time.time;
					}
				}
				else if (GameObject.FindGameObjectWithTag("Grunt") == null)
				{
					//SetMusic ("prepare");
					Debug.Log ("WAVES COMPLETE");
					spawningComplete = true;
				}
			}//if beginWaves

		}//if not gameOver
	}


	IEnumerator spawnTime(float gruntSpeed) {
		Debug.Log ("running spawnTime");
		yield return new WaitForSeconds(timeToNextSpawn);
		//StartCoroutine (spawnTime ());
		var grunt = (GameObject) Instantiate(Resources.Load("Prefabs/grunt"), PlaceTower.TiletoWorld(spawnPointTilePos), GetComponent<Transform>().rotation) ;
		grunt.GetComponent<Grunt> ().tileTargetList = tileTargetList;
		//grunt.GetComponent<Grunt> ().targetList = targetList;
		grunt.GetComponent<Grunt> ().speed = gruntSpeed;
		grunt.GetComponent<Grunt> ().hp = 2;

	}

	IEnumerator StartWaves(){
		yield return new WaitForSeconds (timeBeforeFirstWave);
		beginWaves = true;
	}


}
