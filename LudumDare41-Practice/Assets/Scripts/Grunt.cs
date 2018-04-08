using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : MonoBehaviour {
	public bool isEating = false;
	public float timeToEat = 3f;
	public float eatTimer = 3f;

    private bool isAttacking = false;
    private Wall attackTarget = null;

	private GameManager GameManager;
	private PlaceTower PlaceTower;
	private Canvas canvas;

	//public Transform[] targetList;// =new Transform[];
	private Transform target;

	public Vector2[] tileTargetList;
	public Vector2 tileTarget;

	public float speed = 0.1f;
	public int nextTargetIndex = 0;
	public bool inRange = false;
	bool done = false;

	// Use this for initialization
	void Start () {
		//target = targetList[nextTargetIndex];
		tileTarget = tileTargetList[nextTargetIndex];
		this.transform.SetParent (canvas.transform);
	}

	void Awake(){
		GameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		PlaceTower = GameObject.Find ("GameManager").GetComponent<PlaceTower> ();
		canvas = GameObject.Find ("Canvas").GetComponent<Canvas> ();

    }

	
	// Update is called once per frame
	void Update () {
        if (isEating)
        {
            eatTimer += Time.deltaTime;
            if (eatTimer > timeToEat)
            {
                eatTimer = 0f;
                GameManager.EatVegetable();
            }
        }
        if (isAttacking)
        {
            if (attackTarget == null)
            {
                eatTimer = timeToEat;
                isAttacking = false;
            }
            else
            {

                eatTimer += Time.deltaTime;
                if (eatTimer > timeToEat)
                {
                    eatTimer = 0f;
                    bool destroyed = attackTarget.attack();
                    if (destroyed)
                    {
                        eatTimer = timeToEat;
                        isAttacking = false;
                    }
                }
            }
        }
	}

	void FixedUpdate() {
		//Travel toward next target in target list
		if (!done) {
			if (tileTarget != null) {
				Rigidbody2D rb = GetComponent<Rigidbody2D> ();
				rb.velocity = Vector3.Normalize (PlaceTower.TiletoWorld(tileTarget) - transform.position) * speed;
				float distance = Vector3.Distance (transform.position, PlaceTower.TiletoWorld(tileTarget));

				Debug.Log (this.transform.localRotation);
				Vector3 targ = PlaceTower.TiletoWorld(tileTarget);
				targ.z = 0f;


				Vector3 objectPos = transform.position;
				targ.x = targ.x - objectPos.x;
				targ.y = targ.y - objectPos.y;

				float angle = Mathf.Atan2 (targ.y, targ.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle - 90));
				Debug.Log (this.transform.localRotation);

				if (distance < 20f) {
					++nextTargetIndex;
					if (nextTargetIndex < tileTargetList.Length) {
						tileTarget = tileTargetList [nextTargetIndex];

					} else {
						done = true;
						//velocity goes to 0;
						rb.velocity = Vector3.Normalize (PlaceTower.TiletoWorld(tileTarget) - transform.position) * 0f;
					}

				}

                //script for collisions based on tile map
                /*                Vector2 currentTileLocation = PlaceTower.getTile(transform.position);
                currentTileLocation.y = 8 - currentTileLocation.y; //inverted coordinates on world objects vs mouse
                                MapManager.Tile currentTileType;

                                if (currentTileLocation.x < 0 || currentTileLocation.x > 13 || currentTileLocation.y < 0 || currentTileLocation.y > 8)
                                    currentTileType = MapManager.Tile.unbuildable;
                                else
                                    currentTileType = canvas.GetComponent<MapManager>().tileMap[(int)currentTileLocation.x, (int)currentTileLocation.y];*/

                if (isEating || isAttacking)
                {
                    rb.velocity = Vector3.Normalize(PlaceTower.TiletoWorld(tileTarget) - transform.position) * 0f;

                } 
            }


		}//if
	}

	 void OnTriggerEnter2D(Collider2D coll){
		//Debug.Log ("COLLIDED" + coll);
		if (coll.gameObject.tag == "garden") {
			isEating = true;
		}
        if (coll.gameObject.tag == "wall")
        {
            isAttacking = true;
            attackTarget = coll.gameObject.GetComponent<Wall>();

        }

	} 
}
