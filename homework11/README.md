## 1、 图片识别与建模
### AR SDK下载
[官网](https://developer.vuforia.com/)
下载结束之后将`VuforiaSupportInstalller`安装到Unity的根目录
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191224123444241.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L1Bhc3NlbmdlcjMxN18=,size_16,color_FFFFFF,t_70)
### 创建证书，获取License Key
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191224123738445.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L1Bhc3NlbmdlcjMxN18=,size_16,color_FFFFFF,t_70)
### 创建目标数据库
>用于对所有Target及其特征数据进行管理和保存

![在这里插入图片描述](https://img-blog.csdnimg.cn/20191224124144719.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L1Bhc3NlbmdlcjMxN18=,size_16,color_FFFFFF,t_70)
添加好的target，点击`DownloadDatabase`下载unity扩展包并导入项目

![在这里插入图片描述](https://img-blog.csdnimg.cn/2019122412385269.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L1Bhc3NlbmdlcjMxN18=,size_16,color_FFFFFF,t_70)
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191224124533474.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L1Bhc3NlbmdlcjMxN18=,size_16,color_FFFFFF,t_70)

### 创建AR Camera，添加License Key
右侧`create`->`Vuforia Engine`->选择添加`AR Camera`
`Inspector` ->`Open Vuforia Engine configuration`->添加`License Key`
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191224124702734.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L1Bhc3NlbmdlcjMxN18=,size_16,color_FFFFFF,t_70)
这步开始的时候遇到了无法选择`Open Vuforia Engine configuration`
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191224125713189.png)
对应解决办法：
[参考链接](https://blog.csdn.net/qq_35768238/article/details/80728931)
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191224125720662.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L1Bhc3NlbmdlcjMxN18=,size_16,color_FFFFFF,t_70)
### 添加Image Target
删除原有Main Camera，AR Camra下面添加一个`Camera`，下载好的皮卡丘模型挂载到`Image Targe`t下，并在`Image Target Behaviour` 添加我们刚刚下好的数据库（3D_course）
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191224125410362.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L1Bhc3NlbmdlcjMxN18=,size_16,color_FFFFFF,t_70)
### 图像识别结果
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191224124935657.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L1Bhc3NlbmdlcjMxN18=,size_16,color_FFFFFF,t_70)
## 2、 虚拟按键小游戏
### 增加虚拟按键
`Advanced`->`Add Virtual Button`
![在这里插入图片描述](https://img-blog.csdnimg.cn/2019122412491962.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L1Bhc3NlbmdlcjMxN18=,size_16,color_FFFFFF,t_70)
### 创建脚本
&emsp;对`IVuforiaButtonEventHandler`接口进行实现，以对虚拟按钮的按下与释放事件进行监听并处理，挂载到`ImageTarget`下并对变量进行指定
```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

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
```
>将Vuforia的`Virtual button`预制体挂载到`ImageTarget`下作为子对象，同时调整至合适大小和位置。为了使虚拟按钮可见，可以在按钮下添加相应大小的平面并附着材质
>
![在这里插入图片描述](https://img-blog.csdnimg.cn/20191224124950596.png?x-oss-process=image/watermark,type_ZmFuZ3poZW5naGVpdGk,shadow_10,text_aHR0cHM6Ly9ibG9nLmNzZG4ubmV0L1Bhc3NlbmdlcjMxN18=,size_16,color_FFFFFF,t_70)

[博客链接](https://blog.csdn.net/Passenger317_/article/details/103680593)    

[视频演示链接](http://m.v.qq.com/play/play.html?vid=y304030716n&ptag=4_7.7.2.23017_copy)

