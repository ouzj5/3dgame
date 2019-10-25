using System;
using System.Collections.Generic;
using UnityEngine;

public enum Direction : int {LEFT = 0, UP, RIGHT, DOWN};
public class Player : UnitySingleton<Player> {
	
	public void move(Direction direction) {
		if (!SceneController.Instance ().gameStart)
			return;
		gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, ((int)direction - 1) * 90, 0));
		switch (direction) {
		case Direction.UP:
			gameObject.transform.position += new Vector3(0, 0, 0.1f);
			break;
		case Direction.DOWN:
			gameObject.transform.position += new Vector3(0, 0, -0.1f);
			break;
		case Direction.LEFT:
			gameObject.transform.position += new Vector3(-0.1f, 0, 0);
			break;
		case Direction.RIGHT:
			gameObject.transform.position += new Vector3(0.1f, 0, 0);
			break;
		}
	}
}