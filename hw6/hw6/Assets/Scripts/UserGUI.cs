using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour{
	private GUIStyle MyStyle;
	private GUIStyle MyButtonStyle;

	void Start() {
		MyStyle = new GUIStyle ();
		MyStyle.fontSize = 20;
		MyStyle.normal.textColor = new Color (255f, 0, 0);

		MyButtonStyle = new GUIStyle ("button");
		MyButtonStyle.fontSize = 30;
	}
	void OnGUI() {
		//回合显示
		GUI.Label (new Rect (10, 10, 100, 20), "Round:" + (SceneController.Instance ().GetRoundIndex () + 1), MyStyle);
		//分数显示
		GUI.Label (new Rect (10, 40, 100, 20), "Score:" + (ScoreRecorder.Instance().score), MyStyle);
		if (SceneController.Instance ().gameStart) {
			//重启
			if (GUI.Button (new Rect (Screen.width / 2 - 75, 20, 150, 50), "Restart", MyButtonStyle)) {
				SceneController.Instance ().Reset ();
				SceneController.Instance ().gameStart = false;
			}
		} else {
			//游戏开始
			if (GUI.Button (new Rect (Screen.width / 2 - 75, 20, 150, 50), "Start", MyButtonStyle)) {
				SceneController.Instance ().gameStart = true;
			}
		}
	}
}