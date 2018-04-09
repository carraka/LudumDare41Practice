using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public GameObject origTarget;
	public Vector2 origTargetVelocity;
	public float speed;

	private Canvas canvas;

	void Awake ()
	{
		canvas = GameObject.Find ("Canvas").GetComponent<Canvas> ();

	}
	// Use this for initialization
	void Start () {
		this.transform.SetParent (canvas.transform);

		StartCoroutine (checkSpeed ());

	}
	IEnumerator checkSpeed() {
		yield return new WaitForSeconds (3);
		StartCoroutine (checkSpeed ());
		if (GetComponent<Rigidbody2D>().velocity.magnitude == 0) {
			Destroy (this.gameObject);	
		}
	}
	// Update is called once per frame
	void FixedUpdate () {
		
		try {
			
			if (origTarget != null)	{
				

					transform.position = Vector3.MoveTowards(transform.position, origTarget.transform.position, speed);

			}
			else
			{
				Destroy(this.gameObject);
			}
		}
		catch(MissingReferenceException e) {}


	}
	void OnTriggerEnter2D(Collider2D coll){

		if (coll.gameObject.tag == "Grunt") {
			
			coll.gameObject.GetComponent<Grunt> ().hp--;

			AudioSource audio = gameObject.AddComponent < AudioSource > ();
			audio.PlayOneShot ((AudioClip)Resources.Load ("Audio/SoundFX/ldp2_cannon_hit"));

			Destroy (this.gameObject);	
		}
	}
}
