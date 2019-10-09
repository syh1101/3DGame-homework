﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DiskFactory : MonoBehaviour {

	private List<GameObject> used = new List<GameObject>();
	private List<GameObject> free = new List<GameObject>();

	private Color[] color = { Color.red, Color.green, Color.blue, Color.yellow };

	public GameObject GetDisk(int ruler)
	{
		GameObject a_disk;
		if (free.Count > 0)
		{
			a_disk = free[0];
			free.Remove(free[0]);
		}
		else
		{
			a_disk = GameObject.Instantiate(Resources.Load("prefabs/Disk")) as GameObject;
			Debug.Log(a_disk);
		}

		a_disk.GetComponent<DiskData>().size = UnityEngine.Random.Range(0, 7-ruler);
		a_disk.GetComponent<DiskData>().color = color[UnityEngine.Random.Range(0, 4)];
		a_disk.GetComponent<DiskData>().speed = UnityEngine.Random.Range(5+ruler, 8+ruler);

		a_disk.transform.localScale = new Vector3(a_disk.GetComponent<DiskData>().size * 2, a_disk.GetComponent<DiskData>().size * 0.1f, a_disk.GetComponent<DiskData>().size * 2);
		a_disk.GetComponent<Renderer>().material.color = a_disk.GetComponent<DiskData>().color;
		a_disk.SetActive(true);

		used.Add(a_disk);
		return a_disk;
	}
		
	public void FreeDisk(GameObject disk)
	{
		for(int i = 0; i < used.Count; i++)
		{
			if(used[i] == disk)
			{
				disk.SetActive(false);
				used.Remove(used[i]);
				free.Add(disk);
			}
		}
	}
}