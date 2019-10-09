using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.game;


public class UserGUI : MonoBehaviour
{
	private IUserAction action;
	private float width, height;
	private string countDownTitle;

	void Start()
	{
		countDownTitle = "Start";
		action = SSDirector.getInstance().currentScenceController as IUserAction;
	}

	float castw(float scale)
	{
		return (Screen.width - width) / scale;
	}

	float casth(float scale)
	{
		return (Screen.height - height) / scale;
	}

	void OnGUI()
	{
		width = Screen.width / 12;
		height = Screen.height / 12;

		GUI.Button(new Rect(860, 10, 80, 30), "Time: "+((RoundController)SSDirector.getInstance().currentScenceController).count.ToString());
		//round
		GUI.Button(new Rect(590, 10, 80, 30), "Round "+((RoundController)SSDirector.getInstance().currentScenceController).getRound());
		//score
		GUI.Button(new Rect(680, 10, 80, 30), "Score "+((RoundController)SSDirector.getInstance().currentScenceController).scoreRecorder.getScore().ToString());

		if (GUI.Button (new Rect (770, 10, 80, 30), "Restart")) {
			SSDirector.getInstance ().currentScenceController.Restart ();
		}

		if (SSDirector.getInstance().currentScenceController.state != State.WIN && SSDirector.getInstance().currentScenceController.state != State.LOSE
			&& GUI.Button(new Rect(500, 10, 80, 30), countDownTitle))
		{
			if (countDownTitle == "Start") {
				countDownTitle = "Pause";

				SSDirector.getInstance().currentScenceController.Resume();
			}
			else if (countDownTitle == "Continue")
			{
				countDownTitle = "Pause";
				SSDirector.getInstance().currentScenceController.Resume();
			}
			else
			{
				countDownTitle = "Continue";
				SSDirector.getInstance().currentScenceController.Pause();
			}
		}

		if (SSDirector.getInstance().currentScenceController.state == State.WIN)//win
		{
			if (GUI.Button(new Rect(castw(2f), casth(4f), width, height), "You Win!"))
			{
	
				SSDirector.getInstance().currentScenceController.Restart();
			}
		}
		else if (SSDirector.getInstance().currentScenceController.state == State.LOSE)//lose
		{
			if (GUI.Button(new Rect(castw(2f), casth(4f), width, height), "You Lose!"))
			{
				SSDirector.getInstance().currentScenceController.Restart();

			}
		}
	}

	void Update()
	{
		action.shoot();
	}

}