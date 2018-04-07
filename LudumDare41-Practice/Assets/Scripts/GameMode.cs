using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour {

    public GameObject tower;
    public Canvas canvas;
    public bool towerPlacementMode;

    private float tileWidth;
    private float tileHeight;
    private GameObject previewTower;
    // Use this for initialization
    void Start () {
        tileWidth = Camera.main.pixelWidth / 16f;
        tileHeight = Camera.main.pixelHeight / 9f;

        previewTower = Instantiate(tower);
        previewTower.transform.SetParent(canvas.transform, true);

        Vector3 scaleTower = new Vector3 (tileWidth/ 100, tileHeight / 100);

        previewTower.gameObject.transform.localScale = scaleTower;
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

    void OnGUI()
    {
        Vector3 p = new Vector3();
        Camera c = Camera.main;
        /*        Event e = Event.current;
                Vector2 mousePos = new Vector2();

                // Get the mouse position from Event.
                // Note that the y position from Event is inverted.
                mousePos.x = e.mousePosition.x;
                mousePos.y = c.pixelHeight - e.mousePosition.y;
        */

        Vector2 mousePos = getMousePos();
        Vector2 tilePos = getTile(mousePos);

        if (towerPlacementMode)
        {

            //find tile x,y
            //move sprite to tile x,y
            Vector3 previewPos = new Vector3(tilePos.x * tileWidth, c.pixelHeight - tilePos.y * tileHeight - tileHeight);
            //previewPos = Camera.main.ScreenToWorldPoint(previewPos);
            previewTower.transform.position = previewPos;
        }
        else
        {
            //hide tower placement preview
            previewTower.transform.position = new Vector3(-500, -500);
        }
    }
}
