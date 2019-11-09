using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMove : MonoBehaviour {
	private ParticleSystem particlesys;  			// 粒子系统
	private ParticleSystem.Particle[] particleArr;  // 粒子数组  
	private ParticleData[] particles; 				// 粒子数据数组
	public Gradient colorGradient; 					// 粒子颜色渐变

	public int count = 10000;       				// 粒子数量  
	public float size = 0.03f;      				// 粒子大小  
	public float minRad = 1f;  						// 最小半径  
	public float maxRad = 3f; 						// 最大半径  
	public bool clock = true;   					// 顺时针|逆时针  
	public float speed = 2f;        				// 速度
	public float pingPong = 0.002f;  				// 游离范围



	void Start () {									//初始化粒子数组、粒子系统
		ParticleSystem.MainModule settings = GetComponent<ParticleSystem>().main;
		particleArr = new ParticleSystem.Particle[count];
		particles = new ParticleData[count];
		// 初始化粒子系统  
		particlesys = this.GetComponent<ParticleSystem>();
		settings.startSpeed = 0;            // 粒子位置由程序控制  
		settings.startSize = size;          // 设置粒子大小  
		settings.loop = false;              // 设置粒子不循环
		settings.maxParticles = count;      // 设置最大粒子量  
		particlesys.Emit(count);               // 发射粒子
		particlesys.GetParticles(particleArr);

		for (int i = 0; i < count; ++i){
			// 随机每个粒子距离中心的半径，同时希望粒子集中在平均半径附近  
			float midRadius = (maxRad + minRad) / 2;
			float minRate = Random.Range(1.0f, midRadius / minRad);
			float maxRate = Random.Range(midRadius / maxRad, 1.0f);
			float radius = Random.Range(minRad * minRate, maxRad * maxRate);

			// 随机每个粒子的角度  
			float angle = Random.Range(0.0f,360.0f);
			float theta = angle / 180 * Mathf.PI;

			// 随机每个粒子的游离起始时间  
			float time = Random.Range(0.0f, 360.0f);
			//给每个粒子赋值，并保存粒子数据
			particles[i] = new ParticleData(radius, angle, time);
			//圆环
			//particleArr[i].position = new Vector3(particles[i].radius * Mathf.Cos(theta), 0f, particles[i].radius * Mathf.Sin(theta));
			//心形
			particleArr[i].position = new Vector3(0.1f * particles[i].radius * 16 * Mathf.Sin(theta) * Mathf.Sin(theta) * Mathf.Sin(theta), 0.1f * particles[i].radius *(13 * Mathf.Cos(theta) - 5 * Mathf.Cos(theta * 2) - 2 * Mathf.Cos(theta * 3) - Mathf.Cos(4 * theta)), 0f);
			particleArr[i].color = colorGradient.Evaluate(particles[i].angle / 360.0f);
		}

		particlesys.SetParticles(particleArr, particleArr.Length);
	}

	private int tier = 10;  // 速度差分层数
	void Update () {
		for (int i = 0; i < count; i++) {
			if (clock)  // 顺时针旋转  
				particles[i].angle -= (i % tier + 1) * (speed / particles[i].radius / tier);
			else            // 逆时针旋转  
				particles[i].angle += (i % tier + 1) * (speed / particles[i].radius / tier);
			// 保证angle在0~360度  
			particles[i].angle = (360 + particles[i].angle) % 360.0f;
			float theta = particles[i].angle / 180 * Mathf.PI;

			particleArr[i].position = new Vector3(0.1f * particles[i].radius * 16 * Mathf.Sin(theta) * Mathf.Sin(theta) * Mathf.Sin(theta), 0.1f * particles[i].radius *(13 * Mathf.Cos(theta) - 5 * Mathf.Cos(theta * 2) - 2 * Mathf.Cos(theta * 3) - Mathf.Cos(4 * theta)), 0f);

			// 粒子在半径方向上游离  
			particles[i].time += Time.deltaTime;
			particles[i].radius += Mathf.PingPong(particles[i].time / minRad / maxRad, pingPong) - pingPong / 2.0f;

			particleArr[i].color = colorGradient.Evaluate(particles[i].angle / 360.0f);

		}

		particlesys.SetParticles(particleArr, particleArr.Length);
	}
}