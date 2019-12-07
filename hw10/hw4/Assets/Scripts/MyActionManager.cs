using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySceneActionManager : SSActionManager  //本游戏管理器
{

	private SSMoveToAction moveboat;     //移动船到结束岸，移动船到开始岸
	private SequenceAction movech;

	public MySceneController sceneController;

	protected new void Start() {
		sceneController = (MySceneController)Director.get_Instance().curren;
		sceneController.actionManager = this;
	}
	public void moveBoat(GameObject boat, Vector3 dest, float speed) {
		moveboat = SSMoveToAction.GetSSAction(dest, speed);
		this.RunAction(boat, moveboat, this);
	}
	public void moveCharacter(GameObject ch, Vector3 middle, Vector3 dest,float speed) {
		//先到岸边，再上船
		SSAction action1 = SSMoveToAction.GetSSAction(middle, speed);
		SSAction action2 = SSMoveToAction.GetSSAction(dest, speed);
		movech = SequenceAction.GetSSAcition(1, 0, new List<SSAction>{action1, action2}); //船动作组合
		this.RunAction(ch, movech, this);
	}
}