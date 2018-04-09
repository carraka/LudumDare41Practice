using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

	/*
	 * 
	To run end game script for a win: PlayEnding(true);
	To run end game script for a loos: PlayEnding(false);

	*/


	public bool gameIsOver = false;

	public AudioSource[] bgAudio;

	private Image EndScreen;
	private Image creditsImage;
	private Image menuImage;
	private Text WinText;
	private Text LoseText;
	private Button CreditsButton;
	private Button ReturntoMenuButton;

	public Image[] winLoseImages;

	// Use this for initialization
	void Awake () {
		bgAudio = GameObject.Find ("Main Camera").GetComponents<AudioSource> ();

		EndScreen = GameObject.Find ("EndScreen").GetComponent<Image> ();
		WinText = GameObject.Find ("WinText").GetComponent<Text> ();
		LoseText = GameObject.Find ("LoseText").GetComponent<Text> ();
		CreditsButton = GameObject.Find ("CreditsButton").GetComponent<Button> ();
		ReturntoMenuButton = GameObject.Find ("ReturntoMenuButton").GetComponent<Button> ();
		creditsImage = GameObject.Find ("CreditsButton").GetComponent<Image> ();
		menuImage = GameObject.Find ("ReturntoMenuButton").GetComponent<Image> ();
	
		/*for (int i = 0; i < 6; i++)
		{
			string temp = "winLose" + i;
			Debug.Log (temp);
			winLoseImages [i] = GameObject.Find (temp).GetComponent<Image> ();
		}
	*/

	}

	void Start(){
		HideEndGraphics ();
		//PlayEnding (true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayEnding(bool gameWin){
		gameIsOver = true;

		EndScreen.enabled = true;
		CreditsButton.enabled = true;
		ReturntoMenuButton.enabled = true;
		creditsImage.enabled = true;
		menuImage.enabled = true;

		bgAudio[1].Stop ();

		AudioSource audio = gameObject.AddComponent < AudioSource > ();

		if (gameWin)
		{
			audio.PlayOneShot ((AudioClip)Resources.Load ("Audio/Music/ldp2_victory"));

			WinText.enabled = true;
			for (int i = 0; i < 3; i++)
			{
				winLoseImages [i].enabled = true;
			}
		}
		else
		{
			audio.PlayOneShot ((AudioClip)Resources.Load ("Audio/Music/ldp2_loss"));

			LoseText.enabled = true;
			for (int i = 3; i < 3; i++)
			{
				winLoseImages [i].enabled = true;
			}
		}

	}

	void HideEndGraphics(){
		for (int i = 0; i < 6; i++)
		{
			winLoseImages [i].enabled = false;
		}

	}
}
