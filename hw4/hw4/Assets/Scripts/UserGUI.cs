using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {
	private UserAction action;  //MySceneController中与用户动作相关的接口
	private GUIStyle MyStyle;   //字体样式
	private GUIStyle MyButtonStyle;
	public static int outcome; //游戏当前的结果状态，0: 游戏未结束，1: 玩家胜利， -1: 玩家失败

	void Start(){
		//获得UserAction接口对象，从而能够使用MySceneContoller中对应的方法
		action = Director.get_Instance ().curren as UserAction;

		MyStyle = new GUIStyle ();
		MyStyle.fontSize = 40;
		MyStyle.normal.textColor = new Color (255f, 0, 0);
		MyStyle.alignment = TextAnchor.MiddleCenter;

		MyButtonStyle = new GUIStyle ("button");
		MyButtonStyle.fontSize = 30;
	}
	void reStart(){
		//显示从新开始按钮
		if (GUI.Button (new Rect (Screen.width/2-Screen.width/8, Screen.height/2+100, 150, 50), "Restart", MyButtonStyle)) {
			outcome = 0;
			action.restart ();
			action.setMove (0);
		}
	}

	void OnGUI(){
		reStart (); //显示restart按钮
		if (outcome == -1) {
			//玩家失败游戏结束
			GUI.Label (new Rect (Screen.width/2-Screen.width/8, 50, 100, 50), "Game Over!!!", MyStyle);
			reStart ();
		} else if (outcome == 1) {
			//玩家胜利
			GUI.Label (new Rect (Screen.width/2-Screen.width/8, 50, 100, 50), "You Win!!!", MyStyle);
			reStart ();
		}
	}
}
