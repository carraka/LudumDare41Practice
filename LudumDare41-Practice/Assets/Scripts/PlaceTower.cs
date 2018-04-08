using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceTower : MonoBehaviour {

    public GameObject tower;
    public GameObject wall;
    public Canvas canvas;
    public enum towerPlacementMode { off, tower, wall };
    public towerPlacementMode buildCommand;

    private float tileWidth;
    private float tileHeight;
    private GameObject previewBuild;
    private MapManager level;
    private Vector3 offset;

    private GameManager GameManager;

    // Use this for initialization
    void Start () {
        tileWidth = Camera.main.pixelWidth / 16f;
        tileHeight = Camera.main.pixelHeight / 9f;

        offset = new Vector3 (tileWidth / 2, tileHeight / 2);

        previewBuild = null;
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //buildCommand = towerPlacementMode.tower;

    }

    // Update is called once per frame
    void Update()
    {

    }


    //gets mouse position, with 0,0 being the top-left corner
    Vector2 getMousePos()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos.y = Camera.main.pixelHeight - mousePos.y;

        return mousePos;
    }

    //converts mouse position to a tile location, with 0,0 being the top left tile, and 15,8 being the lower right tile
    Vector2 getTile(Vector2 mousePos)
    {
        Vector2 tileLocation;

        tileLocation.x = Mathf.Floor(mousePos.x / tileWidth);
        tileLocation.y = Mathf.Floor(mousePos.y / tileHeight);

        return tileLocation;
    }

    void buildStructure(Vector3 tilePos)
    {

    }
    void OnGUI()
    {
        Vector2 mousePos = getMousePos();
        Vector2 tilePos = getTile(mousePos);

        if (buildCommand == towerPlacementMode.off)
        {
            if (previewBuild != null)
            {
                Destroy(previewBuild);
                previewBuild = null;
            }
        }
        else
        {

            if (previewBuild == null)
            {
                if (buildCommand == towerPlacementMode.tower)
                    previewBuild = Instantiate(tower);
                if (buildCommand == towerPlacementMode.wall)
                    previewBuild = Instantiate(wall);

                previewBuild.transform.SetParent(canvas.transform, true);

                Vector3 scaleTower = new Vector3(tileWidth / 100, tileHeight / 100);

                previewBuild.gameObject.transform.localScale = scaleTower;
            }

            //move sprite to tile x,y            
            //Vector3 previewPos = new Vector3(tilePos.x * tileWidth, Camera.main.pixelHeight - tilePos.y * tileHeight - tileHeight);
            Vector3 previewPos = TiletoWorld(tilePos);

            //if out of screen, make preview invisible and exit before checking map tile array
            if (tilePos.x < 0 || tilePos.x > 13 || tilePos.y < 0 || tilePos.y > 8)
            {
                previewBuild.GetComponent<Image>().color = Color.clear;
                return;
            }

            previewBuild.transform.position = previewPos;


            if ((buildCommand == towerPlacementMode.tower && canvas.GetComponent<MapManager>().tileMap[(int)tilePos.x, (int)tilePos.y] == MapManager.Tile.field) ||
                (buildCommand == towerPlacementMode.wall && canvas.GetComponent<MapManager>().tileMap[(int)tilePos.x, (int)tilePos.y] == MapManager.Tile.road))
            {
                previewBuild.GetComponent<Image>().color = Color.green;

                //if left click, build tower
                if (Input.GetMouseButtonDown(0))
                {
                    previewBuild.GetComponent<Image>().color = Color.white;

                    if (buildCommand == towerPlacementMode.tower)
                    {
                        canvas.GetComponent<MapManager>().tileMap[(int)tilePos.x, (int)tilePos.y] = MapManager.Tile.tower;  // put tower on tilemap
                        GameManager.wood -= GameManager.costTowerWood;    //pay wood cost
                        GameManager.stone -= GameManager.costTowerStone;  //pay stone cost
                    }
                    if (buildCommand == towerPlacementMode.wall)
                    {
                        canvas.GetComponent<MapManager>().tileMap[(int)tilePos.x, (int)tilePos.y] = MapManager.Tile.wall;  // put wall on tilemap
                        GameManager.wood -= GameManager.costWallWood;    //pay wood cost
                        GameManager.stone -= GameManager.costWallStone;  //pay stone cost
                    }
                    buildCommand = towerPlacementMode.off; //buildStructure(tilePos);
                    previewBuild = null;

                    // play build sound effect here
                }
            }
            else
                previewBuild.GetComponent<Image>().color = Color.red;

            //if right click or escape button pressed, cancel built
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
            {
                buildCommand = towerPlacementMode.off;
            }

            
        }
    }

	//converts x, y position to position in world in relation to camera
	public Vector3 TiletoWorld(Vector2 tileInput)
	{
		Vector3 worldPos = new Vector3(tileInput.x * tileWidth, Camera.main.pixelHeight - tileInput.y * tileHeight - tileHeight);
        worldPos += offset;

		return worldPos;
	}

}
