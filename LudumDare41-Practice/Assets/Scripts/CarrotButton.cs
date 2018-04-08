using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CarrotButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
		GameManager.PickVegetable ("carrot");

	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.UpdateInfoBox("Carrot");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.HideInfoBox();
    }

}
