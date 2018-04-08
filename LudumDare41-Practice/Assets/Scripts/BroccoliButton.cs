using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BroccoliButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private GameManager GameManager;

	private Button thisButton;

	// Use this for initialization
	void Start () {
		Button btn = this.GetComponent<Button> ();
		btn.onClick.AddListener (TaskOnClick);
	}

	void Awake () {
		GameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();

	}
	// Update is called once per frame
	void Update () {

	}

	void TaskOnClick(){
		GameManager.PickVegetable ("broc");

	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.UpdateInfoBox("Broccoli");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.HideInfoBox();
    }

}