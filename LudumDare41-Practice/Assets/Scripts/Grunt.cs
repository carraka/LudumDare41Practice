using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : MonoBehaviour {
	public bool isEating = false;
	public float timeToEat = 3f;
	public float eatTimer = 3f;

	private GameManager GameManager;

	public Transform[] targetList;// =new Transform[];
	private Transform target;
	public float speed = 0.1f;
	public int nextTargetIndex = 0;
	public bool inRange = false;
	bool done = false;

	// Use this for initialization
	void Start () {
		target = targetList[nextTargetIndex];
	}

	void Awake(){
		GameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();

	}

	
	// Update is called once per frame
	void Update () {
		if (isEating) {
			eatTimer += Time.deltaTime;
			if (eatTimer > timeToEat) {
				eatTimer = 0f;
				GameManager.EatVegetable ();
			}
		}
	}

	void FixedUpdate() {
		//Travel toward next target in target list
		if (!done) {
			if (target != null) {
				Rigidbody2D rb = GetComponent<Rigidbody2D> ();
				rb.velocity = Vector3.Normalize (target.transform.position - transform.position) * speed;
				float distance = Vector3.Distance (transform.position, target.transform.position);
				if (distance < 20f) {
					++nextTargetIndex;
					if (nextTargetIndex < targetList.Length) {
						target = targetList [nextTargetIndex];		
					} else {
						done = true;
						//velocity goes to 0;
						rb.velocity = Vector3.Normalize (target.transform.position - transform.position) * 0f;
					}

				}
			}
		}//if


	}

	void OnTriggerEnter2D(Collider2D coll){
		//Debug.Log ("COLLIDED" + coll);
		if (coll.gameObject.tag == "garden") {
			isEating = true;
		}

	}
}
