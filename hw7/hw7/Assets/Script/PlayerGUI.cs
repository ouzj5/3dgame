using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGUI : MonoBehaviour{
	Player player;
	public void Awake() {
		player = Player.Instance();
	}
	public void Update() {
		if (Input.GetKey(KeyCode.UpArrow)) {
			player.move(Direction.UP);
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			player.move(Direction.DOWN);
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			player.move(Direction.LEFT);
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			player.move(Direction.RIGHT);
		}
	}
}

