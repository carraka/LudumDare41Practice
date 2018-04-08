using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntSpawnPoint : MonoBehaviour {
	public float timeToNextSpawn = 1f;
	public float gruntSpeed = 3f;
	//public Transform[] targetList;

	private PlaceTower PlaceTower;

	public Vector2 spawnPointTilePos = new Vector2 (2, 2);

	public Vector2[] tileTargetList = new Vector2[] {
		new Vector2 (2, 8),
		new Vector2 (5, 8),
		new Vector2 (5, 4),
		new Vector2 (8, 4),
		new Vector2 (8, 5),
		new Vector2 (13, 5)
	};

	// Use this for initialization
	void Start () {
		//GetComponent<SpriteRenderer> ().enabled = false;

					
	}

	void Awake (){
		StartCoroutine (spawnTime ());
		PlaceTower = GameObject.Find ("GameManager").GetComponent<PlaceTower> ();
	}

	IEnumerator spawnTime() {
		yield return new WaitForSeconds(timeToNextSpawn);
		StartCoroutine (spawnTime ());
		var grunt = (GameObject) Instantiate(Resources.Load("Prefabs/grunt"), PlaceTower.TiletoWorld(spawnPointTilePos), GetComponent<Transform>().rotation) ;
		grunt.GetComponent<Grunt> ().tileTargetList = tileTargetList;
		//grunt.GetComponent<Grunt> ().targetList = targetList;
		grunt.GetComponent<Grunt> ().speed = gruntSpeed;

	}

}
