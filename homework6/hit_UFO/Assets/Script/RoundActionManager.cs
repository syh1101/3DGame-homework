﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.game;

public class RoundActionManager : SSActionManager, ISSActionCallback, IActionManager
{
	public RoundController scene;
	public MoveToAction action1, action2;
	public SequenceAction saction;
	float speed;
	public int If_Active;


	public void addRandomAction(GameObject gameObj)
	{
		int[] X = { -20, 20 };
		int[] Y = { -5, 5 };
		int[] Z = { -20, -20 };

		// 随机生成起始点和终点
		Vector3 starttPos = new Vector3(
			UnityEngine.Random.Range(-70, 70),
			UnityEngine.Random.Range(-10, 10),
			UnityEngine.Random.Range(100, 150)
		);

		gameObj.transform.position = starttPos;

		Vector3 randomTarget = new Vector3(
			X[UnityEngine.Random.Range(0, 2)],
			Y[UnityEngine.Random.Range(0, 2)],
			Z[UnityEngine.Random.Range(0, 2)]
		);

		MoveToAction action = MoveToAction.getAction(randomTarget, gameObj.GetComponent<DiskData>().speed);

		RunAction(gameObj, action, this);
	}

	protected  void Start()
	{
		scene = (RoundController)SSDirector.getInstance().currentScenceController;
		scene.actionManager = this;
	}

	protected new void Update()
	{
		base.Update();
	}

	public void actionDone(SSAction source)
	{
		Debug.Log("Done");
	}
}