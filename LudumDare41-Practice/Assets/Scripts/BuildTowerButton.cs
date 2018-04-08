using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildTowerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private GameManager GameManager;

	private Button thisButton;

	// Use this for initialization
	void Start () {
		thisButton = this.GetComponent<Button> ();
		thisButton.onClick.AddListener (TaskOnClick);
	}

	void Awake () {
		GameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();

	}
	// Update is called once per frame
	void Update ()
    {
        ColorBlock colorVar = thisButton.colors;
        if (GameManager.wood >= GameManager.costTowerWood && GameManager.stone >= GameManager.costTowerStone)
            colorVar.highlightedColor = new Color32(51, 255, 221, 255);
        else
            colorVar.highlightedColor = new Color32(255, 0, 0, 255);
        thisButton.colors = colorVar;
    }

    void TaskOnClick()
    {
        if (GameManager.wood >= GameManager.costTowerWood && GameManager.stone >= GameManager.costTowerStone) // if adequate supplies
        {
            if (GameManager.GetComponent<PlaceTower>().buildCommand == PlaceTower.towerPlacementMode.off)
                GameManager.GetComponent<GameManager>().PickVegetable("build"); //subtract 1 veggie from stock, but not if already building

            GameManager.GetComponent<PlaceTower>().buildCommand = PlaceTower.towerPlacementMode.tower; //set placement mode to tower
            GameManager.UpdateInfoBox("Build");
        }
        else
        {   // buzz noise
            GameManager.UpdateInfoBox("Not Enough");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.UpdateInfoBox("Cost Tower");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.HideInfoBox();
    }
}
