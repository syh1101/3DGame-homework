using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

[System.Obsolete]
public class VirtualButtonEventHandler : MonoBehaviour, IVirtualButtonEventHandler
{
	// virtual button
    public GameObject vb;
    //动画组件
    public Animator ani;
    void IVirtualButtonEventHandler.OnButtonPressed(VirtualButtonBehaviour vb)
    {
        ani.SetTrigger("Take Off");
        Debug.Log("按钮按下！");
    }

    void IVirtualButtonEventHandler.OnButtonReleased(VirtualButtonBehaviour vb)
    {
        ani.SetTrigger("Land");
        Debug.Log("按钮释放！");
    }

    // Start is called before the first frame update
    void Start()
    {
        VirtualButtonBehaviour vbb = vb.GetComponent<VirtualButtonBehaviour>();
        if(vbb)
        {
            vbb.RegisterEventHandler(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
