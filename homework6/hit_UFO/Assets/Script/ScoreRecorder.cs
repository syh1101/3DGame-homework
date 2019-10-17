using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour {
	private float score;

	public float getScore()
	{
		return score;
	}

	public void Record(GameObject disk)
	{
		score += (100 - disk.GetComponent<DiskData>().size *(20 - disk.GetComponent<DiskData>().speed));

		Color c = disk.GetComponent<DiskData>().color;
		switch (c.ToString())
		{
		case "red":
			score += 250;
			break;
		case "green":
			score += 200;
			break;
		case "blue":
			score += 150;
			break;
		case "yellow":
			score += 100;
			break;
		}
	}

	public void Reset()
	{
		score = 0;
	}
}
