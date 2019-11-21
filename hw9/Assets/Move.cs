using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour{
	public void Awake() {
		
	}
	public void Update() {
		if (Input.GetKey(KeyCode.UpArrow)) {
			move(Direction.UP);
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			move(Direction.DOWN);
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			move(Direction.LEFT);
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			move(Direction.RIGHT);
		}
	}
	public enum Direction : int {LEFT = 0, UP, RIGHT, DOWN};

		public void move(Direction direction) {
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

