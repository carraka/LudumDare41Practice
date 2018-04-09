using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

    public int initialHP;
    public int tileX;
    public int tileY;

    private int currentHP;

	// Use this for initialization
	void Start () {
        currentHP = initialHP;
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public bool attack(int damage = 1)
    {
        Debug.Log(currentHP);
        currentHP -= damage;
		AudioSource audio = gameObject.AddComponent < AudioSource > ();
		audio.PlayOneShot ((AudioClip)Resources.Load ("Audio/SoundFX/ldp2_stonewall_damage"));


        if (currentHP <= 0)
        {
            Destroy(this.gameObject);
            GameObject.Find("Canvas").GetComponent<MapManager>().tileMap[tileX, tileY] = MapManager.Tile.road;
            // play building destruction sound

            return true; //return true if destroyed
        }
        else return false;
    }
}
