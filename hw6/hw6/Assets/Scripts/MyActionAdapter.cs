using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAdapter : SSActionManager{
	private SSMoveToAction movedisk;
	private PhysicAction physic;
	public SceneController sceneController;
	public void Start(){
		sceneController = SceneController.Instance();
		sceneController.action = this;
	}

	public void moveDisk(GameObject disk, Vector3 dest, float speed) {
		movedisk = SSMoveToAction.GetSSAction (dest, speed);		//获得被是配的对象
		this.RunAction (disk, movedisk, this);						//调用被是配对象的函数
		physic = PhysicAction.GetPhysicAction ();
		this.RunAction (disk, physic, this);
	}
}

