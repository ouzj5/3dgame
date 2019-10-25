using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : UnitySingleton<UserGUI>{
	private GUIStyle MyStyle;   //字体样式
	private GUIStyle MyButtonStyle;
	public static int outcome = 0; //游戏当前的结果状态，0: 游戏未结束，1: 玩家胜利， -1: 玩家失败
	int score = 0;

	void Start(){
		//获得UserAction接口对象，从而能够使用MySceneContoller中对应的方法
		//action = Director.get_Instance ().curren as UserAction;

		MyStyle = new GUIStyle ();
		MyStyle.fontSize = 40;
		MyStyle.normal.textColor = new Color (255f, 0, 0);
		MyStyle.alignment = TextAnchor.MiddleCenter;

		MyButtonStyle = new GUIStyle ("button");
		MyButtonStyle.fontSize = 30;
	}

	public void addScore() {
		if (outcome != -1)	
			score ++;
	}
	public void gameOver() {
		outcome = -1;
		SceneController.Instance ().gameStart = false;
	}
	void OnGUI(){
		GUI.Label (new Rect (30, 30, 100, 20), "Score:" + score, MyStyle);
		if (GUI.Button (new Rect (Screen.width / 2 - 75, 20, 150, 50), "Start", MyButtonStyle)) {
			outcome = 0;
			score = 0;
			SceneController.Instance ().restart ();
		}
		if (outcome == -1) {
			//玩家失败游戏结束
			GUI.Label (new Rect (Screen.width/2-Screen.width/8, 50, 100, 50), "Game Over!!!", MyStyle);
		} 
	}
}
