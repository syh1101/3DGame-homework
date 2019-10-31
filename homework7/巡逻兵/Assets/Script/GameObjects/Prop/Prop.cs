using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour {
    public int block;                      
    public bool follow_player = false;    

    private void Start()
    {
        if (gameObject.GetComponent<Rigidbody>())
        {
            gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    void Update()
    {
        if (this.gameObject.transform.localEulerAngles.x != 0 || gameObject.transform.localEulerAngles.z != 0)
        {
            gameObject.transform.localEulerAngles = new Vector3(0, gameObject.transform.localEulerAngles.y, 0);
        }
        if (gameObject.transform.position.y != 0)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
        }
    }
}
