using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSMoveToAction : SSAction                        //移动
{
	public Vector3 dest;        //移动到的目的地
	public float speed;           //移动速度
	public bool enableEmit = true;
	public Vector3 force;

	private SSMoveToAction() { }
	public static SSMoveToAction GetSSAction(Vector3 force, float speed) {
		SSMoveToAction action = ScriptableObject.CreateInstance<SSMoveToAction>();//创建实例
		action.force = force;
		action.speed = speed;
		return action;
	}

	public override void Update() {

	}
	public override void FixedUpdate() {	//给物体添加力的时候需要使用FixedUpdate函数
		if(!this.destroy) {		
			if(enableEmit) {				//给物体添加一个力，从而给物体一个初速度
				gameobject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
				enableEmit = false;
			}
			this.destroy = true;			//添加完力就进入回收队列
			this.callback.SSActionEvent(this); 
		}
	}

	public override void Start() {
		//移动过程无动作
	}
}