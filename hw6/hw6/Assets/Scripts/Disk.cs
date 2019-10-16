using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour {

	private float timeLeave;			//记录每个disk剩余飞行的时间
	public  Vector3 velocity;			//每个飞碟的基础速度,disk的速度由基础速度与种类决定
	public GameObject nextDisk;			//记录下一个空闲的飞碟
	public int poolIndex;				//记录该飞碟对象在对象池中的位置
	public ClickGUI clickgui;			//每个飞碟的鼠标点击响应脚本
	public int kind;					//记录每个飞碟对象的类型
	public Rigidbody rigid;
	bool enableEmit = true;

	void Awake() {
		timeLeave = 0;
		/*
		 *创建、设置飞碟脚本 
		*/
		clickgui = this.gameObject.AddComponent(typeof(ClickGUI)) as ClickGUI;
		rigid = this.gameObject.AddComponent<Rigidbody> ();						//添加物理属性
		clickgui.setDisk(this);
	}

	public void init(Vector3 _position, Vector3 _velocity, float _lifeTime) {
		this.transform.position = _position;
		timeLeave = _lifeTime;
		velocity = _velocity;
	}

	void Update() {
		timeLeave -= Time.deltaTime;
		if (timeLeave < 0 || gameObject.transform.position.y < -5 || gameObject.transform.position.x < -10) 				//时间到了，返回到对象池
			ReturnToPool();
	}

	public void ReturnToPool() {
		//摧毁disk时需要将其的速度归零
		gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		DiskProductor.Instance().Return(poolIndex, kind);
	}
}