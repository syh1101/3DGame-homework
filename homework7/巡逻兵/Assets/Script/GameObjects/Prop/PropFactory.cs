using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropFactory {
    
    public static PropFactory PF = new PropFactory();
    private Dictionary<int, GameObject> used = new Dictionary<int, GameObject>();

    private PropFactory()
    {
    }

    int[] pos_x = { -7, 1, 7 };
    int[] pos_z = { 8, 2, -8 };
    public Dictionary<int, GameObject> GetProp()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                GameObject newProp = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Prop"));
                newProp.AddComponent<Prop>();
                newProp.transform.position = new Vector3(pos_x[j], 0, pos_z[i]);
                newProp.GetComponent<Prop>().block = i * 3 + j;
                newProp.SetActive(true);
                used.Add(i*3+j, newProp);
            }
        }
        return used;
    }

    public void StopPatrol()
    {
        for (int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                used[i * 3 + j].transform.position = new Vector3(pos_x[j], 0, pos_z[i]);
            }
        }
    }
}
