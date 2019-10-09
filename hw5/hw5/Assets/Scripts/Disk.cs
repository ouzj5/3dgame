using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour {

	private float timeLeave;			//记录每个disk剩余飞行的时间
	private Vector3 velocity;			//每个飞碟的基础速度,disk的速度由基础速度与种类决定
	public GameObject nextDisk;			//记录下一个空闲的飞碟
	public int poolIndex;				//记录该飞碟对象在对象池中的位置
	public ClickGUI clickgui;			//每个飞碟的鼠标点击响应脚本
	public int kind;					//记录每个飞碟对象的类型

	void Awake() {
		timeLeave = 0;
		/*
		 *创建、设置飞碟脚本 
		*/
		clickgui = this.gameObject.AddComponent(typeof(ClickGUI)) as ClickGUI;
		clickgui.setDisk(this);
	}

	public void init(Vector3 _position, Vector3 _velocity, float _lifeTime) {
		this.transform.position = _position;
		timeLeave = _lifeTime;
		velocity = _velocity;
	}

	void Update() {
		timeLeave -= Time.deltaTime;
		if (timeLeave < 0) 				//时间到了，返回到对象池
			ReturnToPool();
		else {							//还有时间就继续移动
			SceneController.Instance().actionManager.moveDisk (this.gameObject, velocity * Time.deltaTime * (kind + 1) + this.transform.position, 50);
		}
	}

	public void ReturnToPool() {
		DiskProductor.Instance().Return(poolIndex, kind);

	}
}