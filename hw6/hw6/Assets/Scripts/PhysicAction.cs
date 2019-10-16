using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicAction : SSAction {	
	float acceleration;						//重力加速度

	private PhysicAction() { }
	public static PhysicAction GetPhysicAction() {
		PhysicAction action = ScriptableObject.CreateInstance<PhysicAction>();//创建实例
		return action;
	}
	public override void Start () {
		acceleration = 9.8f;
	}

	public override void Update () {
		if (gameobject.activeSelf) {		//物体运动期间，每隔一个时间单位，物体的速度会加上一个a
			gameobject.GetComponent<Rigidbody> ().velocity += Vector3.down * acceleration * Time.deltaTime;
		} else {							//disk返回后回收该动作对象
			this.destroy = true;
			this.callback.SSActionEvent(this); 
		}

	}

	public override void FixedUpdate() {
	}
}