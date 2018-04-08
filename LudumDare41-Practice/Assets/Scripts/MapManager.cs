using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    public enum Tile {unbuildable = 0, field, road, tower, wall, garden};
    public Tile[,] tileMap;
    public bool loadLevel;
    public int levelNumber;
	// Use this for initialization
	void Start () {
        loadLevel = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (loadLevel)
        {
            //if (levelNumber == 1)
            tileMap = new Tile[,] {
                { Tile.field,Tile.field,Tile.field,Tile.field,Tile.field,Tile.field,Tile.field,Tile.field,Tile.field },
                { Tile.road,Tile.road,Tile.road,Tile.road,Tile.road,Tile.road,Tile.road,Tile.road,Tile.field },
                { Tile.field,Tile.field,Tile.field,Tile.field,Tile.field,Tile.field,Tile.field,Tile.road,Tile.field},
                { Tile.field,Tile.field,Tile.field,Tile.field,Tile.field,Tile.field,Tile.field,Tile.road,Tile.field},
                { Tile.field,Tile.field,Tile.field,Tile.road,Tile.road,Tile.road,Tile.road,Tile.road,Tile.field },
                { Tile.field,Tile.field,Tile.field,Tile.road,Tile.field,Tile.field,Tile.field,Tile.field,Tile.field },
                { Tile.field,Tile.field,Tile.field,Tile.road,Tile.field,Tile.field,Tile.field,Tile.field,Tile.field },
                { Tile.field,Tile.field,Tile.field,Tile.road,Tile.road,Tile.field,Tile.field,Tile.field,Tile.field },
                { Tile.field,Tile.field,Tile.field,Tile.field,Tile.road,Tile.field,Tile.field,Tile.field,Tile.field },
                { Tile.field,Tile.field,Tile.field,Tile.field,Tile.road,Tile.field,Tile.field,Tile.field,Tile.field },
                { Tile.field,Tile.field,Tile.field,Tile.field,Tile.road,Tile.field,Tile.field,Tile.field,Tile.field },
                { Tile.unbuildable,Tile.unbuildable,Tile.unbuildable,Tile.garden,Tile.garden,Tile.garden,Tile.unbuildable,Tile.unbuildable,Tile.unbuildable },
                { Tile.unbuildable,Tile.unbuildable,Tile.unbuildable,Tile.garden,Tile.garden,Tile.garden,Tile.unbuildable,Tile.unbuildable,Tile.unbuildable },
                { Tile.unbuildable,Tile.unbuildable,Tile.unbuildable,Tile.garden,Tile.garden,Tile.garden,Tile.unbuildable,Tile.unbuildable,Tile.unbuildable }
            };
            loadLevel = false;
        }
	}
}
