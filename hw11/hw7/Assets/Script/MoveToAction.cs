using System;
using System.Collections.Generic;
using UnityEngine;

public class SSMoveToAction: SSAction {
	public Vector3 target;
	public float speed;

	public static SSMoveToAction GetSSAction(Vector3 _target, float _speed) {
		SSMoveToAction action = ScriptableObject.CreateInstance<SSMoveToAction>();
		action.target = _target;
		action.speed = _speed;
		return action;
	}

	public override void Start() {

	}

	public override void Update() {
		if (!SceneController.Instance ().gameStart)
			return;
		//Debug.Log(target);
		this.transform.position = Vector3.MoveTowards (this.transform.position, target, speed);
		if (this.transform.position.x == target.x
		&& this.transform.position.z == target.z
		&& UserGUI.Instance ()
		) {
			this.destroy = true;
			this.callback.SSActionEvent (this);
		}
		
	}

}
