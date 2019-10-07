using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSMoveToAction : SSAction                        //移动
{
	public Vector3 dest;        //移动到的目的地
	public float speed;           //移动速度

	private SSMoveToAction() { }
	public static SSMoveToAction GetSSAction(Vector3 dest, float speed) {
		SSMoveToAction action = ScriptableObject.CreateInstance<SSMoveToAction>();//创建实例
		action.dest = dest;
		action.speed = speed;
		return action;
	}

	public override void Update() {
		this.transform.position = Vector3.MoveTowards(this.transform.position, dest, speed * Time.deltaTime);
		if (this.transform.position == dest)
		{
			this.destroy = true;
			this.callback.SSActionEvent(this);      //告诉动作管理或动作组合这个动作已完成
		}
	}
	public override void Start() {
		//移动过程无动作
	}
}