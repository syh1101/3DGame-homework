using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHalo : MonoBehaviour {
    private ParticleSystem particleSys;  // 粒子系统  
    private ParticleSystem.Particle[] particleArr;  // 粒子数组  
    private CirclePosition[] circle; // 极坐标数组
    public int count = 10000;       // 粒子数量  
    public float size = 0.03f;      // 粒子大小  
    public float minRadius = 5.0f;  // 最小半径  
    public float maxRadius = 12.0f; // 最大半径  
    public bool clockwise = true;   // 顺时针|逆时针  
    public float speed = 2f;        // 速度  
    public float maxRadiusChange = 0.02f;  // 移动范围
    private NormalDistribution normalGenerator = new NormalDistribution(); // 高斯分布生成器
    public Color startColor = Color.blue; //初始颜色

    void Start()
    {   // 初始化粒子数组  
        particleArr = new ParticleSystem.Particle[count];
        circle = new CirclePosition[count];
        // 初始化粒子系统  
        particleSys = this.GetComponent<ParticleSystem>();
        var main = particleSys.main;
        main.startSpeed = 0;              
        main.startSize = size;          // 设置粒子大小  
        main.loop = false;
        main.maxParticles = count;      // 设置最大粒子量  
        particleSys.Emit(count);               // 发射粒子  
        particleSys.GetParticles(particleArr);
        RandomlySpread();   // 初始化各粒子位置  
    }

    private int tier = 12;  
    void Update()
    {
        for (int i = 0; i < count; i++)
        {
            if (clockwise)  // 顺时针旋转  
                circle[i].angle -= (i % tier + 1) * (speed / circle[i].radius / tier);
            else            // 逆时针旋转  
                circle[i].angle += (i % tier + 1) * (speed / circle[i].radius / tier);
            circle[i].angle = (360.0f + circle[i].angle) % 360.0f;
            float theta = circle[i].angle / 180 * Mathf.PI;
            particleArr[i].position = new Vector3(circle[i].radius * Mathf.Cos(theta), 0f, circle[i].radius * Mathf.Sin(theta));
            particleArr[i].startColor = startColor;
            circle[i].time += Time.deltaTime;
            circle[i].radius += Mathf.PingPong(circle[i].time / minRadius / maxRadius, maxRadiusChange) - maxRadiusChange / 2.0f;
        }

        particleSys.SetParticles(particleArr, particleArr.Length);
    }

    void RandomlySpread()
    {
        for (int i = 0; i < count; ++i)
        {   
            float midRadius = (maxRadius + minRadius) / 2;
            float radius = (float)normalGenerator.NextGaussian(midRadius, 0.7);

            float angle = Random.Range(0.0f, 360.0f);
            float theta = angle / 180 * Mathf.PI;
            float time = Random.Range(0.0f, 360.0f);    
            float radiusChange = Random.Range(0.0f, maxRadiusChange);   
            circle[i] = new CirclePosition(radius, angle, time);
            particleArr[i].position = new Vector3(circle[i].radius * Mathf.Cos(theta), 0f, circle[i].radius * Mathf.Sin(theta));
        }

        particleSys.SetParticles(particleArr, particleArr.Length);
    }
}
