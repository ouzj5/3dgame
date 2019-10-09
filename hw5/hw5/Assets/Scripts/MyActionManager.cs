using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySceneActionManager : SSActionManager  //本游戏管理器
{

	private SSMoveToAction moveboat;     //移动船到结束岸，移动船到开始岸
	private SSMoveToAction movedisk;
	public SceneController sceneController;

	protected new void Start() {
		sceneController = SceneController.Instance();
		//sceneController.actionManager = this;
	}
	public void moveBoat(GameObject boat, Vector3 dest, float speed) {
		moveboat = SSMoveToAction.GetSSAction(dest, speed);
		this.RunAction(boat, moveboat, this);
	}
	public void moveDisk(GameObject disk, Vector3 dest, float speed) {
		movedisk = SSMoveToAction.GetSSAction (dest, speed);
		this.RunAction (disk, movedisk, this);
	}
}