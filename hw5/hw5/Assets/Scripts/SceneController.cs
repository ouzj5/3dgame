﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]


public class SceneController : UnitySingleton<SceneController>{

	public static int roundNum = 10;				//回合数量
	public static int trialNumber = 10;				//每个Round有多少个trial

	public float[] level = new float[roundNum]; 		//记录每个round出现的时间间隔，每个round的记录难度
	public float random; 							//时间间隔随机部分的范围

	private float time;								//用于计时
	private float timeInterval;						//记录时间间隔

	[SerializeField] private int round;				//round记录第几个回合
	[SerializeField] private int trial;				//count记录第几个trial

	public bool gameStart;
	public MySceneActionManager actionManager;		//动作管理对象

	void Awake() {
		_instance = this;
		for (int i = 0; i < roundNum; i++) {		//初始化每个round的难度，round值越大，时间间隔越小，难度越高
			level [i] = 6f - 0.5f * roundNum;
		}
		time = 0; timeInterval = 0; round = 0; trial = 0;
		gameStart = false;							//游戏初始状态为等待
		random = 0.1f;								//飞碟时间随机浮动范围

		actionManager = gameObject.AddComponent<MySceneActionManager>() as MySceneActionManager; 
		gameObject.AddComponent<UserGUI> ();		//添加用户界面
		DiskProductor.Instance(); 					//初始化disk对象创建工厂
		ScoreRecorder.Instance();					//初始化计分器
	}

	void Update() {
		if (gameStart) {							//判断游戏是否已经开始
			time += Time.deltaTime;
			if (time < timeInterval)				//时间没到返回
				return;	
			else {									//时间到创建disk，并且计算得到下一次创建的时间间隔
				trial ++;
				CreateDisk ();
				time -= timeInterval;
				timeInterval = level [round] + Random.Range (-random, random);
				if (trial < trialNumber)
					return;  
				trial = 0;							//一个回合结束
				round++;
			}
		}
	}

	void CreateDisk() {
		//生成随机位置，调用disk工厂创建disk
		Vector3 pos = new Vector3(10f, Random.Range(-4,4), 0);
		Vector3 v = new Vector3(-2f, Random.Range(-0.2f, 0.2f), 0);
		int kind = Random.Range (0, 3);				//随机选择三种飞碟的一种
		DiskProductor.Instance().Create(pos, v, 15f, kind);
	}
	public void Reset() {							//重置函数
		time = 0; timeInterval = 0; round = 0; trial = 0;
		DiskProductor.Instance ().Reset ();
	}
	public int GetRoundIndex() {					//获得回合数
		return round;
	}
}